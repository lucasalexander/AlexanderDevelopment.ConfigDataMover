using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace AlexanderDevelopment.ConfigDataMover
{
    public partial class CheckLatestVersion : Form
    {
        const string _latestReleaseEndpoint = "https://api.github.com/repos/lucasalexander/AlexanderDevelopment.ConfigDataMover/releases/latest";
        string _releaseurl;

        public CheckLatestVersion()
        {
            InitializeComponent();
            label1.Text = "Checking GitHub for latest release information . . . ";
            CheckGitHub();
        }

        void CheckGitHub()
        {
            try {
                WebClient client = new WebClient();
                client.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)";
                string responseString = client.DownloadString(_latestReleaseEndpoint);
                JObject releaseObject = JObject.Parse(responseString);

                string currentversion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string latestrelease = releaseObject["tag_name"].Value<string>().Replace("v", "");
                DateTime releasedate = releaseObject["published_at"].Value<DateTime>();
                _releaseurl = releaseObject["html_url"].Value<string>();

                StringBuilder versionBuilder = new StringBuilder();
                versionBuilder.AppendLine(string.Format("Your current version: {0}\n", currentversion));
                versionBuilder.AppendLine(string.Format("Latest release version: {0}", latestrelease));
                versionBuilder.AppendLine(string.Format("Latest release date: {0}\n", releasedate.ToShortDateString()));

                label1.Text = versionBuilder.ToString();

                linkLabel1.Visible = true;
            }
            catch(Exception ex)
            {
                label1.Text = string.Format("Could not get latest release information from GitHub: {0}", ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo(_releaseurl);
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
