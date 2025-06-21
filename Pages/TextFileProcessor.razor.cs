using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Diagnostics;
using TextSummariser.Models;
using TextSummariser.Models.Config;
using TextSummariser.Services;

namespace TextSummariser.Pages
{
    public partial class TextFileProcessor : ComponentBase, IDisposable
    {
        private readonly SummaryState _state = new();
        private readonly ApiSettings _apiSettings = new();
        
        private bool _isFileSelected;
        private bool _isInitialized;
        private CancellationTokenSource? _cancellationTokenSource;

        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] private ITextProcessingService TextProcessingService { get; set; } = null!;
        [Inject] private ILocalStorageService LocalStorage { get; set; } = null!;
        
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var savedKey = await LocalStorage.GetItemAsync<string>(AppConstants.ApiKeyStorageKey);
                if (!string.IsNullOrEmpty(savedKey))
                {
                    _apiSettings.GeminiApiKey = savedKey;
                    _apiSettings.RememberApiKey = true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error loading API key: {exception.Message}");
            }

            _isInitialized = true;
            await base.OnInitializedAsync();
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
        
        private async Task LoadFile(InputFileChangeEventArgs eventArgs)
        {
            var file = eventArgs.File;

            try
            {
                await ReadFileAsync(file);
            }
            catch (Exception exception)
            {
                _state.SetError($"Error loading file: {exception.Message}");
                _isFileSelected = false;
            }
        }
        
        private async Task ReadFileAsync(IBrowserFile file)
        {                    
            if (file.ContentType != AppConstants.ExpectedContentType)
            {
                _state.SetError("Please select a text (.txt) file.");
                _isFileSelected = false;
                return;
            }

            try
            {
                await using var stream = file.OpenReadStream(maxAllowedSize: AppConstants.MaxFileSize);
                using var reader = new StreamReader(stream);
                var content = await reader.ReadToEndAsync();

                _state.SetFile(file.Name, content);
                _isFileSelected = true;
            }
            catch (Exception exception)
            {
                _state.SetError($"Failed to read file: {exception.Message}");
                _isFileSelected = false;
                throw;
            }
        }
        
        private async Task ProcessFile()
        {
            if (!_isFileSelected)
            {
                _state.SetError("Please select a text file.");
                return;
            }

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            _state.StartProcessing();
            await InvokeAsync(StateHasChanged);

            try
            {
                if (string.IsNullOrEmpty(_apiSettings.GeminiApiKey))
                {
                    throw new InvalidOperationException("Gemini API key is not configured. Please add it to your application settings.");
                }

                if (_apiSettings.RememberApiKey)
                {
                    await SaveApiKeyAsync();
                }

                var summaryText = await TextProcessingService.GenerateSummaryAsync(
                    _state.FileContent,
                    _apiSettings.GeminiApiKey);

                if (!string.IsNullOrEmpty(summaryText) && !cancellationToken.IsCancellationRequested)
                {
                    await PrintResponseAsync(summaryText, cancellationToken);
                }
                else if (!cancellationToken.IsCancellationRequested)
                {
                    _state.CompleteProcessing("No response generated.");
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Text processing operation was canceled");
            }
            catch (Exception exception)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    _state.SetError($"An error occurred: {exception.Message}");
                }
            }
            finally
            {
                if (!cancellationToken.IsCancellationRequested && _state.Status == ProcessingStatus.Processing)
                {
                    _state.Status = ProcessingStatus.Completed;
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
        
        private async Task PrintResponseAsync(string fullResponse, CancellationToken cancellationToken)
        {
            var summary = string.Empty;

            foreach (var character in fullResponse)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                summary += character;
                _state.CompleteProcessing(summary);
                await InvokeAsync(StateHasChanged);
                await Task.Delay(1, cancellationToken);
            }
        }

        private async Task CopySummary()
        {
            if (string.IsNullOrEmpty(_state.SummaryText))
            {
                return;
            }

            try
            {
                var success = await JsRuntime.InvokeAsync<bool>("copyToClipboard", _state.SummaryText);

                if (!success)
                {
                    _state.SetError("Failed to copy to clipboard. Please try again or copy manually.");
                }
            }
            catch (Exception ex)
            {
                _state.SetError($"Failed to copy: {ex.Message}");
            }
            finally
            {
                await InvokeAsync(StateHasChanged);
            }
        }
        
        private async Task SaveApiKeyAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_apiSettings.GeminiApiKey))
                {
                    await LocalStorage.SetItemAsync(AppConstants.ApiKeyStorageKey, _apiSettings.GeminiApiKey);
                }
                else
                {
                    await LocalStorage.RemoveItemAsync(AppConstants.ApiKeyStorageKey);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error saving API key: {exception.Message}");
            }
        }
    }
}