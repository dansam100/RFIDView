using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BModule;
using iAnywhere.RfidNet.Rfid.Multiprotocol;
using iAnywhere.RfidNet.Core;
using System.Threading;

namespace RFIDView
{
    /// <summary>
    /// Shows the viewstate of the current control.
    /// </summary>
    public enum ViewState
    {
        Events,
        Errors
    }
    
    public partial class Viewer : Form
    {
        public delegate void PopulateDelegate(object o);

        private Connector connector;
        private ViewState viewState;

        public event MonitorUpdateHandler MonitoringStatusChanged;

        private BindingListView<EventInfo> rfidEventInfoBindingSource;
        private BindingListView<RnErrorEventArgs> rnErrorEventArgsBindingSource;

        public Viewer()
        {
            this.rfidEventInfoBindingSource = new BindingListView<EventInfo>();
            this.rnErrorEventArgsBindingSource = new BindingListView<RnErrorEventArgs>();
            //
            //rfidEventInfoBindingSource
            //
            this.rfidEventInfoBindingSource.Manager.AddEvent = EventInfo.AddHandler;
            this.rfidEventInfoBindingSource.FilterCollection.Add(new FilterSpec("EventType@=Observed"));

            
            InitializeComponent();

            this.connector = new Connector();
            this.connector.ItemPopulate += new PopulateDelegate(PopulateDataGrid);
            this.viewState = ViewState.Events;
            
            this.sidePanel.ControlClicked += new ControlHandler(sidePanel_ControlClicked);
            this.sidePanel.FilterChanged += new FilterTextChanged(sidePanel_FilterChanged);
            this.connector.ConnectionChanged += new ConnectorChangedHandler(connector_ConnectionChanged);
            this.MonitoringStatusChanged += new MonitorUpdateHandler(RFIDViewUI_MonitoringStatusChanged);
        }

        #region dtor
        ~Viewer()
        {
            connector.Disconnect();
        }
        #endregion

        
        #region ProgressBar Changes...

        public void UpdateStatus(object info, ConnectorStatusEventArgs e)
        {
            if (!this.InvokeRequired)
            {
                if (this.MonitoringStatusChanged != null)
                    this.MonitoringStatusChanged(info, e);
            }
            else this.Invoke(new MonitorUpdateHandler(this.UpdateStatus), info, e);
        }

        void RFIDViewUI_MonitoringStatusChanged(object info, ConnectorStatusEventArgs e)
        {
            this.monitorBar.PerformUpdate(info, e);
        }
        #endregion

        #region SidePanel Events...
        void sidePanel_FilterChanged(object sender, string text)
        {
            SidePanel panel = sender as SidePanel;
            if (panel != null)
            {
                IBindingListView view = null;
                if (this.viewState == ViewState.Events)
                {
                    view = this.rfidEventInfoBindingSource as IBindingListView;
                }
                else
                {
                    view = this.rnErrorEventArgsBindingSource as IBindingListView;
                }
                view.Filter = string.Format("{0} @ {1}%", panel.Filter, text);
            }
        }

        void sidePanel_ControlClicked(object sender, ControlHandlerEventArgs e)
        {
            ControlDefinition def = e.Definition;

            if (def.Label == "View Events" && viewState != ViewState.Events)
            {
                
                viewState = ViewState.Events;
                this.splitContainer.Panel2.Controls.Clear();
                this.splitContainer.Panel2.Controls.Add(rfidDataGrid);

                this.splitContainer.Invalidate();
            }
            else if (def.Label == "View Errors" && viewState != ViewState.Errors)
            {
                viewState = ViewState.Errors;
                this.splitContainer.Panel2.Controls.Clear();
                this.splitContainer.Panel2.Controls.Add(rfidErrorDataGrid);

                this.splitContainer.Invalidate();
            }
        }
        #endregion

