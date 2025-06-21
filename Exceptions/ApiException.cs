namespace TextSummariser.Exceptions
{
    /// <summary>
    /// Exception thrown when there are issues with external API operations
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Creates a new instance of ApiException
        /// </summary>
        public ApiException() : base("An error occurred while communicating with the API")
        {
        }

        /// <summary>
        /// Creates a new instance of ApiException with a specific message
        /// </summary>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of ApiException with a specific message and inner exception
        /// </summary>
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// The HTTP status code if available
        /// </summary>
        public int? StatusCode { get; set; }
    }
}
