using GenerativeAI;
using GenerativeAI.Core;
using TextSummariser.Models.Config;

namespace TextSummariser.Services
{
    /// <summary>
    /// Implementation of ITextProcessingService using Google's Gemini model
    /// </summary>
    public class GeminiTextProcessingService : ITextProcessingService
    {
        /// <summary>
        /// Generates a summary of the provided text content using Gemini AI
        /// </summary>
        /// <param name="textContent">The text content to summarize</param>
        /// <param name="apiKey">The Gemini API key</param>
        /// <returns>The generated summary text</returns>
        /// <exception cref="ArgumentException">Thrown when API key is null or empty</exception>
        public async Task<string> GenerateSummaryAsync(string textContent, string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Gemini API key is not configured", nameof(apiKey));
            }

            var gemini = new GeminiModel(
                apiKey: apiKey,
                modelParams: new ModelParams() { Model = "gemini-1.5-pro" }
            );

            var fullPrompt = $"{AppConstants.DefaultPromptText}\n\nDocument content:\n{textContent}";
            var response = await gemini.GenerateContentAsync(fullPrompt);

            return response.Text ?? "No response generated.";
        }
    }
}
