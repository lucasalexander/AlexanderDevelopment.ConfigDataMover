using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexanderDevelopment.ConfigDataMover
{
    public partial class JobError : Form
    {
        public JobError()
        {
            InitializeComponent();
        }

        public void SetDetails(string label, string details)
        {
            errorLabel.Text = label;
            detailsTextBox.Text = details;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(detailsTextBox.Text);
        }
    }
}
