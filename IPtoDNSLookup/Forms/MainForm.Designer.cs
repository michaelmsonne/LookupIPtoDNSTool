namespace LookupsIPsToDNS.Forms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.groupBoxIPs = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValidIPCount = new System.Windows.Forms.Label();
            this.cleanupIPListButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.lookupButton = new System.Windows.Forms.Button();
            this.backgroundWorkerLookup = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExporttoCSV = new System.Windows.Forms.Button();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCleanOutput = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripTop = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIPsFromcsvFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveIPsTocsvFileFromIPListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonGetCurrentConnectedIPs = new System.Windows.Forms.Button();
            this.groupBoxFormActions = new System.Windows.Forms.GroupBox();
            this.groupBoxIPs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStripTop.SuspendLayout();
            this.groupBoxFormActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressTextBox.Location = new System.Drawing.Point(6, 19);
            this.ipAddressTextBox.Multiline = true;
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ipAddressTextBox.Size = new System.Drawing.Size(198, 609);
            this.ipAddressTextBox.TabIndex = 0;
            this.ipAddressTextBox.TextChanged += new System.EventHandler(this.ipAddressTextBox_TextChanged);
            // 
            // groupBoxIPs
            // 
            this.groupBoxIPs.Controls.Add(this.label1);
            this.groupBoxIPs.Controls.Add(this.lblValidIPCount);
            this.groupBoxIPs.Controls.Add(this.ipAddressTextBox);
            this.groupBoxIPs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxIPs.Location = new System.Drawing.Point(12, 32);
            this.groupBoxIPs.Name = "groupBoxIPs";
            this.groupBoxIPs.Size = new System.Drawing.Size(212, 649);
            this.groupBoxIPs.TabIndex = 1;
            this.groupBoxIPs.TabStop = false;
            this.groupBoxIPs.Text = "IP´s to get DNS information about";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 631);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Valid IP´s in list:";
            // 
            // lblValidIPCount
            // 
            this.lblValidIPCount.AutoSize = true;
            this.lblValidIPCount.Location = new System.Drawing.Point(81, 631);
            this.lblValidIPCount.Name = "lblValidIPCount";
            this.lblValidIPCount.Size = new System.Drawing.Size(14, 13);
            this.lblValidIPCount.TabIndex = 6;
            this.lblValidIPCount.Text = "0";
            // 
            // cleanupIPListButton
            // 
            this.cleanupIPListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cleanupIPListButton.Location = new System.Drawing.Point(8, 102);
            this.cleanupIPListButton.Name = "cleanupIPListButton";
            this.cleanupIPListButton.Size = new System.Drawing.Size(117, 23);
            this.cleanupIPListButton.TabIndex = 4;
            this.cleanupIPListButton.Text = "Clean IP list to lookup";
            this.cleanupIPListButton.UseVisualStyleBackColor = true;
            this.cleanupIPListButton.Click += new System.EventHandler(this.cleanupIPListButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(8, 73);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(198, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel lookup of the IP list";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // lookupButton
            // 
            this.lookupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupButton.Location = new System.Drawing.Point(8, 44);
            this.lookupButton.Name = "lookupButton";
            this.lookupButton.Size = new System.Drawing.Size(198, 23);
            this.lookupButton.TabIndex = 2;
            this.lookupButton.Text = "Lookup IP´s in list";
            this.lookupButton.UseVisualStyleBackColor = true;
            this.lookupButton.Click += new System.EventHandler(this.buttonLookup_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonExporttoCSV);
            this.groupBox1.Controls.Add(this.resultDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(230, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 787);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DNS information lookedup from IP´s";
            // 
            // buttonExporttoCSV
            // 
            this.buttonExporttoCSV.Enabled = false;
            this.buttonExporttoCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExporttoCSV.Location = new System.Drawing.Point(411, 757);
            this.buttonExporttoCSV.Name = "buttonExporttoCSV";
            this.buttonExporttoCSV.Size = new System.Drawing.Size(141, 23);
            this.buttonExporttoCSV.TabIndex = 2;
            this.buttonExporttoCSV.Text = "Export lookup data to .csv";
            this.buttonExporttoCSV.UseVisualStyleBackColor = true;
            this.buttonExporttoCSV.Click += new System.EventHandler(this.buttonExporttoCSV_Click);
            // 
            // resultDataGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.resultDataGridView.Location = new System.Drawing.Point(7, 18);
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.resultDataGridView.Size = new System.Drawing.Size(545, 733);
            this.resultDataGridView.TabIndex = 0;
            // 
            // buttonCleanOutput
            // 
            this.buttonCleanOutput.Enabled = false;
            this.buttonCleanOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCleanOutput.Location = new System.Drawing.Point(131, 102);
            this.buttonCleanOutput.Name = "buttonCleanOutput";
            this.buttonCleanOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonCleanOutput.TabIndex = 1;
            this.buttonCleanOutput.Text = "Clean output";
            this.buttonCleanOutput.UseVisualStyleBackColor = true;
            this.buttonCleanOutput.Click += new System.EventHandler(this.buttonCleanOutput_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 818);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // menuStripTop
            // 
            this.menuStripTop.BackColor = System.Drawing.Color.White;
            this.menuStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStripTop.Location = new System.Drawing.Point(0, 0);
            this.menuStripTop.Name = "menuStripTop";
            this.menuStripTop.Size = new System.Drawing.Size(800, 24);
            this.menuStripTop.TabIndex = 5;
            this.menuStripTop.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadIPsFromcsvFileToolStripMenuItem,
            this.saveIPsTocsvFileFromIPListToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadIPsFromcsvFileToolStripMenuItem
            // 
            this.loadIPsFromcsvFileToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.loadIPsFromcsvFileToolStripMenuItem.Name = "loadIPsFromcsvFileToolStripMenuItem";
            this.loadIPsFromcsvFileToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.loadIPsFromcsvFileToolStripMenuItem.Text = "Load IPs from .csv file to lookup";
            this.loadIPsFromcsvFileToolStripMenuItem.Click += new System.EventHandler(this.loadIPsFromcsvFileToolStripMenuItem_Click);
            // 
            // saveIPsTocsvFileFromIPListToolStripMenuItem
            // 
            this.saveIPsTocsvFileFromIPListToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.saveIPsTocsvFileFromIPListToolStripMenuItem.Name = "saveIPsTocsvFileFromIPListToolStripMenuItem";
            this.saveIPsTocsvFileFromIPListToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveIPsTocsvFileFromIPListToolStripMenuItem.Text = "Save IP´s to .csv file from IP list";
            this.saveIPsTocsvFileFromIPListToolStripMenuItem.Click += new System.EventHandler(this.saveIPsTocsvFileFromIPListToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // buttonGetCurrentConnectedIPs
            // 
            this.buttonGetCurrentConnectedIPs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetCurrentConnectedIPs.Location = new System.Drawing.Point(6, 16);
            this.buttonGetCurrentConnectedIPs.Name = "buttonGetCurrentConnectedIPs";
            this.buttonGetCurrentConnectedIPs.Size = new System.Drawing.Size(198, 23);
            this.buttonGetCurrentConnectedIPs.TabIndex = 5;
            this.buttonGetCurrentConnectedIPs.Text = "Get current host connected IPs";
            this.buttonGetCurrentConnectedIPs.UseVisualStyleBackColor = true;
            this.buttonGetCurrentConnectedIPs.Click += new System.EventHandler(this.buttonGetCurrentConnectedIPs_Click);
            // 
            // groupBoxFormActions
            // 
            this.groupBoxFormActions.Controls.Add(this.buttonCleanOutput);
            this.groupBoxFormActions.Controls.Add(this.buttonGetCurrentConnectedIPs);
            this.groupBoxFormActions.Controls.Add(this.cleanupIPListButton);
            this.groupBoxFormActions.Controls.Add(this.lookupButton);
            this.groupBoxFormActions.Controls.Add(this.cancelButton);
            this.groupBoxFormActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFormActions.Location = new System.Drawing.Point(12, 687);
            this.groupBoxFormActions.Name = "groupBoxFormActions";
            this.groupBoxFormActions.Size = new System.Drawing.Size(212, 132);
            this.groupBoxFormActions.TabIndex = 6;
            this.groupBoxFormActions.TabStop = false;
            this.groupBoxFormActions.Text = "Actions";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 840);
            this.Controls.Add(this.groupBoxFormActions);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripTop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxIPs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripTop;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookups IPs and get domain";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.groupBoxIPs.ResumeLayout(false);
            this.groupBoxIPs.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStripTop.ResumeLayout(false);
            this.menuStripTop.PerformLayout();
            this.groupBoxFormActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.GroupBox groupBoxIPs;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLookup;
        private System.Windows.Forms.Button lookupButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button buttonCleanOutput;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.MenuStrip menuStripTop;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button buttonExporttoCSV;
        private System.Windows.Forms.ToolStripMenuItem loadIPsFromcsvFileToolStripMenuItem;
        private System.Windows.Forms.Button cleanupIPListButton;
        private System.Windows.Forms.Button buttonGetCurrentConnectedIPs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValidIPCount;
        private System.Windows.Forms.GroupBox groupBoxFormActions;
        private System.Windows.Forms.ToolStripMenuItem saveIPsTocsvFileFromIPListToolStripMenuItem;
    }
}

