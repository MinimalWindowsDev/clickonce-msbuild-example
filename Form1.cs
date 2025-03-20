using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace ClickOnceDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Display version information
            this.Text = $"ClickOnce Demo - Version {GetApplicationVersion()}";

            // Set up the label and button
            label1.Text = $"Application Version: {GetApplicationVersion()}\r\n" +
                         $"Deployment Version: {GetDeploymentVersion()}\r\n" +
                         $"Is Network Deployed: {ApplicationDeployment.IsNetworkDeployed}\r\n" +
                         $"Update Time: {DateTime.Now.ToString()}";

            button1.Text = "Check for Updates";
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    ApplicationDeployment deployment = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo info = deployment.CheckForDetailedUpdate();

                    if (info.UpdateAvailable)
                    {
                        DialogResult result = MessageBox.Show(
                            $"An update is available. Would you like to update the application now?\r\n\r\n" +
                            $"Current version: {GetApplicationVersion()}\r\n" +
                            $"Available version: {info.AvailableVersion}",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            deployment.Update();
                            MessageBox.Show("The application has been updated. Restarting now.", "Update Complete");
                            Application.Restart();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You already have the latest version.", "No Updates");
                    }
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show($"The update cannot be downloaded: {dde.Message}", "Update Error");
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show($"The application cannot be updated: {ide.Message}", "Update Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("This application is not network deployed. Updates are not available.", "Not Deployed");
            }
        }

        private string GetApplicationVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private string GetDeploymentVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            return "Not deployed";
        }
    }
}