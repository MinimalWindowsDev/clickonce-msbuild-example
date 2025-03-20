# Uninstall Button Experiment

This branch contains an experimental attempt to add an uninstall button to the application.

## What was attempted

We tried to add a button that would open the Windows Control Panel (appwiz.cpl) to guide users to uninstall the application. The implementation included:

1. Adding a new button to the form
2. When clicked, confirming the user's intention to uninstall
3. Opening the Windows Programs and Features control panel
4. Providing instructions for the user to complete the uninstall

## Why it didn't work

The approach faced several limitations:

1. The ApplicationDeployment class doesn't provide a direct method to uninstall a ClickOnce application programmatically
2. Our initial attempt mistakenly assumed there was an `Uninstall()` method in the ApplicationDeployment class
3. The fallback approach of launching the control panel works but requires manual user intervention
4. This creates a less seamless experience than a true one-click uninstall

## Standard ClickOnce uninstall process

ClickOnce applications are designed to be uninstalled through the standard Windows application management:

1. Control Panel > Programs > Programs and Features
2. Or in newer Windows versions: Settings > Apps > Apps & features
3. Find the application in the list (by the product name specified in the .csproj file)
4. Select it and click "Uninstall"

## Future possibilities

For a more automated uninstall experience, these approaches could be explored:

- Using Windows Management Instrumentation (WMI) to programmatically uninstall the application
- Creating a separate uninstaller utility that handles the uninstallation process
- Using MSI packaging alongside ClickOnce for more control over the installation/uninstallation process
- Registering a custom uninstall command during deployment

## Conclusion

For this minimal working example, we'll stick with the standard ClickOnce uninstall mechanism through Windows' Programs and Features. This approach aligns with ClickOnce's design philosophy of integrating with Windows' built-in application management.
