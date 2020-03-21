namespace GUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolloMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.followToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followWhere0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rePinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yourPinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupNameTxt = new System.Windows.Forms.ToolStripTextBox();
            this.setGroupBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyAssignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prooxyCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryAddAccFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visbleBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.FolloMenu,
            this.PinMenuItem,
            this.rePinToolStripMenuItem,
            this.toolStripMenuItem1,
            this.proxyToolStripMenuItem,
            this.tryAddAccFromListToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.openToolStripMenuItem.Text = "Edit";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // FolloMenu
            // 
            this.FolloMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.followToolStripMenuItem,
            this.followWhere0ToolStripMenuItem});
            this.FolloMenu.Name = "FolloMenu";
            this.FolloMenu.Size = new System.Drawing.Size(67, 24);
            this.FolloMenu.Text = "Follow";
            this.FolloMenu.Click += new System.EventHandler(this.FolloMenu_Click);
            // 
            // followToolStripMenuItem
            // 
            this.followToolStripMenuItem.Name = "followToolStripMenuItem";
            this.followToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.followToolStripMenuItem.Text = "Follow";
            this.followToolStripMenuItem.Click += new System.EventHandler(this.followToolStripMenuItem_Click);
            // 
            // followWhere0ToolStripMenuItem
            // 
            this.followWhere0ToolStripMenuItem.Name = "followWhere0ToolStripMenuItem";
            this.followWhere0ToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.followWhere0ToolStripMenuItem.Text = "Follow Where 0";
            this.followWhere0ToolStripMenuItem.Click += new System.EventHandler(this.followWhere0ToolStripMenuItem_Click);
            // 
            // PinMenuItem
            // 
            this.PinMenuItem.Name = "PinMenuItem";
            this.PinMenuItem.Size = new System.Drawing.Size(43, 24);
            this.PinMenuItem.Text = "Pin";
            this.PinMenuItem.Click += new System.EventHandler(this.PinMenuItem_Click);
            // 
            // rePinToolStripMenuItem
            // 
            this.rePinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomToolStripMenuItem,
            this.yourPinToolStripMenuItem});
            this.rePinToolStripMenuItem.Name = "rePinToolStripMenuItem";
            this.rePinToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.rePinToolStripMenuItem.Text = "Re Pin";
            this.rePinToolStripMenuItem.Click += new System.EventHandler(this.rePinToolStripMenuItem_Click);
            // 
            // yourPinToolStripMenuItem
            // 
            this.yourPinToolStripMenuItem.Name = "yourPinToolStripMenuItem";
            this.yourPinToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.yourPinToolStripMenuItem.Text = "Your Pin";
            this.yourPinToolStripMenuItem.Click += new System.EventHandler(this.yourPinToolStripMenuItem_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.randomToolStripMenuItem.Text = "Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupNameTxt,
            this.setGroupBtn});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(64, 24);
            this.toolStripMenuItem1.Text = "Group";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // groupNameTxt
            // 
            this.groupNameTxt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupNameTxt.Name = "groupNameTxt";
            this.groupNameTxt.Size = new System.Drawing.Size(100, 27);
            // 
            // setGroupBtn
            // 
            this.setGroupBtn.Name = "setGroupBtn";
            this.setGroupBtn.Size = new System.Drawing.Size(174, 26);
            this.setGroupBtn.Text = "Assing";
            this.setGroupBtn.Click += new System.EventHandler(this.setGroupBtn_Click);
            // 
            // proxyToolStripMenuItem
            // 
            this.proxyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proxyAssignToolStripMenuItem,
            this.prooxyCheckerToolStripMenuItem,
            this.clearProxyToolStripMenuItem});
            this.proxyToolStripMenuItem.Name = "proxyToolStripMenuItem";
            this.proxyToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.proxyToolStripMenuItem.Text = "Proxy";
            this.proxyToolStripMenuItem.Click += new System.EventHandler(this.proxyToolStripMenuItem_Click);
            // 
            // proxyAssignToolStripMenuItem
            // 
            this.proxyAssignToolStripMenuItem.Name = "proxyAssignToolStripMenuItem";
            this.proxyAssignToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.proxyAssignToolStripMenuItem.Text = "Proxy Assign";
            // 
            // prooxyCheckerToolStripMenuItem
            // 
            this.prooxyCheckerToolStripMenuItem.Name = "prooxyCheckerToolStripMenuItem";
            this.prooxyCheckerToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.prooxyCheckerToolStripMenuItem.Text = "Prooxy Checker";
            this.prooxyCheckerToolStripMenuItem.Click += new System.EventHandler(this.prooxyCheckerToolStripMenuItem_Click);
            // 
            // clearProxyToolStripMenuItem
            // 
            this.clearProxyToolStripMenuItem.Name = "clearProxyToolStripMenuItem";
            this.clearProxyToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.clearProxyToolStripMenuItem.Text = "Clear Proxy";
            this.clearProxyToolStripMenuItem.Click += new System.EventHandler(this.clearProxyToolStripMenuItem_Click);
            // 
            // tryAddAccFromListToolStripMenuItem
            // 
            this.tryAddAccFromListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAccountToolStripMenuItem,
            this.addAccountToolStripMenuItem1,
            this.resetStatusToolStripMenuItem,
            this.updatePasswordToolStripMenuItem,
            this.renameAccountToolStripMenuItem,
            this.addXmlToolStripMenuItem});
            this.tryAddAccFromListToolStripMenuItem.Name = "tryAddAccFromListToolStripMenuItem";
            this.tryAddAccFromListToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.tryAddAccFromListToolStripMenuItem.Text = " Account";
            // 
            // checkAccountToolStripMenuItem
            // 
            this.checkAccountToolStripMenuItem.Name = "checkAccountToolStripMenuItem";
            this.checkAccountToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.checkAccountToolStripMenuItem.Text = "Check Account";
            this.checkAccountToolStripMenuItem.Click += new System.EventHandler(this.checkAccountToolStripMenuItem_Click);
            // 
            // addAccountToolStripMenuItem1
            // 
            this.addAccountToolStripMenuItem1.Image = global::GUI.Properties.Resources.Danleech_Simple_Pinterest;
            this.addAccountToolStripMenuItem1.Name = "addAccountToolStripMenuItem1";
            this.addAccountToolStripMenuItem1.Size = new System.Drawing.Size(206, 26);
            this.addAccountToolStripMenuItem1.Text = "Add account";
            this.addAccountToolStripMenuItem1.Click += new System.EventHandler(this.addAccountToolStripMenuItem1_Click_1);
            // 
            // resetStatusToolStripMenuItem
            // 
            this.resetStatusToolStripMenuItem.Name = "resetStatusToolStripMenuItem";
            this.resetStatusToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.resetStatusToolStripMenuItem.Text = "Reset Status";
            this.resetStatusToolStripMenuItem.Click += new System.EventHandler(this.resetStatusToolStripMenuItem_Click);
            // 
            // updatePasswordToolStripMenuItem
            // 
            this.updatePasswordToolStripMenuItem.Name = "updatePasswordToolStripMenuItem";
            this.updatePasswordToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.updatePasswordToolStripMenuItem.Text = "Update Password";
            this.updatePasswordToolStripMenuItem.Click += new System.EventHandler(this.updatePasswordToolStripMenuItem_Click);
            // 
            // renameAccountToolStripMenuItem
            // 
            this.renameAccountToolStripMenuItem.Name = "renameAccountToolStripMenuItem";
            this.renameAccountToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.renameAccountToolStripMenuItem.Text = "Rename Account";
            this.renameAccountToolStripMenuItem.Click += new System.EventHandler(this.renameAccountToolStripMenuItem_Click);
            // 
            // addXmlToolStripMenuItem
            // 
            this.addXmlToolStripMenuItem.Name = "addXmlToolStripMenuItem";
            this.addXmlToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.addXmlToolStripMenuItem.Text = "Add Xml";
            this.addXmlToolStripMenuItem.Click += new System.EventHandler(this.addXmlToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visbleBox});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.optionsToolStripMenuItem_CheckStateChanged);
            // 
            // visbleBox
            // 
            this.visbleBox.AutoCompleteCustomSource.AddRange(new string[] {
            "On",
            "Off"});
            this.visbleBox.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.visbleBox.Name = "visbleBox";
            this.visbleBox.Size = new System.Drawing.Size(121, 28);
            this.visbleBox.TextChanged += new System.EventHandler(this.visbleBox_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1067, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "Info";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 504);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCount.AutoSize = true;
            this.labelCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCount.Location = new System.Drawing.Point(12, 536);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(46, 17);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "label1";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStatus.Location = new System.Drawing.Point(64, 537);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(46, 17);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "label1";
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuery.Location = new System.Drawing.Point(879, 533);
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(188, 22);
            this.textBoxQuery.TabIndex = 6;
            this.textBoxQuery.TextChanged += new System.EventHandler(this.textBoxQuery_TextChanged);
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Items.AddRange(new object[] {
            "UserName ",
            "Email ",
            "FullName"});
            this.comboBoxColumn.Location = new System.Drawing.Point(752, 533);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(121, 24);
            this.comboBoxColumn.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.textBoxQuery);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Account Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FolloMenu;
        private System.Windows.Forms.ToolStripMenuItem PinMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox groupNameTxt;
        private System.Windows.Forms.ToolStripMenuItem setGroupBtn;
        private System.Windows.Forms.ToolStripMenuItem proxyToolStripMenuItem;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ToolStripMenuItem proxyAssignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prooxyCheckerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tryAddAccFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAccountToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearProxyToolStripMenuItem;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ToolStripMenuItem rePinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followWhere0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePasswordToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem renameAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addXmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox visbleBox;
        private System.Windows.Forms.ToolStripMenuItem yourPinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
    }
}

