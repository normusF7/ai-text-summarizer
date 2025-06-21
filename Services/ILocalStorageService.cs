namespace TextSummariser.Services
{
    /// <summary>
    /// Provides access to browser local storage
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Gets an item from local storage
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="key">The storage key</param>
        /// <returns>The stored item or default value if not found</returns>
        Task<T?> GetItemAsync<T>(string key);

        /// <summary>
        /// Sets an item in local storage
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="key">The storage key</param>
        /// <param name="value">The value to store</param>
        Task SetItemAsync<T>(string key, T value);

        /// <summary>
        /// Removes an item from local storage
        /// </summary>
        /// <param name="key">The storage key</param>
        Task RemoveItemAsync(string key);
    }
}
