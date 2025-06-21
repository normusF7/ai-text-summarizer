namespace TextSummariser.Models.Config
{
    /// <summary>
    /// Contains application-wide constants
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// Maximum file size for uploads
        /// </summary>
        public const long MaxFileSize = 5 * 1024 * 1024;

        /// <summary>
        /// Expected content type for file uploads
        /// </summary>
        public const string ExpectedContentType = "text/plain";

        /// <summary>
        /// Local storage key for API key
        /// </summary>
        public const string ApiKeyStorageKey = "gemini-api-key";

        /// <summary>
        /// Default prompt text for summarization
        /// </summary>
        public const string DefaultPromptText = "Please summarize the key points in this document";
    }
}
