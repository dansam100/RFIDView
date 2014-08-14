using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RFIDView
{
    public partial class Connect : Form
    {
        Connector connector;
        System.Text.RegularExpressions.Regex regex;
        string guidregex = @"^([0-9a-fA-F]){8}(-([0-9a-fA-F]){4}){3}-(([0-9a-fA-F]){12})$";
        string urlregex = @"^(http|https|tcp){1}:/{2}(www\.)?([-\w\.]+)+(:\d+)?(/([\w/_\.]*(\?\S+)?)?)?$";

        public Connect()
        {
            InitializeComponent();
        }

        public Connect(Connector connector)
        {
            this.connector = connector;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.urlBox.Text = Properties.Settings.Default.url;
            this.guidBox.Text = Properties.Settings.Default.guid;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            string url = null, guid = null, source = null;
            bool tryConnect = true;

            if (!string.IsNullOrEmpty(this.sourceBox.Text))
                    source = this.sourceBox.Text;

                if (this.checkBox.Checked)
                {
                    if (!string.IsNullOrEmpty(this.urlBox.Text) && !string.IsNullOrEmpty(this.guidBox.Text))
                    {
                        regex = new System.Text.RegularExpressions.Regex(urlregex,
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
                        if (regex.IsMatch(this.urlBox.Text))
                        {
                            url = this.urlBox.Text;
                        }
                        else
                        {
                            //invalid url
                            string error = string.Format("Invalid url entered. Please correct!");
                            MessageBox.Show(error, "Invalid Url!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            tryConnect = false;
                        }

                        if (tryConnect)
                        {
                            regex = new System.Text.RegularExpressions.Regex(guidregex,
                                System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

                            if (regex.IsMatch(this.guidBox.Text))
                            {
                                guid = this.guidBox.Text;
                            }
                            else
                            {
                                //Invalid guid
                                string error = string.Format("Invalid guid format specified. Please correct!");
                                MessageBox.Show(error, "Invalid Guid!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                tryConnect = false;
                            }
                        }
                    }
                }
                else
                {
                    url = Properties.Settings.Default.url;
                    guid = Properties.Settings.Default.guid;
                }

            if (tryConnect && connector.Connect(this.sourceBox.Text, url, guid))
            {
                DialogResult = DialogResult.OK;
            }
        }


        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                if (!checkBox.Checked)
                {
                    this.urlBox.Text = Properties.Settings.Default.url;
                    this.guidBox.Text = Properties.Settings.Default.guid;
                }

                this.urlBox.Enabled = checkbox.Checked;
                this.guidBox.Enabled = checkbox.Checked;
            }
        }

    }
}