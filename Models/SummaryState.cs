namespace TextSummariser.Models
{
    public class SummaryState
    {
        /// <summary>
        /// Status of the current processing operation
        /// </summary>
        public ProcessingStatus Status { get; set; } = ProcessingStatus.NotStarted;

        /// <summary>
        /// The generated summary text
        /// </summary>
        public string SummaryText { get; private set; } = string.Empty;

        /// <summary>
        /// Error message if any operation failed
        /// </summary>
        public string? ErrorMessage { get; private set; }

        /// <summary>
        /// Name of the source file
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        /// Content of the source file
        /// </summary>
        public string FileContent { get; private set; } = string.Empty;

        /// <summary>
        /// Whether a file has been loaded
        /// </summary>
        public bool IsFileLoaded => !string.IsNullOrEmpty(FileContent) && !string.IsNullOrEmpty(FileName);

        /// <summary>
        /// Whether summary has been generated
        /// </summary>
        public bool HasSummary => !string.IsNullOrEmpty(SummaryText);

        /// <summary>
        /// Whether there is an error to display
        /// </summary>
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        /// <summary>
        /// Resets file-related data
        /// </summary>
        public void ResetFile()
        {
            FileName = string.Empty;
            FileContent = string.Empty;
            ResetSummary();
        }

        /// <summary>
        /// Resets summary-related data while keeping file data
        /// </summary>
        public void ResetSummary()
        {
            Status = ProcessingStatus.NotStarted;
            SummaryText = string.Empty;
            ErrorMessage = null;
        }

        /// <summary>
        /// Sets file data
        /// </summary>
        public void SetFile(string fileName, string content)
        {
            FileName = fileName;
            FileContent = content;
            ResetSummary();
        }

        /// <summary>
        /// Starts processing
        /// </summary>
        public void StartProcessing()
        {
            Status = ProcessingStatus.Processing;
            SummaryText = string.Empty;
            ErrorMessage = null;
        }

        /// <summary>
        /// Completes processing with success
        /// </summary>
        public void CompleteProcessing(string summary)
        {
            Status = ProcessingStatus.Completed;
            SummaryText = summary;
            ErrorMessage = null;
        }

        /// <summary>
        /// Sets an error state
        /// </summary>
        public void SetError(string message)
        {
            ErrorMessage = message;
        }
    }
}
