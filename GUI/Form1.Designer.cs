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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolloMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupNameTxt = new System.Windows.Forms.ToolStripTextBox();
            this.setGroupBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyAssignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prooxyCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tryAddAccFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
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
            this.toolStripMenuItem1,
            this.proxyToolStripMenuItem,
            this.UpdateMenu,
            this.tryAddAccFromListToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // FolloMenu
            // 
            this.FolloMenu.Name = "FolloMenu";
            this.FolloMenu.Size = new System.Drawing.Size(67, 24);
            this.FolloMenu.Text = "Follow";
            this.FolloMenu.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // PinMenuItem
            // 
            this.PinMenuItem.Name = "PinMenuItem";
            this.PinMenuItem.Size = new System.Drawing.Size(43, 24);
            this.PinMenuItem.Text = "Pin";
            this.PinMenuItem.Click += new System.EventHandler(this.PinMenuItem_Click);
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
            this.prooxyCheckerToolStripMenuItem});
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
            // UpdateMenu
            // 
            this.UpdateMenu.Name = "UpdateMenu";
            this.UpdateMenu.Size = new System.Drawing.Size(112, 24);
            this.UpdateMenu.Text = "Uppdate Pass";
            this.UpdateMenu.Click += new System.EventHandler(this.UpdateMenu_Click);
            // 
            // tryAddAccFromListToolStripMenuItem
            // 
            this.tryAddAccFromListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAccountToolStripMenuItem,
            this.addAccountToolStripMenuItem1,
            this.resetStatusToolStripMenuItem});
            this.tryAddAccFromListToolStripMenuItem.Name = "tryAddAccFromListToolStripMenuItem";
            this.tryAddAccFromListToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.tryAddAccFromListToolStripMenuItem.Text = " Account";
            // 
            // checkAccountToolStripMenuItem
            // 
            this.checkAccountToolStripMenuItem.Name = "checkAccountToolStripMenuItem";
            this.checkAccountToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.checkAccountToolStripMenuItem.Text = "Check Account";
            this.checkAccountToolStripMenuItem.Click += new System.EventHandler(this.checkAccountToolStripMenuItem_Click);
            // 
            // addAccountToolStripMenuItem1
            // 
            this.addAccountToolStripMenuItem1.Name = "addAccountToolStripMenuItem1";
            this.addAccountToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.addAccountToolStripMenuItem1.Text = "Add account";
            this.addAccountToolStripMenuItem1.Click += new System.EventHandler(this.addAccountToolStripMenuItem1_Click_1);
            // 
            // resetStatusToolStripMenuItem
            // 
            this.resetStatusToolStripMenuItem.Name = "resetStatusToolStripMenuItem";
            this.resetStatusToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.resetStatusToolStripMenuItem.Text = "Reset Status";
            this.resetStatusToolStripMenuItem.Click += new System.EventHandler(this.resetStatusToolStripMenuItem_Click);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem proxyAssignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prooxyCheckerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem tryAddAccFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAccountToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetStatusToolStripMenuItem;
    }
}

