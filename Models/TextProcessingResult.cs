using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextSummariser.Models
{
    /// <summary>
    /// Represents the current state and results of a text processing operation
    /// </summary>
    public class TextProcessingResult : INotifyPropertyChanged
    {
        private ProcessingStatus _status = ProcessingStatus.NotStarted;
        private string _summaryText = string.Empty;
        private string? _errorMessage;
        private string _fileName = string.Empty;
        private string _fileContent = string.Empty;

        /// <summary>
        /// Event that fires when a property changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Current processing status
        /// </summary>
        public ProcessingStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        /// <summary>
        /// The generated summary text
        /// </summary>
        public string SummaryText
        {
            get => _summaryText;
            set => SetProperty(ref _summaryText, value);
        }

        /// <summary>
        /// Error message if processing failed
        /// </summary>
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Name of the processed file
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        /// <summary>
        /// Content of the processed file
        /// </summary>
        public string FileContent
        {
            get => _fileContent;
            set => SetProperty(ref _fileContent, value);
        }

        /// <summary>
        /// Checks whether the result contains valid processable content
        /// </summary>
        public bool HasContent => !string.IsNullOrEmpty(FileContent) && !string.IsNullOrEmpty(FileName);

        /// <summary>
        /// Resets the result to its initial state
        /// </summary>
        public void Reset()
        {
            Status = ProcessingStatus.NotStarted;
            SummaryText = string.Empty;
            ErrorMessage = null;
            FileName = string.Empty;
            FileContent = string.Empty;
        }

        /// <summary>
        /// Sets a property value and raises the PropertyChanged event
        /// </summary>
        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
