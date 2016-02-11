namespace AlexanderDevelopment.ConfigDataMover
{
    partial class SetConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverTextbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.testConnectionButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.domainLabel = new System.Windows.Forms.Label();
            this.domainTextbox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.serverLabel = new System.Windows.Forms.Label();
            this.useCrmRadiobutton = new System.Windows.Forms.RadioButton();
            this.useFileRadiobutton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathTextbox = new System.Windows.Forms.TextBox();
            this.setConnectionButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.connectionPanel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.connectionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverTextbox
            // 
            this.serverTextbox.Location = new System.Drawing.Point(65, 25);
            this.serverTextbox.Name = "serverTextbox";
            this.serverTextbox.Size = new System.Drawing.Size(302, 20);
            this.serverTextbox.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.testConnectionButton);
            this.groupBox1.Controls.Add(this.passwordLabel);
            this.groupBox1.Controls.Add(this.passwordTextbox);
            this.groupBox1.Controls.Add(this.domainLabel);
            this.groupBox1.Controls.Add(this.domainTextbox);
            this.groupBox1.Controls.Add(this.usernameLabel);
            this.groupBox1.Controls.Add(this.usernameTextbox);
            this.groupBox1.Controls.Add(this.serverLabel);
            this.groupBox1.Controls.Add(this.serverTextbox);
            this.groupBox1.Location = new System.Drawing.Point(21, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CRM connection settings";
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(65, 129);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.Size = new System.Drawing.Size(115, 23);
            this.testConnectionButton.TabIndex = 107;
            this.testConnectionButton.Text = "Test connection";
            this.testConnectionButton.UseVisualStyleBackColor = true;
            this.testConnectionButton.Click += new System.EventHandler(this.testConnectionButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(6, 103);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Password";
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(65, 103);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(302, 20);
            this.passwordTextbox.TabIndex = 45;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(6, 77);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(43, 13);
            this.domainLabel.TabIndex = 5;
            this.domainLabel.Text = "Domain";
            // 
            // domainTextbox
            // 
            this.domainTextbox.Location = new System.Drawing.Point(65, 77);
            this.domainTextbox.Name = "domainTextbox";
            this.domainTextbox.Size = new System.Drawing.Size(302, 20);
            this.domainTextbox.TabIndex = 40;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(6, 51);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username";
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(65, 51);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(302, 20);
            this.usernameTextbox.TabIndex = 35;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(6, 25);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(38, 13);
            this.serverLabel.TabIndex = 1;
            this.serverLabel.Text = "Server";
            // 
            // useCrmRadiobutton
            // 
            this.useCrmRadiobutton.AutoSize = true;
            this.useCrmRadiobutton.Location = new System.Drawing.Point(3, 6);
            this.useCrmRadiobutton.Name = "useCrmRadiobutton";
            this.useCrmRadiobutton.Size = new System.Drawing.Size(131, 17);
            this.useCrmRadiobutton.TabIndex = 10;
            this.useCrmRadiobutton.TabStop = true;
            this.useCrmRadiobutton.Text = "Use CRM organization";
            this.useCrmRadiobutton.UseVisualStyleBackColor = true;
            this.useCrmRadiobutton.CheckedChanged += new System.EventHandler(this.useCrmRadiobutton_CheckedChanged);
            // 
            // useFileRadiobutton
            // 
            this.useFileRadiobutton.AutoSize = true;
            this.useFileRadiobutton.Location = new System.Drawing.Point(3, 29);
            this.useFileRadiobutton.Name = "useFileRadiobutton";
            this.useFileRadiobutton.Size = new System.Drawing.Size(60, 17);
            this.useFileRadiobutton.TabIndex = 20;
            this.useFileRadiobutton.TabStop = true;
            this.useFileRadiobutton.Text = "Use file";
            this.useFileRadiobutton.UseVisualStyleBackColor = true;
            this.useFileRadiobutton.CheckedChanged += new System.EventHandler(this.useFileRadiobutton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pathLabel);
            this.groupBox2.Controls.Add(this.pathTextbox);
            this.groupBox2.Location = new System.Drawing.Point(21, 245);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 62);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File settings";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(6, 25);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(47, 13);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.Text = "File path";
            // 
            // pathTextbox
            // 
            this.pathTextbox.Location = new System.Drawing.Point(65, 25);
            this.pathTextbox.Name = "pathTextbox";
            this.pathTextbox.Size = new System.Drawing.Size(302, 20);
            this.pathTextbox.TabIndex = 60;
            // 
            // setConnectionButton
            // 
            this.setConnectionButton.Location = new System.Drawing.Point(86, 313);
            this.setConnectionButton.Name = "setConnectionButton";
            this.setConnectionButton.Size = new System.Drawing.Size(115, 23);
            this.setConnectionButton.TabIndex = 100;
            this.setConnectionButton.Text = "Update connection";
            this.setConnectionButton.UseVisualStyleBackColor = true;
            this.setConnectionButton.Click += new System.EventHandler(this.setConnectionButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(207, 313);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 23);
            this.cancelButton.TabIndex = 105;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // connectionPanel
            // 
            this.connectionPanel.Controls.Add(this.useFileRadiobutton);
            this.connectionPanel.Controls.Add(this.useCrmRadiobutton);
            this.connectionPanel.Location = new System.Drawing.Point(21, 12);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(389, 49);
            this.connectionPanel.TabIndex = 106;
            // 
            // SetConnection
            // 
            this.AcceptButton = this.setConnectionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(428, 375);
            this.ControlBox = false;
            this.Controls.Add(this.connectionPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.setConnectionButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetConnection";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SetConnection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.connectionPanel.ResumeLayout(false);
            this.connectionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox serverTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.TextBox domainTextbox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.RadioButton useCrmRadiobutton;
        private System.Windows.Forms.RadioButton useFileRadiobutton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextbox;
        private System.Windows.Forms.Button setConnectionButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.Button testConnectionButton;
    }
}