using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Services;

namespace AlexanderDevelopment.ConfigDataMover
{
    public partial class SetConnection : Form
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
                    useFileRadiobutton.Checked = true;
                    useCrmRadiobutton.Checked = false;

                    clearCrmConnectionParams();

                    pathTextbox.Text = dict["FILE"];
                }
                else
                {
                    useFileRadiobutton.Checked = false;
                    useCrmRadiobutton.Checked = true;

                    clearFileParams();

                    if (dict.ContainsKey("SERVER"))
                        serverTextbox.Text = dict["SERVER"];

                    if (dict.ContainsKey("URL"))
                        serverTextbox.Text = dict["URL"];

                    if (dict.ContainsKey("SERVICE URI"))
                        serverTextbox.Text = dict["SERVICE URI"];

                    if (dict.ContainsKey("USERNAME"))
                        usernameTextbox.Text = dict["USERNAME"];

                    if (dict.ContainsKey("DOMAIN"))
                        domainTextbox.Text = dict["DOMAIN"];

                    if (dict.ContainsKey("PASSWORD"))
                        passwordTextbox.Text = dict["PASSWORD"];
                }
            }
            else
            {
                //starting with a blank connection, assume it's to crm
                useFileRadiobutton.Checked = false;
                useCrmRadiobutton.Checked = true;

                clearFileParams();
            }

            if (_isSource)
            {
                this.Text = "Set source";
            }
            else
            {
                this.Text = "Set target";
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setConnectionButton_Click(object sender, EventArgs e)
        {
            string connectionString = string.Empty;
            if (useFileRadiobutton.Checked)
            {
                connectionString = string.Format("file={0}", pathTextbox.Text);
            }
            else
            {
                if (validateUrl(serverTextbox.Text))
                {
                    if (!string.IsNullOrWhiteSpace(domainTextbox.Text))
                    {
                        connectionString = string.Format("url={0};username={1};domain={2};password={3};", serverTextbox.Text, usernameTextbox.Text, domainTextbox.Text, passwordTextbox.Text);
                    }
                    else
                    {
                        connectionString = string.Format("url={0};username={1};password={2};", serverTextbox.Text, usernameTextbox.Text, passwordTextbox.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Server URL is invalid.");
                    return;
                }
            }

            if (_isSource)
            {
                ((MainForm)this.Owner).SetSource(connectionString);
            }
            else
            {
                ((MainForm)this.Owner).SetTarget(connectionString);
            }
            this.Close();
        }

        private void useCrmRadiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (useCrmRadiobutton.Checked)
            {
                clearFileParams();
                enableCrmConnectionParams();
            }
            else
            {
                clearCrmConnectionParams();
                disableCrmConnectionParams();
            }
        }

        private void useFileRadiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (useFileRadiobutton.Checked)
            {
                clearCrmConnectionParams();
                disableCrmConnectionParams();
            }
            else
            {
                clearFileParams();
                enableCrmConnectionParams();
            }
        }

        void clearCrmConnectionParams()
        {
            serverTextbox.Text = string.Empty;
            usernameTextbox.Text = string.Empty;
            domainTextbox.Text = string.Empty;
            passwordTextbox.Text = string.Empty;
        }

        void clearFileParams()
        {
            pathTextbox.Text = string.Empty;
        }

        void disableCrmConnectionParams()
        {
            serverTextbox.Enabled = false;
            usernameTextbox.Enabled = false;
            domainTextbox.Enabled = false;
            passwordTextbox.Enabled = false;
            testConnectionButton.Enabled = false;
            pathTextbox.Enabled = true;
        }

        void enableCrmConnectionParams()
        {
            serverTextbox.Enabled = true;
            usernameTextbox.Enabled = true;
            domainTextbox.Enabled = true;
            passwordTextbox.Enabled = true;
            testConnectionButton.Enabled = true;
            pathTextbox.Enabled = false;
        }

        bool validateUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        private void testConnectionButton_Click(object sender, EventArgs e)
        {
            testConnectionButton.Enabled = false;
            string connectionString = string.Empty;
            if (validateUrl(serverTextbox.Text))
            {
                if (!string.IsNullOrWhiteSpace(domainTextbox.Text))
                {
                    connectionString = string.Format("url={0};username={1};domain={2};password={3};", serverTextbox.Text, usernameTextbox.Text, domainTextbox.Text, passwordTextbox.Text);
                }
                else
                {
                    connectionString = string.Format("url={0};username={1};password={2};", serverTextbox.Text, usernameTextbox.Text, passwordTextbox.Text);
                }
                try
                {
                    CrmConnection testConnection = CrmConnection.Parse(connectionString);
                    testConnection.ClientCredentials.SupportInteractive = false;
                    using (OrganizationService service = new OrganizationService(testConnection))
                    {
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
                    }
                    MessageBox.Show("Validation succeeded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Validation failed: {0}", ex.Message));
                }
            }
            else
            {
                MessageBox.Show("Server URL is invalid.");
            }
            testConnectionButton.Enabled = true;
        }
    }
}