namespace AlexanderDevelopment.ConfigDataMover
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.stepNameTextBox = new System.Windows.Forms.TextBox();
            this.stepFetchTextBox = new System.Windows.Forms.TextBox();
            this.removeStepButton = new System.Windows.Forms.Button();
            this.stepDetailGroupBox = new System.Windows.Forms.GroupBox();
            this.updateOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.stepFetchLabel = new System.Windows.Forms.Label();
            this.stepNameLabel = new System.Windows.Forms.Label();
            this.stepsGroupBox = new System.Windows.Forms.GroupBox();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.addStepButton = new System.Windows.Forms.Button();
            this.stepListBox = new System.Windows.Forms.ListBox();
            this.mapBuCheckBox = new System.Windows.Forms.CheckBox();
            this.mapCurrencyCheckBox = new System.Windows.Forms.CheckBox();
            this.guidMappingGridView = new System.Windows.Forms.DataGridView();
            this.sourceGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clearMappingsButton = new System.Windows.Forms.Button();
            this.removeMappingButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editTargetButton = new System.Windows.Forms.Button();
            this.editSourceButton = new System.Windows.Forms.Button();
            this.saveConnectionsCheckBox = new System.Windows.Forms.CheckBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.formStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRunButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepDetailGroupBox.SuspendLayout();
            this.stepsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guidMappingGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.formStatusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stepNameTextBox
            // 
            this.stepNameTextBox.Location = new System.Drawing.Point(70, 26);
            this.stepNameTextBox.Name = "stepNameTextBox";
            this.stepNameTextBox.Size = new System.Drawing.Size(198, 20);
            this.stepNameTextBox.TabIndex = 105;
            this.stepNameTextBox.Leave += new System.EventHandler(this.stepNameTextBox_Leave);
            // 
            // stepFetchTextBox
            // 
            this.stepFetchTextBox.Location = new System.Drawing.Point(9, 72);
            this.stepFetchTextBox.Multiline = true;
            this.stepFetchTextBox.Name = "stepFetchTextBox";
            this.stepFetchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.stepFetchTextBox.Size = new System.Drawing.Size(259, 179);
            this.stepFetchTextBox.TabIndex = 110;
            this.stepFetchTextBox.Leave += new System.EventHandler(this.stepFetchTextBox_Leave);
            // 
            // removeStepButton
            // 
            this.removeStepButton.Location = new System.Drawing.Point(85, 256);
            this.removeStepButton.Name = "removeStepButton";
            this.removeStepButton.Size = new System.Drawing.Size(75, 23);
            this.removeStepButton.TabIndex = 65;
            this.removeStepButton.Text = "Remove step";
            this.removeStepButton.UseVisualStyleBackColor = true;
            this.removeStepButton.Click += new System.EventHandler(this.removeStepButton_Click);
            // 
            // stepDetailGroupBox
            // 
            this.stepDetailGroupBox.Controls.Add(this.updateOnlyCheckBox);
            this.stepDetailGroupBox.Controls.Add(this.stepFetchLabel);
            this.stepDetailGroupBox.Controls.Add(this.stepNameLabel);
            this.stepDetailGroupBox.Controls.Add(this.stepNameTextBox);
            this.stepDetailGroupBox.Controls.Add(this.stepFetchTextBox);
            this.stepDetailGroupBox.Location = new System.Drawing.Point(305, 195);
            this.stepDetailGroupBox.Name = "stepDetailGroupBox";
            this.stepDetailGroupBox.Size = new System.Drawing.Size(274, 287);
            this.stepDetailGroupBox.TabIndex = 100;
            this.stepDetailGroupBox.TabStop = false;
            this.stepDetailGroupBox.Text = "Step details";
            // 
            // updateOnlyCheckBox
            // 
            this.updateOnlyCheckBox.AutoSize = true;
            this.updateOnlyCheckBox.Location = new System.Drawing.Point(9, 260);
            this.updateOnlyCheckBox.Name = "updateOnlyCheckBox";
            this.updateOnlyCheckBox.Size = new System.Drawing.Size(112, 17);
            this.updateOnlyCheckBox.TabIndex = 111;
            this.updateOnlyCheckBox.Text = "Update-only step?";
            this.updateOnlyCheckBox.UseVisualStyleBackColor = true;
            this.updateOnlyCheckBox.CheckedChanged += new System.EventHandler(this.updateOnlyCheckBox_CheckedChanged);
            // 
            // stepFetchLabel
            // 
            this.stepFetchLabel.AutoSize = true;
            this.stepFetchLabel.Location = new System.Drawing.Point(6, 56);
            this.stepFetchLabel.Name = "stepFetchLabel";
            this.stepFetchLabel.Size = new System.Drawing.Size(110, 13);
            this.stepFetchLabel.TabIndex = 7;
            this.stepFetchLabel.Text = "Step FetchXML query";
            // 
            // stepNameLabel
            // 
            this.stepNameLabel.AutoSize = true;
            this.stepNameLabel.Location = new System.Drawing.Point(6, 29);
            this.stepNameLabel.Name = "stepNameLabel";
            this.stepNameLabel.Size = new System.Drawing.Size(58, 13);
            this.stepNameLabel.TabIndex = 6;
            this.stepNameLabel.Text = "Step name";
            // 
            // stepsGroupBox
            // 
            this.stepsGroupBox.Controls.Add(this.clearAllButton);
            this.stepsGroupBox.Controls.Add(this.moveDownButton);
            this.stepsGroupBox.Controls.Add(this.moveUpButton);
            this.stepsGroupBox.Controls.Add(this.addStepButton);
            this.stepsGroupBox.Controls.Add(this.removeStepButton);
            this.stepsGroupBox.Controls.Add(this.stepListBox);
            this.stepsGroupBox.Location = new System.Drawing.Point(12, 195);
            this.stepsGroupBox.Name = "stepsGroupBox";
            this.stepsGroupBox.Size = new System.Drawing.Size(281, 287);
            this.stepsGroupBox.TabIndex = 50;
            this.stepsGroupBox.TabStop = false;
            this.stepsGroupBox.Text = "Job steps";
            // 
            // clearAllButton
            // 
            this.clearAllButton.Location = new System.Drawing.Point(162, 256);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(75, 23);
            this.clearAllButton.TabIndex = 80;
            this.clearAllButton.Text = "Clear steps";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(197, 56);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 23);
            this.moveDownButton.TabIndex = 75;
            this.moveDownButton.Text = "Move down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(197, 27);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 23);
            this.moveUpButton.TabIndex = 70;
            this.moveUpButton.Text = "Move up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // addStepButton
            // 
            this.addStepButton.Location = new System.Drawing.Point(8, 256);
            this.addStepButton.Name = "addStepButton";
            this.addStepButton.Size = new System.Drawing.Size(75, 23);
            this.addStepButton.TabIndex = 60;
            this.addStepButton.Text = "Add step";
            this.addStepButton.UseVisualStyleBackColor = true;
            this.addStepButton.Click += new System.EventHandler(this.addStepButton_Click);
            // 
            // stepListBox
            // 
            this.stepListBox.FormattingEnabled = true;
            this.stepListBox.Location = new System.Drawing.Point(8, 26);
            this.stepListBox.Name = "stepListBox";
            this.stepListBox.Size = new System.Drawing.Size(182, 225);
            this.stepListBox.TabIndex = 55;
            this.stepListBox.SelectedIndexChanged += new System.EventHandler(this.stepListBox_SelectedIndexChanged);
            // 
            // mapBuCheckBox
            // 
            this.mapBuCheckBox.AutoSize = true;
            this.mapBuCheckBox.Location = new System.Drawing.Point(9, 98);
            this.mapBuCheckBox.Name = "mapBuCheckBox";
            this.mapBuCheckBox.Size = new System.Drawing.Size(168, 17);
            this.mapBuCheckBox.TabIndex = 25;
            this.mapBuCheckBox.Text = "Map root business unit GUID?";
            this.mapBuCheckBox.UseVisualStyleBackColor = true;
            // 
            // mapCurrencyCheckBox
            // 
            this.mapCurrencyCheckBox.AutoSize = true;
            this.mapCurrencyCheckBox.Location = new System.Drawing.Point(9, 121);
            this.mapCurrencyCheckBox.Name = "mapCurrencyCheckBox";
            this.mapCurrencyCheckBox.Size = new System.Drawing.Size(153, 17);
            this.mapCurrencyCheckBox.TabIndex = 30;
            this.mapCurrencyCheckBox.Text = "Map base currency GUID?";
            this.mapCurrencyCheckBox.UseVisualStyleBackColor = true;
            // 
            // guidMappingGridView
            // 
            this.guidMappingGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guidMappingGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sourceGuid,
            this.targetGuid});
            this.guidMappingGridView.Location = new System.Drawing.Point(6, 23);
            this.guidMappingGridView.Name = "guidMappingGridView";
            this.guidMappingGridView.Size = new System.Drawing.Size(555, 136);
            this.guidMappingGridView.TabIndex = 155;
            this.guidMappingGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.guidMappingGridView_CellEndEdit);
            this.guidMappingGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.guidMappingGridView_CellValidating);
            this.guidMappingGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.guidMappingGridView_KeyDown);
            // 
            // sourceGuid
            // 
            this.sourceGuid.HeaderText = "Source GUID";
            this.sourceGuid.Name = "sourceGuid";
            this.sourceGuid.Width = 250;
            // 
            // targetGuid
            // 
            this.targetGuid.HeaderText = "Target GUID";
            this.targetGuid.Name = "targetGuid";
            this.targetGuid.Width = 250;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearMappingsButton);
            this.groupBox1.Controls.Add(this.removeMappingButton);
            this.groupBox1.Controls.Add(this.guidMappingGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 488);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 203);
            this.groupBox1.TabIndex = 150;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GUID mappings";
            // 
            // clearMappingsButton
            // 
            this.clearMappingsButton.Location = new System.Drawing.Point(120, 165);
            this.clearMappingsButton.Name = "clearMappingsButton";
            this.clearMappingsButton.Size = new System.Drawing.Size(103, 23);
            this.clearMappingsButton.TabIndex = 165;
            this.clearMappingsButton.Text = "Clear mappings";
            this.clearMappingsButton.UseVisualStyleBackColor = true;
            this.clearMappingsButton.Click += new System.EventHandler(this.clearMappingsButton_Click);
            // 
            // removeMappingButton
            // 
            this.removeMappingButton.Location = new System.Drawing.Point(8, 165);
            this.removeMappingButton.Name = "removeMappingButton";
            this.removeMappingButton.Size = new System.Drawing.Size(106, 23);
            this.removeMappingButton.TabIndex = 160;
            this.removeMappingButton.Text = "Remove mapping";
            this.removeMappingButton.UseVisualStyleBackColor = true;
            this.removeMappingButton.Click += new System.EventHandler(this.removeMappingButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.editTargetButton);
            this.groupBox2.Controls.Add(this.editSourceButton);
            this.groupBox2.Controls.Add(this.saveConnectionsCheckBox);
            this.groupBox2.Controls.Add(this.mapBuCheckBox);
            this.groupBox2.Controls.Add(this.targetLabel);
            this.groupBox2.Controls.Add(this.sourceLabel);
            this.groupBox2.Controls.Add(this.mapCurrencyCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 161);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration data job details";
            // 
            // editTargetButton
            // 
            this.editTargetButton.Location = new System.Drawing.Point(9, 46);
            this.editTargetButton.Name = "editTargetButton";
            this.editTargetButton.Size = new System.Drawing.Size(105, 23);
            this.editTargetButton.TabIndex = 19;
            this.editTargetButton.Text = "Select target";
            this.editTargetButton.UseVisualStyleBackColor = true;
            this.editTargetButton.Click += new System.EventHandler(this.editTargetButton_Click);
            // 
            // editSourceButton
            // 
            this.editSourceButton.Location = new System.Drawing.Point(9, 15);
            this.editSourceButton.Name = "editSourceButton";
            this.editSourceButton.Size = new System.Drawing.Size(105, 23);
            this.editSourceButton.TabIndex = 16;
            this.editSourceButton.Text = "Select source";
            this.editSourceButton.UseVisualStyleBackColor = true;
            this.editSourceButton.Click += new System.EventHandler(this.editSourceButton_Click);
            // 
            // saveConnectionsCheckBox
            // 
            this.saveConnectionsCheckBox.AutoSize = true;
            this.saveConnectionsCheckBox.Location = new System.Drawing.Point(9, 75);
            this.saveConnectionsCheckBox.Name = "saveConnectionsCheckBox";
            this.saveConnectionsCheckBox.Size = new System.Drawing.Size(146, 17);
            this.saveConnectionsCheckBox.TabIndex = 20;
            this.saveConnectionsCheckBox.Text = "Save connection details?";
            this.saveConnectionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(124, 46);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(96, 13);
            this.targetLabel.TabIndex = 154;
            this.targetLabel.Text = "No target specified";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(124, 20);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(101, 13);
            this.sourceLabel.TabIndex = 153;
            this.sourceLabel.Text = "No source specified";
            // 
            // formStatusStrip
            // 
            this.formStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.formStatusStrip.Location = new System.Drawing.Point(0, 724);
            this.formStatusStrip.Name = "formStatusStrip";
            this.formStatusStrip.Size = new System.Drawing.Size(590, 22);
            this.formStatusStrip.TabIndex = 151;
            this.formStatusStrip.Text = "Status";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileButton,
            this.toolStripSeparator2,
            this.toolStripRunButton,
            this.toolStripSeparator1,
            this.helpButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(590, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileButton
            // 
            this.fileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadJobToolStripMenuItem,
            this.saveJobToolStripMenuItem});
            this.fileButton.Image = ((System.Drawing.Image)(resources.GetObject("fileButton.Image")));
            this.fileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(38, 22);
            this.fileButton.Text = "File";
            this.fileButton.ToolTipText = "File";
            // 
            // loadJobToolStripMenuItem
            // 
            this.loadJobToolStripMenuItem.Name = "loadJobToolStripMenuItem";
            this.loadJobToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.loadJobToolStripMenuItem.Text = "Load job";
            this.loadJobToolStripMenuItem.Click += new System.EventHandler(this.loadJobButton_Click);
            // 
            // saveJobToolStripMenuItem
            // 
            this.saveJobToolStripMenuItem.Name = "saveJobToolStripMenuItem";
            this.saveJobToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.saveJobToolStripMenuItem.Text = "Save job";
            this.saveJobToolStripMenuItem.Click += new System.EventHandler(this.saveJobButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripRunButton
            // 
            this.toolStripRunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripRunButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRunButton.Image")));
            this.toolStripRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRunButton.Name = "toolStripRunButton";
            this.toolStripRunButton.Size = new System.Drawing.Size(52, 22);
            this.toolStripRunButton.Text = "Run job";
            this.toolStripRunButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpButton
            // 
            this.helpButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpButton.Image = ((System.Drawing.Image)(resources.GetObject("helpButton.Image")));
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(61, 22);
            this.helpButton.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesButton_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.toolStripAboutButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(590, 746);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.formStatusStrip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stepsGroupBox);
            this.Controls.Add(this.stepDetailGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Dynamics CRM Configuration Data Mover";
            this.stepDetailGroupBox.ResumeLayout(false);
            this.stepDetailGroupBox.PerformLayout();
            this.stepsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guidMappingGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.formStatusStrip.ResumeLayout(false);
            this.formStatusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox stepNameTextBox;
        private System.Windows.Forms.TextBox stepFetchTextBox;
        private System.Windows.Forms.Button removeStepButton;
        private System.Windows.Forms.GroupBox stepDetailGroupBox;
        private System.Windows.Forms.Label stepFetchLabel;
        private System.Windows.Forms.Label stepNameLabel;
        private System.Windows.Forms.GroupBox stepsGroupBox;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button addStepButton;
        private System.Windows.Forms.ListBox stepListBox;
        private System.Windows.Forms.CheckBox mapBuCheckBox;
        private System.Windows.Forms.CheckBox mapCurrencyCheckBox;
        private System.Windows.Forms.DataGridView guidMappingGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeMappingButton;
        private System.Windows.Forms.Button clearMappingsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetGuid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.CheckBox updateOnlyCheckBox;
        private System.Windows.Forms.CheckBox saveConnectionsCheckBox;
        private System.Windows.Forms.StatusStrip formStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripRunButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button editTargetButton;
        private System.Windows.Forms.Button editSourceButton;
        private System.Windows.Forms.ToolStripDropDownButton fileButton;
        private System.Windows.Forms.ToolStripMenuItem loadJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton helpButton;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

