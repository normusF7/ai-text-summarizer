# Text Summarizer

A Blazor WebAssembly application that uses Generative AI to summarize text files.

## Features

- Upload and process text files (.txt format)
- Summarize text content using Google's Gemini AI
- Typewriter-style animated rendering of summary text
- Save API keys in browser local storage
- Copy summary to clipboard

## Prerequisites

- .NET 9.0 SDK or later
- A Gemini AI API key

## Getting Started

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet restore` to restore dependencies
4. Run `dotnet build` to build the project
5. Run `dotnet run` to start the application
6. Open your browser and navigate to `https://localhost:5001`

### Using the Application

1. Enter your Gemini API key in the API Configuration section
2. Upload a text file (.txt format)
3. Click the "Summarize Text" button
4. View the generated summary
5. Use the "Copy Summary" button to copy the summary to your clipboard

## Project Structure

- **Models**: Data models and DTOs
  - **Config**: Configuration-related models
  - **ProcessingStatus.cs**: Enum for processing state
  - **TextProcessingResult.cs**: Result model for text processing

- **Services**: Business logic and external integrations
  - **ITextProcessingService.cs**: Interface for text processing
  - **GeminiTextProcessingService.cs**: Gemini AI implementation
  - **ILocalStorageService.cs**: Interface for browser storage
  - **BrowserLocalStorageService.cs**: Implementation for local storage access

- **Pages**: Blazor pages
  - **TextFileProcessor.razor**: Main file processing component

## License

MIT
