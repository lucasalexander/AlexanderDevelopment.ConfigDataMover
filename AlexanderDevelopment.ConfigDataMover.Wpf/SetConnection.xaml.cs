// --------------------------------------------------------------------------------------------------------------------
// SetConnection.xaml.cs
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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace AlexanderDevelopment.ConfigDataMover.Wpf
{
    /// <summary>
    /// Interaction logic for SetConnection.xaml
    /// </summary>
    public partial class SetConnection : Window
    {
        private bool _isSource;

        public SetConnection()
        {
            InitializeComponent();
        }

        public SetConnection(string connectionstring, bool isSource)
        {
            InitializeComponent();

            _isSource = isSource;
            if (!string.IsNullOrWhiteSpace(connectionstring))
            {
                var dict = Utility.ParseConnectionString(connectionstring);

                if (dict.ContainsKey("FILE"))
                {
                    useFileRadioButton.IsChecked = true;
                    useOauthRadioButton.IsChecked = false;

                    clearOAuthParams();

                    pathTextBox.Text = dict["FILE"];
                }
                else
                {
                    useFileRadioButton.IsChecked = false;
                    useOauthRadioButton.IsChecked = true;

                    clearFileParams();
                    oauthTextBox.Text = connectionstring;

                }
            }
            else
            {
                //starting with a blank connection, assume it's to crm
                useFileRadioButton.IsChecked = false;
                useOauthRadioButton.IsChecked = true;

                clearFileParams();
            }

            if (_isSource)
            {
                this.Title = "Set source";
            }
            else
            {
                this.Title = "Set target";
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setConnectionButton_Click(object sender, EventArgs e)
        {
            string connectionString = string.Empty;
            if (useFileRadioButton.IsChecked.HasValue ? useFileRadioButton.IsChecked.Value : false)
            {
                connectionString = string.Format("file={0}", pathTextBox.Text);
            }
            else
            {
                connectionString = oauthTextBox.Text;
                /*if (string.IsNullOrWhiteSpace(oauthTextBox.Text))
                {
                    MessageBox.Show("Must specify connection string.");
                    return;
                }*/
            }

            if (_isSource)
            {
                ((MainWindow)Application.Current.MainWindow).SetSource(connectionString);
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).SetTarget(connectionString);
            }
            this.Close();
        }

        private void enableCorrectParams()
        {
            clearFileParams();
            clearOAuthParams();

            if (useOauthRadioButton.IsChecked.HasValue ? useOauthRadioButton.IsChecked.Value : false)
            {
                oauthTextBox.IsReadOnly = false;
            }
            else
            {
                oauthTextBox.IsReadOnly = true;
            }
            if (useFileRadioButton.IsChecked.HasValue ? useFileRadioButton.IsChecked.Value : false)
            {
                pathTextBox.IsReadOnly = false;
                selectExistingButton.IsEnabled = true;
            }
            else
            {
                pathTextBox.IsReadOnly = true;
                selectExistingButton.IsEnabled = false;
            }
        }

        private void useCrmRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            enableCorrectParams();
        }

        private void useOauthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            enableCorrectParams();
        }

        private void useFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            enableCorrectParams();
        }

        void clearFileParams()
        {
            pathTextBox.Text = string.Empty;
        }
        void clearOAuthParams()
        {
            oauthTextBox.Text = string.Empty;
        }

        bool validateUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        private void testOauthConnectionButton_Click(object sender, EventArgs e)
        {
            testOauthConnectionButton.IsEnabled = false;
            string connectionString = string.Empty;
            if (string.IsNullOrWhiteSpace(oauthTextBox.Text))
            {
                MessageBox.Show("Must specify connection string.");
                return;
            }
            try
            {
                connectionString = oauthTextBox.Text;
                Microsoft.Xrm.Tooling.Connector.TraceControlSettings.TraceLevel = System.Diagnostics.SourceLevels.All;
                Microsoft.Xrm.Tooling.Connector.TraceControlSettings.AddTraceListener(new System.Diagnostics.TextWriterTraceListener("ConnectionTest.log"));

                IOrganizationService service;

                CrmServiceClient testConnection = new CrmServiceClient(connectionString);
                service = (IOrganizationService)testConnection.OrganizationWebProxyClient != null ? (IOrganizationService)testConnection.OrganizationWebProxyClient : (IOrganizationService)testConnection.OrganizationServiceProxy;


                string testFetch = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='businessunit'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                          </entity>
                        </fetch>";
                EntityCollection buEntities = service.RetrieveMultiple(new FetchExpression(testFetch));
                if (buEntities.Entities.Count < 1)
                {
                    throw new Exception("Could not retrieve results from test query.");
                }

                MessageBox.Show("Validation succeeded");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Validation failed: {0}", ex.Message));
            }
            testOauthConnectionButton.IsEnabled = true;
        }

        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json|TXT Files (*.txt)|*.txt";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                pathTextBox.Text = filename;
            }
        }
    }
}

