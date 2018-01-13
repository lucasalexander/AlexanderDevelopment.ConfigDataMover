// --------------------------------------------------------------------------------------------------------------------
// CheckLatestVersion.xaml.cs
//
// Copyright 2015-2018 Lucas Alexander
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows.Navigation;

namespace AlexanderDevelopment.ConfigDataMover.Wpf
{
    /// <summary>
    /// Interaction logic for CheckLatestVersion.xaml
    /// </summary>
    public partial class CheckLatestVersion : Window
    {
        const string _latestReleaseEndpoint = "https://api.github.com/repos/lucasalexander/AlexanderDevelopment.ConfigDataMover/releases/latest";
        string _releaseurl;

        public CheckLatestVersion()
        {
            InitializeComponent();
            label1.Content = "Checking GitHub for latest release information . . . ";
            CheckGitHub();
        }

        void CheckGitHub()
        {
            try
            {
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

                label1.Content = versionBuilder.ToString();
                //Run run3 = new Run("Link Text.");

                //Hyperlink hyperl = new Hyperlink(run3);
                //hyperl.NavigateUri = new Uri("http://search.msn.com");
                //hyperl.RequestNavigate += Hyperlink_RequestNavigate;

                //label1.Inlines.Add(versionBuilder.ToString());
                //label1.Inlines.Add(run3);
                ////linkLabel1.Visible = true;
            }
            catch (Exception ex)
            {
                label1.Content = string.Format("Could not get latest release information from GitHub: {0}", ex.Message);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
