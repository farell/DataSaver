namespace DataSaver
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewBridgeItem = new System.Windows.Forms.ListView();
            this.rabbitIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rabbitPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rabbitQueueName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdatabaseIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdatabasePort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdatabaseName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 517);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewBridgeItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1024, 305);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据配置";
            // 
            // listViewBridgeItem
            // 
            this.listViewBridgeItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rabbitIP,
            this.rabbitPort,
            this.rabbitQueueName,
            this.cdatabaseIP,
            this.cdatabasePort,
            this.cdatabaseName});
            this.listViewBridgeItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBridgeItem.GridLines = true;
            this.listViewBridgeItem.HideSelection = false;
            this.listViewBridgeItem.Location = new System.Drawing.Point(3, 21);
            this.listViewBridgeItem.Name = "listViewBridgeItem";
            this.listViewBridgeItem.Size = new System.Drawing.Size(1018, 281);
            this.listViewBridgeItem.TabIndex = 0;
            this.listViewBridgeItem.UseCompatibleStateImageBehavior = false;
            this.listViewBridgeItem.View = System.Windows.Forms.View.Details;
            // 
            // rabbitIP
            // 
            this.rabbitIP.Text = "RabbitIP";
            this.rabbitIP.Width = 78;
            // 
            // rabbitPort
            // 
            this.rabbitPort.Text = "RabbitPort";
            this.rabbitPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rabbitPort.Width = 135;
            // 
            // rabbitQueueName
            // 
            this.rabbitQueueName.Text = "RabbitQueueName";
            this.rabbitQueueName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rabbitQueueName.Width = 148;
            // 
            // cdatabaseIP
            // 
            this.cdatabaseIP.Text = "DatabaseIP";
            this.cdatabaseIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cdatabaseIP.Width = 99;
            // 
            // cdatabasePort
            // 
            this.cdatabasePort.Text = "DatabasePort";
            this.cdatabasePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cdatabasePort.Width = 109;
            // 
            // cdatabaseName
            // 
            this.cdatabaseName.Text = "DatabaseName";
            this.cdatabaseName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cdatabaseName.Width = 121;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1024, 208);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(3, 21);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(1018, 184);
            this.textBoxLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.控制ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 控制ToolStripMenuItem
            // 
            this.控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemStart,
            this.ToolStripMenuItemStop});
            this.控制ToolStripMenuItem.Name = "控制ToolStripMenuItem";
            this.控制ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.控制ToolStripMenuItem.Text = "控制";
            // 
            // ToolStripMenuItemStart
            // 
            this.ToolStripMenuItemStart.Name = "ToolStripMenuItemStart";
            this.ToolStripMenuItemStart.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItemStart.Text = "开始";
            this.ToolStripMenuItemStart.Click += new System.EventHandler(this.ToolStripMenuItemStart_Click);
            // 
            // ToolStripMenuItemStop
            // 
            this.ToolStripMenuItemStop.Name = "ToolStripMenuItemStop";
            this.ToolStripMenuItemStop.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItemStop.Text = "停止";
            this.ToolStripMenuItemStop.Click += new System.EventHandler(this.ToolStripMenuItemStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 545);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DataSaver";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewBridgeItem;
        private System.Windows.Forms.ColumnHeader rabbitIP;
        private System.Windows.Forms.ColumnHeader rabbitPort;
        private System.Windows.Forms.ColumnHeader rabbitQueueName;
        private System.Windows.Forms.ColumnHeader cdatabaseIP;
        private System.Windows.Forms.ColumnHeader cdatabasePort;
        private System.Windows.Forms.ColumnHeader cdatabaseName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStart;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStop;
    }
}

