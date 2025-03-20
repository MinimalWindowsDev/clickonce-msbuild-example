@echo off
setlocal enabledelayedexpansion

echo ===== ClickOnce Demo Build and Publish Script =====

REM Set paths - adjust if needed
set MSBUILD_PATH=MSBuild.exe

REM Build the application in Release mode
echo Building the application in Release mode...
%MSBUILD_PATH% ClickOnceDemo.csproj /t:Clean;Rebuild /p:Configuration=Release /v:minimal
if %errorlevel% neq 0 (
    echo Build failed.
    goto :error
)
echo Build successful.

REM Check if version increment is requested
set /p INCREMENT_VERSION="Do you want to increment the application version? (Y/N): "
if /i "%INCREMENT_VERSION%"=="Y" (
    echo Incrementing application version...
    
    REM Use MSBuild to increment the version
    %MSBUILD_PATH% ClickOnceDemo.csproj /t:IncrementApplicationRevision /p:Configuration=Release
    if !errorlevel! neq 0 (
        echo Failed to increment version.
        goto :error
    )
)

REM Get publish location
set PUBLISH_LOCATION=publish
set /p CUSTOM_LOCATION="Enter publish location or press Enter for default [%PUBLISH_LOCATION%]: "
if not "%CUSTOM_LOCATION%"=="" set PUBLISH_LOCATION=%CUSTOM_LOCATION%

REM Publish the application
echo Publishing the application to '%PUBLISH_LOCATION%'...
%MSBUILD_PATH% ClickOnceDemo.csproj /t:Publish /p:PublishDir=%PUBLISH_LOCATION%\ /p:Configuration=Release
if %errorlevel% neq 0 (
    echo Publish failed.
    goto :error
)

echo.
echo ===== Build and Publish Completed Successfully =====
echo.
echo The application has been published to: %CD%\%PUBLISH_LOCATION%
echo.
echo To deploy this application:
echo 1. Copy the contents of the publish folder to your web server or file share
echo 2. If using a web server, ensure the server is configured to serve .application and .manifest files
echo    with the correct MIME types:
echo    - .application: application/x-ms-application
echo    - .manifest: application/x-ms-manifest
echo 3. Users can install the application by navigating to the publish.html page
echo    or by directly opening the .application file
echo.
goto :end

:error
echo.
echo ===== Build and Publish Failed =====
echo.
exit /b 1

:end
echo Press any key to exit...
pause >nul
exit /b 0