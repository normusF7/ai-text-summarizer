# AI Text Summarizer

A Blazor WebAssembly application that leverages Google's Gemini AI to provide intelligent summarization of text files.

## Features

- Upload and process text files (.txt format)
- Generate concise summaries using Google's Gemini AI
- Save API keys in browser local storage
- Copy generated summaries to clipboard with one click
- Clean, responsive user interface

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
  - **SummaryState.cs**: Core state management for the summarization process

- **Services**: Business logic and external integrations
  - **ITextProcessingService.cs**: Interface for text processing
  - **GeminiTextProcessingService.cs**: Gemini AI implementation
  - **ILocalStorageService.cs**: Interface for browser storage
  - **BrowserLocalStorageService.cs**: Implementation for local storage access

- **Pages**: Blazor pages
  - **TextFileProcessor.razor**: Main file processing component

- **Layout**: Application layout components
  - **MainLayout.razor**: Main application layout

## License

MIT
