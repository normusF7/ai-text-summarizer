using Microsoft.JSInterop;
using System.Text.Json;

namespace TextSummariser.Services
{
    /// <summary>
    /// Browser-based implementation of ILocalStorageService
    /// </summary>
    public class BrowserLocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public BrowserLocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <inheritdoc/>
        public async Task<T?> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
            if (json == null)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <inheritdoc/>
        public async Task SetItemAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        /// <inheritdoc/>
        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
