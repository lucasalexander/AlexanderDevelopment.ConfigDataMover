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
            this.authtypeComboBox = new System.Windows.Forms.ComboBox();
            this.authtypeLabel = new System.Windows.Forms.Label();
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.connectionPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverTextbox
            // 
            this.serverTextbox.Location = new System.Drawing.Point(131, 48);
            this.serverTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.serverTextbox.Name = "serverTextbox";
            this.serverTextbox.Size = new System.Drawing.Size(600, 30);
            this.serverTextbox.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.authtypeComboBox);
            this.groupBox1.Controls.Add(this.authtypeLabel);
            this.groupBox1.Controls.Add(this.testConnectionButton);
            this.groupBox1.Controls.Add(this.passwordLabel);
            this.groupBox1.Controls.Add(this.passwordTextbox);
            this.groupBox1.Controls.Add(this.domainLabel);
            this.groupBox1.Controls.Add(this.domainTextbox);
            this.groupBox1.Controls.Add(this.usernameLabel);
            this.groupBox1.Controls.Add(this.usernameTextbox);
            this.groupBox1.Controls.Add(this.serverLabel);
            this.groupBox1.Controls.Add(this.serverTextbox);
            this.groupBox1.Location = new System.Drawing.Point(5, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(779, 371);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CRM connection settings";
            // 
            // authtypeComboBox
            // 
            this.authtypeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.authtypeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.authtypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.authtypeComboBox.FormattingEnabled = true;
            this.authtypeComboBox.Items.AddRange(new object[] {
            "AD",
            "IFD",
            "Office365"});
            this.authtypeComboBox.Location = new System.Drawing.Point(131, 246);
            this.authtypeComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.authtypeComboBox.Name = "authtypeComboBox";
            this.authtypeComboBox.Size = new System.Drawing.Size(600, 33);
            this.authtypeComboBox.TabIndex = 46;
            // 
            // authtypeLabel
            // 
            this.authtypeLabel.AutoSize = true;
            this.authtypeLabel.Location = new System.Drawing.Point(11, 246);
            this.authtypeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.authtypeLabel.Name = "authtypeLabel";
            this.authtypeLabel.Size = new System.Drawing.Size(95, 25);
            this.authtypeLabel.TabIndex = 108;
            this.authtypeLabel.Text = "Auth type";
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(131, 304);
            this.testConnectionButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.Size = new System.Drawing.Size(229, 44);
            this.testConnectionButton.TabIndex = 107;
            this.testConnectionButton.Text = "Test connection";
            this.testConnectionButton.UseVisualStyleBackColor = true;
            this.testConnectionButton.Click += new System.EventHandler(this.testConnectionButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 198);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(98, 25);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Password";
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(131, 198);
            this.passwordTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(600, 30);
            this.passwordTextbox.TabIndex = 45;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(12, 148);
            this.domainLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(79, 25);
            this.domainLabel.TabIndex = 5;
            this.domainLabel.Text = "Domain";
            // 
            // domainTextbox
            // 
            this.domainTextbox.Location = new System.Drawing.Point(131, 148);
            this.domainTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.domainTextbox.Name = "domainTextbox";
            this.domainTextbox.Size = new System.Drawing.Size(600, 30);
            this.domainTextbox.TabIndex = 40;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(12, 98);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(102, 25);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username";
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(131, 98);
            this.usernameTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(600, 30);
            this.usernameTextbox.TabIndex = 35;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(12, 48);
            this.serverLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(70, 25);
            this.serverLabel.TabIndex = 1;
            this.serverLabel.Text = "Server";
            // 
            // useCrmRadiobutton
            // 
            this.useCrmRadiobutton.AutoSize = true;
            this.useCrmRadiobutton.Location = new System.Drawing.Point(5, 11);
            this.useCrmRadiobutton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.useCrmRadiobutton.Name = "useCrmRadiobutton";
            this.useCrmRadiobutton.Size = new System.Drawing.Size(233, 29);
            this.useCrmRadiobutton.TabIndex = 10;
            this.useCrmRadiobutton.TabStop = true;
            this.useCrmRadiobutton.Text = "Use CRM organization";
            this.useCrmRadiobutton.UseVisualStyleBackColor = true;
            this.useCrmRadiobutton.CheckedChanged += new System.EventHandler(this.useCrmRadiobutton_CheckedChanged);
            // 
            // useFileRadiobutton
            // 
            this.useFileRadiobutton.AutoSize = true;
            this.useFileRadiobutton.Location = new System.Drawing.Point(5, 56);
            this.useFileRadiobutton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.useFileRadiobutton.Name = "useFileRadiobutton";
            this.useFileRadiobutton.Size = new System.Drawing.Size(101, 29);
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
            this.groupBox2.Location = new System.Drawing.Point(5, 495);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(779, 119);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File settings";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 48);
            this.pathLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(86, 25);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.Text = "File path";
            // 
            // pathTextbox
            // 
            this.pathTextbox.Location = new System.Drawing.Point(131, 48);
            this.pathTextbox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pathTextbox.Name = "pathTextbox";
            this.pathTextbox.Size = new System.Drawing.Size(600, 30);
            this.pathTextbox.TabIndex = 60;
            // 
            // setConnectionButton
            // 
            this.setConnectionButton.Location = new System.Drawing.Point(5, 626);
            this.setConnectionButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.setConnectionButton.Name = "setConnectionButton";
            this.setConnectionButton.Size = new System.Drawing.Size(229, 44);
            this.setConnectionButton.TabIndex = 100;
            this.setConnectionButton.Text = "Update connection";
            this.setConnectionButton.UseVisualStyleBackColor = true;
            this.setConnectionButton.Click += new System.EventHandler(this.setConnectionButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(244, 626);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(229, 44);
            this.cancelButton.TabIndex = 105;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // connectionPanel
            // 
            this.connectionPanel.Controls.Add(this.useFileRadiobutton);
            this.connectionPanel.Controls.Add(this.useCrmRadiobutton);
            this.connectionPanel.Location = new System.Drawing.Point(5, 6);
            this.connectionPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(779, 94);
            this.connectionPanel.TabIndex = 106;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.connectionPanel);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.setConnectionButton);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 15);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(840, 698);
            this.flowLayoutPanel1.TabIndex = 107;
            // 
            // SetConnection
            // 
            this.AcceptButton = this.setConnectionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(868, 728);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
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
            this.flowLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox authtypeComboBox;
        private System.Windows.Forms.Label authtypeLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}