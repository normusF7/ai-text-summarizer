using GenerativeAI.Core;

namespace TextSummariser.Services
{
    /// <summary>
    /// Provides text processing capabilities using generative AI models
    /// </summary>
    public interface ITextProcessingService
    {
        /// <summary>
        /// Generates a summary of the provided text content
        /// </summary>
        /// <param name="textContent">The text content to summarize</param>
        /// <param name="apiKey">The API key for the AI service</param>
        /// <returns>The generated summary text</returns>
        Task<string> GenerateSummaryAsync(string textContent, string apiKey);
    }
}
