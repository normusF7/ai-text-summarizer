/**
 * Clipboard operations for the application.
 */
window.clipboardInterop = {
    /**
     * Copies text to the clipboard with improved browser compatibility
     * @param {string} text - Text to copy to clipboard
     * @returns {Promise<boolean>} - Whether the operation was successful
     */
    copyToClipboard: async function(text) {
        if (!text) {
            return false;
        }

        try {
            // Use the modern Clipboard API if available
            if (navigator.clipboard && navigator.clipboard.writeText) {
                await navigator.clipboard.writeText(text);
                return true;
            } 

            // Fallback for older browsers
            const textArea = document.createElement('textarea');
            textArea.value = text;
            textArea.style.position = 'fixed';
            textArea.style.left = '-999999px';
            textArea.style.top = '-999999px';
            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();

            const successful = document.execCommand('copy');
            document.body.removeChild(textArea);
            return successful;
        } catch (err) {
            console.error('Failed to copy text: ', err);
            return false;
        }
    }
};
