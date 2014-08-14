using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RFIDView
{
    public partial class PanTextBox : TextBox
    {
        private bool lostfocus = false;
        private object lockObj = null;
        private bool textchanged = false, tracker = false;
        private Timer timer;

        public event FilterTextChanged FilterChanged;

        public PanTextBox()
        {
            InitializeComponent();
            lockObj = new object();
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
        }

        ~PanTextBox()
        {
            timer.Stop();
            timer.Enabled = false;
        }

        #region Timer
        void timer_Tick(object sender, EventArgs e)
        {
            lock (lockObj)
            {
                if (tracker && !(tracker && textchanged))
                {
                    this.InvokeFilterChanged();
                    timer.Stop();
                    timer.Enabled = false;
                }
                else
                {
                    tracker = textchanged;
                }
                textchanged = false;
            }
        }
        #endregion

        #region Overrides...
        protected override void OnTextChanged(EventArgs e)
        {
            lock (lockObj)
            {
                base.OnTextChanged(e);
                textchanged = true;
                if (!timer.Enabled)
                {
                    timer.Enabled = true;
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                    timer.Start();
                }
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                this.timer.Enabled = false;
                textchanged = tracker = false;
                this.InvokeFilterChanged();
            }
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = Color.Wheat;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
        }


        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            if (lostfocus)
                this.SelectAll();
            lostfocus = false;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.lostfocus = true;
        }
#endregion

        #region Paint
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //    TextFormatFlags flags = TextFormatFlags.NoPadding |
        //    TextFormatFlags.Top | TextFormatFlags.TextBoxControl;
        //    Rectangle rect = this.ClientRectangle;

        //    // Offset the rectangle based on the HorizontalAlignment, 
        //    flags |= TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.Left;
        //    rect.Offset(1, 1);

        //    Font font = new Font(this.Font.FontFamily, 9, FontStyle.Regular);

        //    // Draw the prompt text using TextRenderer
        //    TextRenderer.DrawText(e.Graphics, this.Text, font, e.ClipRectangle,
        //        Color.Black, this.BackColor, flags);
        //}
        #endregion

        /// <summary>
        /// Invokes the filterchanged event if there are any subscriptions
        /// </summary>
        internal void InvokeFilterChanged()
        {
            if (this.FilterChanged != null)
            {
                this.FilterChanged(this.Parent, this.Text);
            }
        }
    }
}