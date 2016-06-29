namespace AlexanderDevelopment.ConfigDataMover
{
    partial class JobError
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
            this.errorLabel = new System.Windows.Forms.Label();
            this.detailsTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.copyLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(12, 21);
            this.errorLabel.MaximumSize = new System.Drawing.Size(432, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(35, 13);
            this.errorLabel.TabIndex = 0;
            this.errorLabel.Text = "label1";
            // 
            // detailsTextBox
            // 
            this.detailsTextBox.Location = new System.Drawing.Point(12, 120);
            this.detailsTextBox.Multiline = true;
            this.detailsTextBox.Name = "detailsTextBox";
            this.detailsTextBox.ReadOnly = true;
            this.detailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.detailsTextBox.Size = new System.Drawing.Size(432, 153);
            this.detailsTextBox.TabIndex = 10;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(369, 279);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 50;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(12, 104);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(62, 13);
            this.detailsLabel.TabIndex = 4;
            this.detailsLabel.Text = "Error details";
            // 
            // copyLinkLabel
            // 
            this.copyLinkLabel.AutoSize = true;
            this.copyLinkLabel.Location = new System.Drawing.Point(12, 279);
            this.copyLinkLabel.Name = "copyLinkLabel";
            this.copyLinkLabel.Size = new System.Drawing.Size(146, 13);
            this.copyLinkLabel.TabIndex = 20;
            this.copyLinkLabel.TabStop = true;
            this.copyLinkLabel.Text = "Copy error details to clipboard";
            this.copyLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.copyLinkLabel_LinkClicked);
            // 
            // JobError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 315);
            this.Controls.Add(this.copyLinkLabel);
            this.Controls.Add(this.detailsLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.detailsTextBox);
            this.Controls.Add(this.errorLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JobError";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Fatal job error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.TextBox detailsTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.LinkLabel copyLinkLabel;
    }
}