using System;
using System.Collections.Generic;
using System.Text;
using BModule;
using iAnywhere.RfidNet.Core;
using System.Windows.Forms;
using iAnywhere.RfidNet.Rfid.Multiprotocol;
using System.ComponentModel;

namespace RFIDView
{    
    public delegate void ConnectorChangedHandler(object sender, ConnectorStatus e);
    public delegate void MonitorUpdateHandler(object info, ConnectorStatusEventArgs e);

    /// <summary>
    /// Connector Status types
    /// </summary>
    [Flags]
    public enum ConnectorStatus
    {
        Error,
        NotConnected,
        Connecting,
        Connected,
        Stopped,
        Monitoring,
    }


    /// <summary>
    /// Business module wrapper
    /// </summary>
    public class Connector
    {
        private RFIDBModule bm = null;
        private ConnectorStatus status;
        private Exception error;

        //connects to simulation by default
        private string url = "tcp://localhost:8383/RnManager";
        private string guid = "82c5e44a-c4bb-4728-a664-6fae9866d2ba";
        private string source = null;
        private int timeout = 2000;
        private static bool connected;

        /// <summary>
        /// Periodically polls the business module for event or error updates.
        /// </summary>
        private System.Windows.Forms.Timer timer = null;

        public event ConnectorChangedHandler ConnectionChanged;
        public event Viewer.PopulateDelegate ItemPopulate;

        public Connector()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = timeout;
            timer.Enabled = false;
        }


        #region Public Functions...
        /// <summary>
        /// Connect to the desired business model
        /// </summary>
        public bool Connect()
        {
            this.error = null;
            try
            {
                this.status = ConnectorStatus.Connecting | ConnectorStatus.NotConnected;
                if (this.ConnectionChanged != null)
                    this.ConnectionChanged(this, status);

                if (!Connected)
                {
                    ThreadedConnect();
                }
            }
            catch(Exception e) {
                connected = false;
                error = e;
                MessageBox.Show(string.Format("Error while connecting to BM.\n\nReason: {0}", e.Message),
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }


        public bool Connect(string source, string url, string guid)
        {
            this.source = (string.IsNullOrEmpty(source)) ? null : source;
            this.guid = guid;
            this.url = url;
            return this.Connect();
        }


        public void Start()
        {
            if (connected && bm.RunStatus != ServiceStatus.Started)
            {
                if (MessageBox.Show(string.Format("Business Module '{0}' ({1}) is currently not running.\nStart it from the web admin console?",
                        bm.Name, bm.Description),
                            "Not Running!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    bm.RunStatus = ServiceStatus.Started;
                    status = ConnectorStatus.Connected;
                    if (this.ConnectionChanged != null)
                        this.ConnectionChanged(this, this.status);
                }
            }
            if (status  == ConnectorStatus.Connected)
            {
                bm.StartReading();
                timer.Enabled = true;
                status = ConnectorStatus.Monitoring;
                if (this.ConnectionChanged != null)
                    this.ConnectionChanged(this, this.status);
            }
        }


        public void Disconnect()
        {
            Stop();
            bm.RunStatus = ServiceStatus.Stopped;
            bm = null;
            connected = false;
            this.status = ConnectorStatus.NotConnected;

            if (this.ConnectionChanged != null)
                this.ConnectionChanged(this, status);
        }


        public void Stop()
        {
            timer.Enabled = false;
            bm.StopReading();
            status = ConnectorStatus.Stopped;

            if (this.ConnectionChanged != null)
                this.ConnectionChanged(this, this.status);
        }


        /// <summary>
        /// Write data to tag.
        /// </summary>
        /// <param name="oldData">old tag data</param>
        /// <param name="newData">new tag data</param>
        /// <returns></returns>
        public bool WriteData(EventInfo oldData, EventInfo newData)
        {
            lock (bm)
            {
                try
                {
                    if (oldData.TagID.CompareTo(newData.TagID) != 0)
                    {
                        bm.WriteToID(oldData.Source, newData.TagID, 0);
                    }
                    bm.WriteToData(oldData.Source, newData.TagID, newData.UserData, 0);
                }
                catch (Exception ex)
                {
                    error = ex;
                    MessageBox.Show(string.Format("Unable to write to tagID: {0} on {1}.\n\nReason: {2}", 
                        oldData.TagID, oldData.Source, ex.Message),
                           "Write Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Timer...
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {                
                if(status == ConnectorStatus.Monitoring)
                {
                    BeginLoad();
                } 
            }
            catch(Exception ex)
            {
                this.Stop();
                error = ex;

                this.status = ConnectorStatus.Error;
                if (this.ConnectionChanged != null)
                    this.ConnectionChanged(this, status);

                MessageBox.Show(string.Format("Connection Failed.\n\nReason: {0}", ex.Message),
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connected = false;
            }
        }

        private void BeginLoad()
        {
            List<object> messages = bm.GetMessages() as List<object>;
            if (messages != null)
            {
                foreach (object m in messages)
                {
                    if(this.ItemPopulate != null)
                        this.ItemPopulate(m);
                }
            }
        }
        #endregion

        #region Connection...
        private void ThreadedConnect()
        {
            try
            {
                if (this.ConnectToBM(guid, url))
                {
                    this.status = ConnectorStatus.Connected;
                }
                else if (connected)
                {
                    this.status = ConnectorStatus.Stopped;
                }

                if (this.ConnectionChanged != null)
                    this.ConnectionChanged(this, status);
            }
            catch (Exception ex)
            {
                this.status = ConnectorStatus.Error;
                error = ex;

                MessageBox.Show(string.Format("Unable to connect to Business Module.\n\nReason: {0}", ex.Message),
                        "Failed to connect!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connected = false;
            }
        }


        /// <summary>
        /// This is used by the app to connect to our BM.
        /// </summary>
        /// <returns>bool indicating if the connection was successful or not.</returns>
        private bool ConnectToBM(string id, string url)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(id))
            {
                this.url = url;
                this.guid = id;
                return this.ConnectToBM();
            }
            return false;
        }


        /// <summary>
        /// This is used by the app to connect to our BM.
        /// </summary>
        /// <returns>bool indicating if the connection was successful or not.</returns>
        private bool ConnectToBM()
        {
            IRnManager rnManager = (IRnManager)Activator.GetObject(typeof(IRnManager), url);
            RnUri uri = new RnUri();
            uri.ID = guid;
            uri.Type = RnUriType.bm;

            if (rnManager == null)
                throw new Exception("rnManager is null");

            string rnUrl = rnManager.GetServiceUrl(uri.Uri);

            if (rnUrl != null)
                bm = (RFIDBModule)Activator.GetObject(typeof(RFIDBModule), rnUrl, source);
            else
                throw new Exception("rnUrl is null");
            //if (bm.RunStatus != ServiceStatus.Started)
            //{
            //    if (MessageBox.Show(string.Format("Business Module '{0}' ({1}) is currently not running.\nStart it from the web admin console?",
            //            bm.Name, bm.Description),
            //                "Not Running!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            //    {
            //        bm.RunStatus = ServiceStatus.Started;
            //        connected = true;
            //    }
            //}
            connected = true;
            return bm.RunStatus == ServiceStatus.Started;
        }
        #endregion

        #region Status Properties...
        public ConnectorStatus Status
        {
            get { return status; }
        }

        public RFIDBModule BusinessModule
        {
            get { return bm; }
        }

        public Exception Error
        {
            get { return this.error; }
        }

        public static bool Connected
        {
            get { return connected; }
        }
        #endregion
    }
}
