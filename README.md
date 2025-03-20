# ClickOnce MSBuild Example

A minimal working example of C# .NET Framework application with ClickOnce deployment using only MSBuild, without requiring the Visual Studio GUI.

## Features

- Pure command-line build and deployment process
- No Visual Studio GUI interaction required
- Simple Windows Forms application that demonstrates ClickOnce updates
- Batch file for building and publishing

## Requirements

- .NET Framework 4.7.2 SDK
- MSBuild (included with Visual Studio 2019 or Build Tools)

## Getting Started

1. Clone this repository
2. Run `build-and-publish.bat` to build and publish the application
3. The application will be published to the `publish` folder
4. Deploy by copying the contents of the `publish` folder to a web server or file share

## Deployment Options

- Web deployment (default)
- File share deployment (modify `InstallUrl` in .csproj)
- CD/DVD deployment (requires changes to `InstallFrom` in .csproj)

## License

This project is licensed under the WTFPL License - see the [LICENSE](LICENSE) file for details.

## Notes for Production Use

For production deployment, you should:

1. Add proper code signing with a trusted certificate
2. Update the `InstallUrl` to your actual deployment URL
3. Consider adding prerequisites and bootstrappers
