using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BModule;

namespace RFIDView
{
    /// <summary>
    /// Performs an update on the EventInfo item class.
    /// </summary>
    /// <param name="oldData">data to update</param>
    /// <param name="newData">updated value</param>
    /// <returns>success or failure of update</returns>
    public delegate bool PerformUpdateHandler(EventInfo oldData, EventInfo newData);
    
    public partial class DataForm : Form
    {
        //public DataGridViewRow datagridRow;
        //private bool mouse_down;
        //private Point rest_location;

        public PerformUpdateHandler PerformUpdateHandler;
        private EventInfo result;
        private EventInfo data;

        public DataForm()
        {
            InitializeComponent();
        }


        public void Populate(EventInfo data)
        {
            this.data = data;
            this.tagIDBox.Text = data.TagID;
            this.dateBox.Text = data.LastSeen.ToString();
            this.sourceBox.Text = data.Source;
            this.dataBox.Text = data.UserData;
        }


        void saveButton_Click(object sender, System.EventArgs e)
        {
            if (this.tagIDBox.Dirty || this.dataBox.Dirty)
            {
                if (ShowConfirmation() != DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    if (this.tagIDBox.Dirty || this.dataBox.Dirty)
                    {
                        result = new EventInfo(this.data);
                        result.TagID = this.tagIDBox.Text;
                        result.UserData = this.dataBox.Text;
                    }

                    if (this.PerformUpdateHandler != null && this.PerformUpdateHandler(this.data, result))
                        this.DialogResult = DialogResult.OK;
                    else { this.DialogResult = DialogResult.Cancel; };
                }
            }
            else { this.DialogResult = DialogResult.Cancel; };
        }


        private DialogResult ShowConfirmation()
        {
            if (tagIDBox.Dirty && tagIDBox.Text == string.Empty)
            {
                MessageBox.Show("Tag ID field cannot be empty", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return DialogResult.No;
            }
            
            string message = tagIDBox.Dirty ? "Tag " : string.Empty;
            message += (tagIDBox.Dirty && dataBox.Dirty) ? "and " : string.Empty;
            message += dataBox.Dirty ? "User Data " : string.Empty;
            message += "field(s) changed.\nSave changes?";

            return (MessageBox.Show(message, "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question));
        }

        #region StringUtils...
        private static byte[] StringToByteArray(string id)
        {
            if (id == null) return null;

            byte[] bytes = new byte[id.Length / 2];

            StringToByteArray(id, 0, bytes);

            return bytes;
        }


        private static void StringToByteArray(string id, int offset, byte[] bytes)
        {
            int i0, i1;

            id = id.ToLower();

            for (int i = 0; i < id.Length; i += 2)
            {
                i0 = (int)id[i];
                i1 = (int)id[i + 1];

                bytes[i / 2 + offset] = (byte)(((i0 - (i0 >= (int)'a' ? (int)'a' - 0xa : (int)'0')) << 4)
                                          | ((i1 - (i1 >= (int)'a' ? (int)'a' - 0xa : (int)'0'))));
            }
        }
        #endregion


        #region Form Movement...
        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HTCAPTION = 0x2;
        //[System.Runtime.InteropServices.DllImport("User32.dll")]
        //public static extern bool ReleaseCapture();
        //[System.Runtime.InteropServices.DllImport("User32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        ReleaseCapture();
        //        SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        //    }
        //}

        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    base.OnMouseEnter(e);
        //    this.Focus();
        //}
        #endregion


        #region Properties...
        public EventInfo Results
        {
            get { return this.result; }
        }
        #endregion

        #region Junk...
        //public DataForm(DataGridViewRow datagridRow)
        //{
        //    this.datagridRow = datagridRow;
        //    InitializeComponent();
        //    PerformSetup();
        //}

        //private void PerformSetup()
        //{
        //    if (this.datagridRow != null)
        //    {
        //        int i = 0;
        //        foreach (DataGridViewCell cell in this.datagridRow.Cells)
        //        {
        //            Panel lblpanel = new Panel();
        //            Panel ctrlPanel = new Panel();
        //            lblpanel.Dock = DockStyle.Fill;
        //            ctrlPanel.Dock = DockStyle.Fill;

        //            Label label = new Label();
        //            label.Text = cell.OwningColumn.Name;
        //            label.TextAlign = ContentAlignment.MiddleCenter;
        //            label.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        //            EditBox tbox = new EditBox();
        //            tbox.Text = cell.Value.ToString();
        //            tbox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        //            if (i < datagridRow.Cells.Count - 1)
        //            {
        //                mainTable.RowCount++;
        //                this.mainTable.RowStyles.Add(
        //                    new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,
        //                        27F));

        //                this.Height += 27;
        //            }

        //            mainTable.Controls.Add(lblpanel, 0, i);
        //            mainTable.Controls.Add(tbox, 1, i);
        //            i++;
        //        }
        //    }
        #endregion
    }


    public class EditBox : TextBox
    {
        private bool dirty;
        private string initialText;
        private ToolTip toolTip;


        public EditBox()
        {
            initialText = string.Empty;
            toolTip = new ToolTip();
            this.Multiline = false;
            this.dirty = false;
        }

        public Boolean Dirty
        {
            get { return this.dirty; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (string.Compare(initialText, this.Text) != 0)
            {
                this.dirty = true;
                this.BackColor = Color.Gold;
            }
            else
            {
                this.dirty = false;
                this.BackColor = Color.White;
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (Text.Length == 0 && this.initialText == string.Empty)
                    initialText = value;
                base.Text = value;
                this.toolTip.SetToolTip(this, this.Text);
            }
        }

        public ToolTip ToolTip
        {
            get { return this.toolTip; }
            set { this.toolTip = value; }
        }
    }
}