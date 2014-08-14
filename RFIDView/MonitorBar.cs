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
    public enum Direction
    {
        Reverse = -1,
        Forward = 1
    }
    
    public partial class MonitorBar : ProgressBar
    {
        private string text;
        private Color barcolor, textcolor;
        private ToolTip tooltip;
        private const int barlength = 80;
        private int barlocation = -barlength, blocksize = 15;
        private Timer timer;

        Direction direction = Direction.Forward;

        public MonitorBar()
        {
            InitializeComponent();
            this.timer = new Timer();
            this.tooltip = new ToolTip();
            this.tooltip.UseAnimation = true;

            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Interval = 1;

            this.MouseClick += new MouseEventHandler(MonitorBar_MouseClick);
        }

        void MonitorBar_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tooltip.GetToolTip(this)))
            {
                MessageBox.Show(this.tooltip.GetToolTip(this), "RFIDView says:..");
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            barlocation += (int)direction;
            Rectangle rect = this.ClientRectangle;

            switch(direction)
            {
                case Direction.Forward:
                    if (barlocation > this.ClientRectangle.Right)
                    {
                        barlocation = this.ClientRectangle.Right;
                        direction = Direction.Reverse;
                    }

                    rect = new Rectangle(this.ClientRectangle.Left + barlocation - 1, this.ClientRectangle.Top,
                        barlength + 1, this.ClientRectangle.Height);
                    break;
                case Direction.Reverse:
                    if ((this.ClientRectangle.Left + barlocation + barlength) < this.ClientRectangle.Left)
                    {
                        barlocation = this.ClientRectangle.Left + (int)direction * barlength;
                        direction = Direction.Forward;
                    }
                    rect = new Rectangle(this.ClientRectangle.Left + barlocation + 1, this.ClientRectangle.Top,
                        barlength + 1, this.ClientRectangle.Height);
                    break;
            }
            this.Invalidate(rect);
        }

        #region Overrides...
        protected override void OnResize(EventArgs e)
        {
            // Invalidate the control to get a repaint.
            this.Invalidate();
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Focus();
        }


        protected override void OnPaint(PaintEventArgs e)
         {
            base.OnPaint(e);

            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this.BackColor,
                Color.White, LinearGradientMode.Horizontal);
            SolidBrush barBrush = new SolidBrush(this.barcolor);
            SolidBrush textBrush = new SolidBrush(this.textcolor);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;

            GraphicsPath path = Rounder.GetRoundedBounds(this.ClientRectangle, Corners.None);
            e.Graphics.FillPath(brush, path);

            //if (!DesignMode)
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                System.Drawing.Font font = new Font(FontFamily.GenericSerif, 8f, FontStyle.Bold);

                if (this.Style == ProgressBarStyle.Continuous)
                {
                    float percent = (float)(base.Value - base.Minimum) / (float)(base.Maximum - base.Minimum);
                    Rectangle rect = this.ClientRectangle;

                    // Calculate area for drawing the progress.
                    rect.Width = (int)((float)rect.Width * percent);

                    GraphicsPath barPath = Rounder.GetRoundedBounds(rect, Corners.All);
                    // Draw the progress meter.
                    g.FillPath(barBrush, barPath);
                }
                else if (this.Style == ProgressBarStyle.Marquee)
                {
                    Rectangle rect = new Rectangle(new Point(barlocation, this.ClientRectangle.Top),
                        new Size(barlength, this.ClientRectangle.Height));

                    GraphicsPath barPath = Rounder.GetRoundedBounds(rect, Corners.None);

                    LinearGradientBrush barBrush2 = new LinearGradientBrush(rect, this.barcolor, Color.Yellow,
                        (direction == Direction.Forward) ? LinearGradientMode.ForwardDiagonal : LinearGradientMode.BackwardDiagonal);

                    g.FillPath(barBrush2, barPath);
                }
                else
                {
                    float percent = (float)(base.Value - base.Minimum) / (float)(base.Maximum - base.Minimum);
                    Rectangle rect = this.ClientRectangle;

                    // Calculate area for drawing the progress.
                    rect.Width = (int)((float)rect.Width * percent);

                    int remainder = 0;
                    int numBlocks = Math.DivRem(rect.Width, blocksize, out remainder);
                    int left = rect.Left, top = rect.Top, height = rect.Height;

                    for (int i = 0; i < numBlocks; i++)
                    {
                        Rectangle block = new Rectangle(left, top, blocksize, height);
                        left += blocksize;

                        GraphicsPath barPath = Rounder.GetRoundedBounds(block, Corners.All);

                        LinearGradientBrush blockBrush = new LinearGradientBrush(block, this.barcolor,
                            Color.White, LinearGradientMode.Horizontal);
                        // Draw the progress meter.

                        g.FillPath(blockBrush, barPath);
                    }

                    Rectangle remBlock = new Rectangle(left, top, remainder, height);
                    g.FillPath(barBrush, Rounder.GetRoundedBounds(remBlock, Corners.All));
                }

                // Draw a three-dimensional border around the control.
                //Draw3DBorder(g);
                Rectangle textArea = new Rectangle(new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
                    this.ClientRectangle.Size);
                g.DrawString(this.BackText, font, textBrush, textArea, sf);
            }

            // Clean up.
            brush.Dispose();
            textBrush.Dispose();
            barBrush.Dispose();
            g.Dispose();	
        }


        private void Draw3DBorder(Graphics g)
        {
            int PenWidth = (int)Pens.White.Width;

            g.DrawLine(Pens.DarkGray,
                new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
                new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top));
            g.DrawLine(Pens.DarkGray,
                new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
                new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth));
            g.DrawLine(Pens.White,
                new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth),
                new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
            g.DrawLine(Pens.White,
                new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top),
                new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
        }
        #endregion


        #region UpdateHandlers...
        private delegate void ToolTipUpdateDelegate(object info, ConnectorStatusEventArgs e);
        public void PerformUpdate(object info, ConnectorStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolTipUpdateDelegate(PerformUpdate), info, e);
            }
            else
            {
                this.PerformUpdate(e);
                if (info != null)
                {
                    this.tooltip.SetToolTip(this, info.ToString());
                    //if (e.Status == ConnectorStatus.Connected)
                    //{
                    //    this.tooltip.IsBalloon = true;
                    //    this.tooltip.Show(info.ToString(), this, 4000);
                    //    this.tooltip.IsBalloon = false;
                    //}
                    //else
                        this.tooltip.Show(info.ToString(), this, 1000);
                }
            }
        }

        public delegate void PerformUpdateDelegate(ConnectorStatusEventArgs e);
        public void PerformUpdate(ConnectorStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new PerformUpdateDelegate(PerformUpdate), e);
            }
            else
            {
                this.Value = (int)(e.Percent * this.Maximum);
                this.BackText = string.Format("{0}", e.Status);
                this.textcolor = Color.Black;
                switch (e.Status)
                {
                    case ConnectorStatus.Error:
                        this.barcolor = Color.Transparent;
                        this.BackColor = Color.Red;
                        this.BackText = string.Concat(this.BackText, "!");
                        this.timer.Enabled = false;
                        break;
                    case ConnectorStatus.Monitoring:
                        this.barcolor = Color.Lime;
                        this.BackColor = Color.White;
                        this.Style = ProgressBarStyle.Marquee;
                        this.BackText = string.Concat(this.BackText, "...");
                        this.timer.Enabled = true;
                        break;
                    case ConnectorStatus.Stopped:
                        this.BackText = string.Concat(this.BackText, "!");
                        this.timer.Enabled = false;
                        break;
                    case ConnectorStatus.Connecting:
                        this.BackText = string.Concat(this.BackText, "...");
                        this.barcolor = Color.Transparent;
                        this.BackColor = Color.Yellow;
                        this.Style = ProgressBarStyle.Continuous;
                        break;
                    case ConnectorStatus.Connected:
                        this.barcolor = Color.Transparent;
                        this.BackColor = Color.Lime;
                        this.timer.Enabled = false;
                        this.Style = ProgressBarStyle.Continuous;
                        break;
                    default:
                        this.BackText = string.Format("Not Connected!");
                        this.barcolor = Color.Transparent;
                        this.BackColor = Color.Red;
                        this.textcolor = Color.Black;
                        this.Style = ProgressBarStyle.Blocks;
                        break;
                }
                this.Invalidate();
            }
        }
        #endregion


        #region Properties...
        [ToolboxItem("System.Int32")]
        public new int Maximum
        {
            get{ return base.Maximum; }
            set
            {
                // Make sure that the maximum value is never set lower than the minimum value.
                if (value < base.Minimum)
                {
                    base.Minimum = value;
                }

                base.Maximum = value;

                // Make sure that value is still in range.
                if (this.Value > base.Maximum)
                {
                    this.Value = base.Maximum;
                }

                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }


        [ToolboxItem("System.Int32")]
        public new int Minimum
        {
            get{ return base.Minimum; }
            set
            {
                // Prevent a negative value.
                if (value < 0)
                {
                    base.Minimum = 0;
                }

                // Make sure that the minimum value is never set higher than the maximum value.
                if (value > base.Maximum)
                {
                    base.Minimum = value;
                }

                // Ensure value is still in range
                if (base.Value < base.Minimum)
                {
                    base.Value = base.Minimum;
                }
                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }


        [ToolboxItem("System.Int32")]
        public new int Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                int oldValue = base.Value;

                // Make sure that the value does not stray outside the valid range.
                if (value < base.Minimum)
                {
                    base.Value = base.Minimum;
                }
                else if (value > base.Maximum)
                {
                    base.Value = base.Maximum;
                }
                else
                {
                    base.Value = value;
                }

                // Invalidate only the changed area.
                float percent;

                Rectangle newValueRect = this.ClientRectangle;
                Rectangle oldValueRect = this.ClientRectangle;

                // Use a new value to calculate the rectangle for progress.
                percent = (float)(base.Value - base.Minimum) / (float)(Maximum - Minimum);
                newValueRect.Width = (int)((float)newValueRect.Width * percent);

                // Use an old value to calculate the rectangle for progress.
                percent = (float)(oldValue - Minimum) / (float)(Maximum - Minimum);
                oldValueRect.Width = (int)((float)oldValueRect.Width * percent);

                Rectangle updateRect = new Rectangle();

                // Find only the part of the screen that must be updated.
                if (newValueRect.Width > oldValueRect.Width)
                {
                    updateRect.X = oldValueRect.Size.Width;
                    updateRect.Width = newValueRect.Width - oldValueRect.Width;
                }
                else
                {
                    updateRect.X = newValueRect.Size.Width;
                    updateRect.Width = oldValueRect.Width - newValueRect.Width;
                }

                updateRect.Height = this.Height;

                //Invalidate the intersection region only.
                this.Invalidate(updateRect);

                //this.Invalidate();
            }
        }

        [ToolboxItem("System.String")]
        public string BackText
        {
            get { return this.text; }
            set { this.text = value; }
        }

        [ToolboxItem("System.Drawing.Color")]
        public Color BarColor
        {
            get { return this.barcolor; }
            set { this.barcolor = value; this.Invalidate(); }
        }

        [ToolboxItem("System.Drawing.Color")]
        public Color TextColor
        {
            get { return this.textcolor; }
            set { this.textcolor = value; this.Invalidate(); }
        }
        #endregion
    }

    /// <summary>
    /// ConnectorStatus Events arguments
    /// </summary>
    public class ConnectorStatusEventArgs
    {
        ConnectorStatus status;
        float percent;

        public ConnectorStatusEventArgs(ConnectorStatus status, float percent)
        {
            this.status = status;
            this.percent = percent;
        }

        public ConnectorStatusEventArgs(ConnectorStatus status)
        {
            this.status = status;
            this.percent = 0;
        }

        public ConnectorStatusEventArgs(float percent)
        {
            this.status = ConnectorStatus.Monitoring;
            this.percent = percent;
        }

        public ConnectorStatus Status
        {
            get { return this.status; }
        }

        public float Percent
        {
            get { return this.percent; }
        }
    }
}