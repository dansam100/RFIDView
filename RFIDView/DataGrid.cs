using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using BModule;

namespace RFIDView
{
    public class GridPoint
    {
        public int column, row;
        public GridPoint(int column, int row)
        {
            this.column = column;
            this.row = row;
        }
    }


    public partial class DataGrid : DataGridView
    {
        private List<string> columns;
        private ContextMenuStrip columnMenu;
        private GridPoint selectedCell;

        public DataGrid()
        {
            InitializeComponent();
            this.columns = new List<string>();
            this.columnMenu = new ContextMenuStrip();
            this.TopLeftHeaderCell.ContextMenuStrip = this.columnMenu;
            this.TopLeftHeaderCell.ToolTipText = "Right-click to add/remove columns.";
            selectedCell = new GridPoint(0, 0);
        }


        #region DataSource
        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                base.DataSource = value;
                ConfigureDataSouce();
            }

        }

        private void ConfigureDataSouce()
        {
            if (!this.AutoGenerateColumns)
            {
                PropertyInfo[] propDesc = (DataSource as IBindingView).UnderlyingType.GetProperties();
                foreach (PropertyInfo prop in propDesc)
                {
                    this.Columns.Add(prop.Name, prop.Name);
                }
            }
        }


        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            IBindingView bindingView = DataSource as IBindingView;

            if (bindingView != null)
            {
                PropertyInfo[] propDesc = bindingView.UnderlyingType.GetProperties();
                foreach (PropertyInfo prop in propDesc)
                {
                    if (e.Column.HeaderText == prop.Name)
                    {
                        bool show = true;
                        foreach (Attribute attr in prop.GetCustomAttributes(true))
                        {
                            DataGridProperty view = attr as DataGridProperty;
                            show = (view != null && view.State == PropertyState.Viewable);
                        }

                        ToolStripMenuItem menuItem = new ToolStripMenuItem(prop.Name);
                        if (!columnMenu.Items.ContainsKey(menuItem.Text))
                        {
                            menuItem.Name = prop.Name;
                            menuItem.Click += new EventHandler(menuItem_Click);
                            menuItem.CheckedChanged += new EventHandler(menuItem_CheckedChanged);
                            columnMenu.Items.Add(menuItem);
                        }
                        if (show)
                        {
                            menuItem.Checked = true;
                            if (!this.columns.Contains(menuItem.Text))
                                this.columns.Add(prop.Name);
                        }
                        else { this.Columns[prop.Name].Visible = false; }
                        break;
                    }
                }
                this.columns.Sort();
            }
        }
        #endregion


        #region Column Menu...

        void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                menuItem.Checked = !menuItem.Checked;
            }
        }


        void menuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && this.Columns.Contains(menuItem.Text))
            {
                this.Columns[menuItem.Text].Visible = menuItem.Checked;
                if (menuItem.Checked && !this.columns.Contains(menuItem.Text))
                {
                    this.columns.Add(menuItem.Text);
                    this.columns.Sort();
                }
                else if(!menuItem.Checked)
                {
                    this.columns.Remove(menuItem.Text);
                    this.columns.Sort();
                }
            }
        }
        #endregion

        #region Overrides...
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            DataGrid datagrid = this;
            // Load context menu on right mouse click
            if (datagrid != null && datagrid.ContextMenu != null)
            {
                DataGridView.HitTestInfo hitTestInfo = datagrid.HitTest(e.X, e.Y);

                // If column is first column
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell &&
                    hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    this.selectedCell = new GridPoint(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex);
                    datagrid.CurrentCell = datagrid[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];

                    if (e.Button == MouseButtons.Right)
                    {
                        datagrid.ContextMenu.Show(datagrid, new Point(e.X, e.Y));
                    }
                }
            }
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            try
            {
                this.CurrentCell = this[selectedCell.column, selectedCell.row];
            }
            catch { }
        }
        #endregion

        #region Properties...
        public List<string> ColumnNames
        {
            get { return this.columns; }
        }

        #endregion
    }
}