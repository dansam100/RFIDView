using System.Windows.Forms;
namespace RFIDView
{
    partial class DataGrid
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
            this.contextMenu = new ContextMenu();
            this.selectAll = new MenuItem();
            this.dash = new MenuItem();

            //
            //contextMenu
            //
            
            //
            //selectAll
            //
            this.selectAll.Text = "Select &All";
            this.selectAll.Enabled = true;
            //
            //dash
            //
            this.dash.Text = "-";
            this.dash.Enabled = true;
            //
            //dataGrid
            //
            this.Text = "DataGrid";
            this.DoubleBuffered = true;
        }


        #endregion

        private ContextMenu contextMenu;
        private MenuItem selectAll;
        private MenuItem dash;
    }
}