        #region Connector Events...
        void connector_ConnectionChanged(object sender, ConnectorStatus e)
        {
            this.sidePanel.Invalidate();

            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectorChangedHandler(connector_ConnectionChanged), sender, e);
            }
            else
            {
                switch (e)
                {
                    case ConnectorStatus.Connected:
                        if (Connector.Connected)
                        {
                            string connectionString = string.Format(
                                "Connected to Business Module: {0}\nDescription: {1}",
                                connector.BusinessModule.Name,
                                    connector.BusinessModule.Description);

                            this.connectToolStripMenuItem.Enabled = false;
                            this.disconnectMenuItem.Enabled = true;
                            this.EnableGrids();
                            ToolTip tip = new ToolTip();
                            tip.IsBalloon = true;
                            tip.Show(connectionString, this.monitorBar, 0, -50, 4000);
                            this.UpdateStatus(connectionString,
                                new ConnectorStatusEventArgs(ConnectorStatus.Connected));
                        }
                        break;

                    case ConnectorStatus.NotConnected:
                        if (!Connector.Connected)
                        {
                            this.connectToolStripMenuItem.Enabled = true;
                            this.disconnectMenuItem.Enabled = false;
                            this.stopButton.Enabled = false;
                            this.clearButton.Enabled = false;
                            this.DisableGrids();
                            this.UpdateStatus("Not Connected!",
                                new ConnectorStatusEventArgs(ConnectorStatus.NotConnected));
                        }
                        break;

                    case ConnectorStatus.Stopped:
                        this.stopButton.Text = "&Start";
                        this.stopButton.Enabled = true;
                        this.clearButton.Enabled = true;
                        this.UpdateStatus("Stopped!",
                                new ConnectorStatusEventArgs(ConnectorStatus.Stopped));
                        break;

                    case ConnectorStatus.Monitoring:
                        this.stopButton.Text = "&Stop";
                        this.stopButton.Enabled = true;
                        this.clearButton.Enabled = true;
                        this.UpdateStatus("Monitoring!",
                                new ConnectorStatusEventArgs(ConnectorStatus.Monitoring));
                        break;

                    case ConnectorStatus.Error:
                        connector.Stop();
                        this.UpdateStatus((sender as Connector).Error,
                                new ConnectorStatusEventArgs(ConnectorStatus.Error));
                        break;
                    default:
                        this.UpdateStatus((sender as Connector).Error,
                                new ConnectorStatusEventArgs(ConnectorStatus.Error));
                        break;
                }
            }
        }
        #endregion
        
        #region MenuItem Events...
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.sidePanel.SetParent(this);
            this.monitorBar.PerformUpdate(new ConnectorStatusEventArgs(ConnectorStatus.NotConnected));
        }


        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connect connectWindow = new Connect(this.connector);
            if (connectWindow.ShowDialog(this) == DialogResult.OK)
            {
                connector.Start();
            }
        }


        private void disconnect_Click(object sender, EventArgs e)
        {
            connector.Disconnect();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            if (aboutBox.ShowDialog(this) == DialogResult.OK)
            {
                aboutBox.Close();
            }
        }
        #endregion

        #region DataGrid...

        void DataGrid_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            this.sidePanel.Invalidate();
        }


        void DataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            if (datagrid != null && e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Left)
            {
                DataForm editform = new DataForm();
                editform.PerformUpdateHandler = this.PerformUpdate;
                editform.Populate(datagrid.Rows[e.RowIndex].DataBoundItem as BModule.EventInfo);

                if (editform.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Data has been written to tag", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //not saved
                }
            }
        }


        bool PerformUpdate(EventInfo oldData, EventInfo newData)
        {
            if (connector != null)
            {
                try
                {
                    if (connector.WriteData(oldData, newData))
                    {
                        this.rfidEventInfoBindingSource.Update(oldData, newData);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),
                           "Update Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }


        void DataGrid_EditClick(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCellMouseEventArgs ev = new DataGridViewCellMouseEventArgs(this.rfidDataGrid.SelectedCells[0].ColumnIndex,
                    this.rfidDataGrid.SelectedCells[0].RowIndex,
                        Cursor.Position.X, Cursor.Position.Y,
                            new MouseEventArgs(MouseButtons.Left, 0, Cursor.Position.X, Cursor.Position.Y, 0));

                this.DataGrid_CellMouseDoubleClick(this.rfidDataGrid, ev);
            }
            catch { }
        }

        void DataGrid_RemoveClick(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = this.rfidDataGrid.SelectedCells[0].OwningRow;
                this.rfidEventInfoBindingSource.Remove(row.DataBoundItem as EventInfo);
            }
            catch { }
        }

        void PopulateDataGrid(object data)
        {
            if (this.rfidDataGrid.InvokeRequired)
            {
                this.rfidDataGrid.Invoke(new PopulateDelegate(PopulateDataGrid), data);
            }
            else
            {
                if (data is EventInfo)
                {
                    lock (this.rfidEventInfoBindingSource)
                    {
                        this.rfidEventInfoBindingSource.Add(data as EventInfo);
                        this.rfidDataGrid.Refresh();
                    }
                }
                else if (data is RnErrorEventArgs)
                {
                    lock (this.rnErrorEventArgsBindingSource)
                    {
                        this.rnErrorEventArgsBindingSource.Add(data as RnErrorEventArgs);
                        this.rfidErrorDataGrid.Refresh();
                    }
                }
            }
        }


        void DisableGrids()
        {
            this.rfidEventInfoBindingSource.Clear();
            this.rnErrorEventArgsBindingSource.Clear();
            this.rfidDataGrid.Enabled = false;
            this.rfidErrorDataGrid.Enabled = false;
        }

        void EnableGrids()
        {
            this.rfidDataGrid.Enabled = true;
            this.rfidErrorDataGrid.Enabled = true;
        }
        #endregion

        #region Buttons...
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (viewState == ViewState.Events)
            {
                lock (rfidEventInfoBindingSource)
                {
                    this.rfidEventInfoBindingSource.Clear();
                }
            }
            else
            {
                lock (rnErrorEventArgsBindingSource)
                {
                    this.rnErrorEventArgsBindingSource.Clear();
                }
            }
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (connector.Status == ConnectorStatus.Monitoring)
            {
                connector.Stop();
            }
            else
            {
                connector.Start();
            }
        }
        #endregion

        #region Properties...
        public List<string> Columns
        {
            get
            {
                if (viewState == ViewState.Events)
                {
                    return this.rfidDataGrid.ColumnNames;
                }
                else
                {
                    return this.rfidErrorDataGrid.ColumnNames;
                }
            }
        }
        #endregion
    }
}