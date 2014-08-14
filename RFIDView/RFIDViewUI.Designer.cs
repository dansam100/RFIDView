using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using BModule;
using iAnywhere.RfidNet.Core;
namespace RFIDView
{
    partial class Viewer
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.maintableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new Splitter();
            this.sidePanel = new SidePanel();
            this.rfidDataGrid = new DataGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.stopButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.statusTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.monitorBar = new MonitorBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gridMenu = new System.Windows.Forms.ContextMenu();
            this.rfidErrorDataGrid = new DataGrid();
            this.menuStrip.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.maintableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rfidDataGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusTablePanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.statusLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rfidErrorDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(768, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            //
            //gridMenu
            //
            gridMenu.MenuItems.AddRange(new MenuItem[]
                {
                    new MenuItem("&Edit Info...", new System.EventHandler(DataGrid_EditClick)),
                    new MenuItem("&Remove", DataGrid_RemoveClick),
                });
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.mainToolStripMenuItem.Text = "&Main";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.connectToolStripMenuItem.Text = "&Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectMenuItem
            // 
            this.disconnectMenuItem.Enabled = false;
            this.disconnectMenuItem.Name = "disconnectMenuItem";
            this.disconnectMenuItem.Size = new System.Drawing.Size(144, 22);
            this.disconnectMenuItem.Text = "&Disconnect";
            this.disconnectMenuItem.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.maintableLayoutPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(768, 524);
            this.mainPanel.TabIndex = 1;
            // 
            // maintableLayoutPanel
            // 
            this.maintableLayoutPanel.ColumnCount = 2;
            this.maintableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.maintableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.maintableLayoutPanel.Controls.Add(this.tableLayoutPanel, 0, 1);
            this.maintableLayoutPanel.Controls.Add(this.panel2, 1, 2);
            this.maintableLayoutPanel.Controls.Add(this.statusTablePanel, 0, 2);
            this.maintableLayoutPanel.Controls.Add(this.panel4, 0, 0);
            this.maintableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maintableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.maintableLayoutPanel.Name = "maintableLayoutPanel";
            this.maintableLayoutPanel.RowCount = 3;
            this.maintableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.05074F));
            this.maintableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.94926F));
            this.maintableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.maintableLayoutPanel.Size = new System.Drawing.Size(768, 524);
            this.maintableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel.ColumnCount = 3;
            this.maintableLayoutPanel.SetColumnSpan(this.tableLayoutPanel, 2);
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel.Controls.Add(this.splitContainer, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 60);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(762, 410);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(18, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.sidePanel);
            this.splitContainer.Panel1MinSize = 0;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.rfidDataGrid);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(725, 404);
            this.splitContainer.SplitterDistance = 125;
            this.splitContainer.SplitterWidth = 10;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.Text = "Splitter";
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.SystemColors.Control;
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(125, 404);
            this.sidePanel.TabIndex = 2;
            this.sidePanel.Text = "SidePanel";
            // 
            // rfidDataGrid
            // 
            this.rfidDataGrid.AllowUserToAddRows = false;
            this.rfidDataGrid.AllowUserToDeleteRows = false;
            this.rfidDataGrid.AllowUserToOrderColumns = true;
            this.rfidDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rfidDataGrid.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.rfidDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rfidDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rfidDataGrid.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rfidDataGrid.Location = new System.Drawing.Point(0, 0);
            this.rfidDataGrid.Name = "rfidDataGrid";
            this.rfidDataGrid.ReadOnly = true;
            this.rfidDataGrid.Size = new System.Drawing.Size(590, 404);
            this.rfidDataGrid.TabIndex = 0;
            this.rfidDataGrid.Text = "DataGrid";
            this.rfidDataGrid.ColumnStateChanged += new System.Windows.Forms.DataGridViewColumnStateChangedEventHandler(this.DataGrid_ColumnStateChanged);
            this.rfidDataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(387, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 45);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(378, 45);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.stopButton);
            this.panel3.Controls.Add(this.clearButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(192, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 39);
            this.panel3.TabIndex = 0;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(3, 6);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(55, 25);
            this.stopButton.TabIndex = 0;
            this.stopButton.Text = "&Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Enabled = false;
            this.clearButton.Location = new System.Drawing.Point(112, 6);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(55, 25);
            this.clearButton.TabIndex = 0;
            this.clearButton.Text = "&Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // statusTablePanel
            // 
            this.statusTablePanel.BackColor = System.Drawing.SystemColors.Control;
            this.statusTablePanel.ColumnCount = 2;
            this.statusTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.47619F));
            this.statusTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.52381F));
            this.statusTablePanel.Controls.Add(this.statusPanel, 0, 0);
            this.statusTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTablePanel.Location = new System.Drawing.Point(3, 476);
            this.statusTablePanel.Name = "statusTablePanel";
            this.statusTablePanel.RowCount = 2;
            this.statusTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.44444F));
            this.statusTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.55556F));
            this.statusTablePanel.Size = new System.Drawing.Size(378, 45);
            this.statusTablePanel.TabIndex = 4;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.Transparent;
            this.statusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusPanel.Controls.Add(this.statusLayoutPanel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusPanel.Location = new System.Drawing.Point(3, 3);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(146, 22);
            this.statusPanel.TabIndex = 2;
            // 
            // statusLayoutPanel
            // 
            this.statusLayoutPanel.ColumnCount = 2;
            this.statusLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.statusLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statusLayoutPanel.Controls.Add(this.monitorBar, 1, 0);
            this.statusLayoutPanel.Controls.Add(this.statusLabel, 0, 0);
            this.statusLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.statusLayoutPanel.Name = "statusLayoutPanel";
            this.statusLayoutPanel.RowCount = 1;
            this.statusLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statusLayoutPanel.Size = new System.Drawing.Size(144, 20);
            this.statusLayoutPanel.TabIndex = 3;
            // 
            // monitorBar
            // 
            this.monitorBar.BackColor = System.Drawing.Color.Blue;
            this.monitorBar.BackText = "Text...";
            this.monitorBar.BarColor = System.Drawing.Color.Lime;
            this.monitorBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorBar.Location = new System.Drawing.Point(56, 3);
            this.monitorBar.Name = "monitorBar";
            this.monitorBar.Size = new System.Drawing.Size(85, 14);
            this.monitorBar.TabIndex = 1;
            this.monitorBar.TextColor = System.Drawing.Color.Black;
            this.monitorBar.Value = 100;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(47, 20);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Status: ";
            // 
            // panel4
            // 
            this.maintableLayoutPanel.SetColumnSpan(this.panel4, 2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(762, 51);
            this.panel4.TabIndex = 5;
            // 
            // rfidErrorDataGrid
            // 
            this.rfidErrorDataGrid.AllowUserToAddRows = false;
            this.rfidErrorDataGrid.AllowUserToDeleteRows = false;
            this.rfidErrorDataGrid.AllowUserToOrderColumns = true;
            this.rfidErrorDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rfidErrorDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rfidErrorDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rfidErrorDataGrid.Location = new System.Drawing.Point(3, 3);
            this.rfidErrorDataGrid.Name = "rfidErrorDataGrid";
            this.rfidErrorDataGrid.ReadOnly = true;
            this.rfidErrorDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rfidErrorDataGrid.Size = new System.Drawing.Size(378, 33);
            this.rfidErrorDataGrid.TabIndex = 4;
            this.rfidErrorDataGrid.Text = "DataGrid";
            this.rfidErrorDataGrid.ColumnStateChanged += new System.Windows.Forms.DataGridViewColumnStateChangedEventHandler(this.DataGrid_ColumnStateChanged);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 548);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(768, 548);
            this.Name = "Viewer";
            this.Text = "RFIDView";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.maintableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rfidDataGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.statusTablePanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.statusLayoutPanel.ResumeLayout(false);
            this.statusLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rfidErrorDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void maintableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                System.Drawing.Drawing2D.LinearGradientBrush br = new System.Drawing.Drawing2D.LinearGradientBrush(
                              e.ClipRectangle,
                              SystemColors.ControlDark,
                              SystemColors.Control,
                              LinearGradientMode.Horizontal);
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.FillPath(br, Rounder.GetRoundedBounds(e.ClipRectangle, Corners.None));
                br.Dispose();
            }
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel maintableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ToolStripMenuItem disconnectMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenu gridMenu;
        private SidePanel sidePanel;
        private Splitter splitContainer;
        private DataGrid rfidDataGrid;
        private DataGrid rfidErrorDataGrid;
        private TableLayoutPanel statusTablePanel;
        private Label statusLabel;
        private Panel panel4;
        private MonitorBar monitorBar;
        private TableLayoutPanel statusLayoutPanel;
    }
}

