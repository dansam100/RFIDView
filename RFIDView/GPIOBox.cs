using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;

namespace RFIDView
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class GPIOBox : UserControl
    {
        private Color upperColor, bottomColor;
        private Corners boxCorners, titleBoxCorners;
        private string title;
        private float titlewidth;

        private Panel body;

        public GPIOBox()
        {
            upperColor = SystemColors.Control;
            bottomColor = SystemColors.ControlLight;
            title = "No Title";
            body = new Panel();

            InitializeComponent();
        }

        #region Overrides...
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    ////body.Bounds = this.DisplayRectangle;
        //    ////body.BackColor = Color.Transparent;
        //    ////base.Controls.Add(body);
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    this.body.Bounds = this.DisplayRectangle;
        //}

        //protected new ControlCollection Controls
        //{
        //    get { return this.body.Controls; }
        //}

        protected override void OnControlAdded(ControlEventArgs e)
        {
            //if (e.Control.GetHashCode() != Body.GetHashCode())
            //    Body.Controls.Add(e.Control);
            //else
            //    base.OnControlAdded(e);

            if (e.Control.Bounds.Top < this.DisplayRectangle.Top)
                e.Control.Location = new Point(e.Control.Location.X, this.DisplayRectangle.Top);
            base.OnControlAdded(e);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle client = this.ClientRectangle;
                Rectangle rect = new Rectangle(client.Left,
                    client.Top + TitleBox.Height, client.Right - 1,
                        client.Height - (TitleBox.Height + 1));
                return rect;
            }
        }
        #endregion

        #region Paint Override...
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            SolidBrush br = new SolidBrush(Color.Transparent);
            pevent.Graphics.FillPath(br, Rounder.GetRoundedBounds(this.ClientRectangle, Corners.None));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle titlebox = this.TitleBox;

            int halfheight = titlebox.Height / 2;

            Rectangle rect = new Rectangle(this.ClientRectangle.Left ,
                this.ClientRectangle.Top + halfheight, this.ClientRectangle.Right - 1,
                    this.ClientRectangle.Height - (halfheight + 1) );

            GraphicsPath path = Rounder.GetRoundedBounds(rect, BoxCorners);

            titlebox.Location = new Point(rect.Left + 15, rect.Top - titlebox.Height / 2);
            GraphicsPath titlepath = Rounder.GetRoundedBounds(titlebox, TitleCorners);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.Alignment = StringAlignment.Near;

            Pen borderPen = new Pen(Color.Black);
            LinearGradientBrush patternBrush = new LinearGradientBrush(rect, upperColor, bottomColor,
                LinearGradientMode.Vertical);

            LinearGradientBrush titleBrush = new LinearGradientBrush(titlebox, upperColor, bottomColor,
                LinearGradientMode.Vertical);

            //paint
            g.FillPath(patternBrush, path);
            g.DrawPath(borderPen, path);



            g.FillPath(titleBrush, titlepath);
            g.DrawPath(borderPen, titlepath);

            g.DrawString(this.title, this.Font, SystemBrushes.ControlText, titlebox.Location, sf);

            //destroy
            patternBrush.Dispose();
            borderPen.Dispose();
            g.Dispose();
        }


        private Rectangle GetStringBounds(Graphics g, string text)
        {
            SizeF sizef = g.MeasureString(text, this.Font);

            if (titlewidth > sizef.Width)
                sizef = new SizeF(titlewidth, sizef.Height);

            Rectangle strBounds = new Rectangle(0, 0, (int)sizef.Width, (int)sizef.Height);
            return strBounds;
        }
        #endregion

        #region Designer Properties...
        [ToolboxItem("System.Drawing.Color")]
        public Color UpperColor
        {
            get { return this.upperColor; }
            set { this.upperColor = value; this.Invalidate(); }
        }

        [ToolboxItem("System.Drawing.Color")]
        public Color BottomColor
        {
            get { return this.bottomColor; }
            set { this.bottomColor = value; this.Invalidate(); }
        }

        [ToolboxItem("RFIDView.Corners")]
        public Corners TitleCorners
        {
            get { return this.titleBoxCorners; }
            set { this.titleBoxCorners = value; this.Invalidate(); }
        }

        [ToolboxItem("RFIDView.Corners")]
        public Corners BoxCorners
        {
            get { return this.boxCorners; }
            set { this.boxCorners = value; this.Invalidate(); }
        }

        [ToolboxItem("System.String")]
        public string Title
        {
            get { return this.title; }
            set { this.title = value; this.Invalidate(); }
        }

        #endregion

        //[ToolboxItem("System.Single")]
        public float TitleWidth
        {
            get { return this.titlewidth; }
            set
            {
                if (value < GetStringBounds(this.CreateGraphics(), string.Empty).Width)
                    value = GetStringBounds(this.CreateGraphics(), string.Empty).Width;
                this.titlewidth = value;
            }
        }


        #region Properties...
        private Panel Body
        {
            get { return this.body; }
        }

        protected Rectangle TitleBox
        {
            get
            {
                Rectangle titlebox = this.GetStringBounds(this.CreateGraphics(), this.title);
                Rectangle rect = this.ClientRectangle;

                titlebox.Location = new Point(rect.Left + 15, rect.Top + titlebox.Height / 2);

                return titlebox;
            }
        }
        #endregion
    }
}