using System.Windows.Forms;
namespace RFIDView
{
    partial class PanTextBox
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
            //
            //panTextBox
            //
            this.Height = 25;
            this.Width = 100;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Font = new System.Drawing.Font(this.Font.FontFamily, 9, System.Drawing.FontStyle.Regular);
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            
            //this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
        }

        #endregion
    }
}