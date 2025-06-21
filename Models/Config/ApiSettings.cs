namespace TextSummariser.Models.Config
{
    /// <summary>
    /// Provides configuration settings for external APIs
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// The API key for Gemini AI service
        /// </summary>
        public string? GeminiApiKey { get; set; }

        /// <summary>
        /// Whether to store the API key in browser storage
        /// </summary>
        public bool RememberApiKey { get; set; }
    }
}
