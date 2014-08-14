namespace RFIDView
{
    partial class DataForm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tagLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dateLabel = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataLabel = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tagIDBox = new EditBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.sourceBox = new EditBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dateBox = new EditBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.dataBox = new EditBox();
            this.btnTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.mainTable.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.btnTableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.08333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.91666F));
            this.tableLayoutPanel.Controls.Add(this.mainPanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnTableLayoutPanel, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(303, 218);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.mainPanel, 2);
            this.mainPanel.Controls.Add(this.mainTable);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPanel.Location = new System.Drawing.Point(3, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(297, 172);
            this.mainPanel.TabIndex = 0;
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 2;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.58537F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.41463F));
            this.mainTable.Controls.Add(this.panel3, 0, 0);
            this.mainTable.Controls.Add(this.panel4, 0, 1);
            this.mainTable.Controls.Add(this.panel5, 0, 2);
            this.mainTable.Controls.Add(this.panel6, 0, 3);
            this.mainTable.Controls.Add(this.panel7, 1, 0);
            this.mainTable.Controls.Add(this.panel8, 1, 1);
            this.mainTable.Controls.Add(this.panel9, 1, 2);
            this.mainTable.Controls.Add(this.panel10, 1, 3);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 4;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTable.Size = new System.Drawing.Size(297, 172);
            this.mainTable.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tagLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(102, 24);
            this.panel3.TabIndex = 0;
            // 
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagLabel.Location = new System.Drawing.Point(5, 3);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(49, 15);
            this.tagLabel.TabIndex = 0;
            this.tagLabel.Text = "Tag ID";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sourceLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(2, 33);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(104, 24);
            this.panel4.TabIndex = 2;
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(6, 0);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(52, 15);
            this.sourceLabel.TabIndex = 1;
            this.sourceLabel.Text = "Source";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dateLabel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(2, 63);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(104, 24);
            this.panel5.TabIndex = 3;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(7, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(71, 15);
            this.dateLabel.TabIndex = 0;
            this.dateLabel.Text = "Last Seen";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataLabel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(2, 93);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(104, 76);
            this.panel6.TabIndex = 4;
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(6, 10);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(71, 15);
            this.dataLabel.TabIndex = 0;
            this.dataLabel.Text = "User Data";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tagIDBox);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(111, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(183, 24);
            this.panel7.TabIndex = 5;
            // 
            // tagIDBox
            // 
            this.tagIDBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagIDBox.Location = new System.Drawing.Point(3, 3);
            this.tagIDBox.Name = "tagIDBox";
            this.tagIDBox.Size = new System.Drawing.Size(174, 21);
            this.tagIDBox.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.sourceBox);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(111, 33);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(183, 24);
            this.panel8.TabIndex = 6;
            // 
            // sourceBox
            // 
            this.sourceBox.BackColor = System.Drawing.Color.Gold;
            this.sourceBox.Enabled = false;
            this.sourceBox.Location = new System.Drawing.Point(3, 0);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(174, 21);
            this.sourceBox.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.dateBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(111, 63);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(183, 24);
            this.panel9.TabIndex = 7;
            // 
            // dateBox
            // 
            this.dateBox.BackColor = System.Drawing.Color.Gold;
            this.dateBox.Enabled = false;
            this.dateBox.Location = new System.Drawing.Point(3, 4);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(174, 21);
            this.dateBox.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.dataBox);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(111, 93);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(183, 76);
            this.panel10.TabIndex = 8;
            // 
            // dataBox
            // 
            this.dataBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataBox.Location = new System.Drawing.Point(0, 0);
            this.dataBox.Multiline = true;
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size(183, 76);
            this.dataBox.TabIndex = 0;
            // 
            // btnTableLayoutPanel
            // 
            this.btnTableLayoutPanel.ColumnCount = 2;
            this.btnTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.btnTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.btnTableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.btnTableLayoutPanel.Controls.Add(this.panel2, 1, 0);
            this.btnTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTableLayoutPanel.Location = new System.Drawing.Point(85, 181);
            this.btnTableLayoutPanel.Name = "btnTableLayoutPanel";
            this.btnTableLayoutPanel.RowCount = 1;
            this.btnTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.btnTableLayoutPanel.Size = new System.Drawing.Size(215, 34);
            this.btnTableLayoutPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(101, 28);
            this.panel1.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(15, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(67, 24);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(110, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 28);
            this.panel2.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(13, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(69, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // DataForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(303, 218);
            this.Controls.Add(this.tableLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit...";
            this.tableLayoutPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainTable.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.btnTableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.TableLayoutPanel btnTableLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label tagLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private EditBox tagIDBox;
        private EditBox dataBox;
        private EditBox sourceBox;
        private EditBox dateBox;
    }
}