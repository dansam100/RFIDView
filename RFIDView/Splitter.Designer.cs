using System;
using System.Windows.Forms;
using RFIDView.Properties;
namespace RFIDView
{
    partial class Splitter
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
            this.menuStrip = new ContextMenuStrip();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.SplitterWidth = 10;
            this.Text = "Splitter";

            //
            //menuStrip
            //
            this.menuStrip.Items.Add("Collapse", Resources.collapse, menuStrip_Collapse);
            //
            //this
            //
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(Splitter_MouseClick);
            this.ContextMenuStrip = menuStrip;
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
        }

        #endregion

        private ContextMenuStrip menuStrip;
    }
}