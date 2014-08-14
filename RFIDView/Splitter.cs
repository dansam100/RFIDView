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
    public partial class Splitter : SplitContainer
    {
        private Boolean EnteredFocus = false;
        
        public Splitter()
        {
            InitializeComponent();
        }

        #region Paint
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                LinearGradientBrush br = new LinearGradientBrush(
                            this.Bounds,
                            SystemColors.ControlDark,
                            SystemColors.ControlLight,
                            LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(br, this.Bounds);
                br.Dispose();
            }
            else
            {
                if (this.Parent != null)
                {
                    Rectangle clientRect = e.ClipRectangle;
                    GraphicsState state = e.Graphics.Save();
                    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    try
                    {
                        e.Graphics.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                        this.InvokePaintBackground(this.Parent, e);
                        this.InvokePaint(this.Parent, e);
                    }
                    finally
                    {
                        e.Graphics.Restore(state);
                    }
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                LinearGradientBrush brush = new LinearGradientBrush(this.SplitterRectangle, Color.White,
                    SystemColors.Control, LinearGradientMode.Horizontal);

                //LinearGradientBrush brush = new LinearGradientBrush(this.SplitterRectangle, Color.FromArgb(135,206,250),
                //    Color.FromArgb(224, 255, 255), LinearGradientMode.Horizontal);

                Pen borderpen = new Pen(new SolidBrush(Color.White));

                Pen pen = new Pen(new SolidBrush(Color.OrangeRed));

                GraphicsPath path = Rounder.GetRoundedBounds(this.SplitterRectangle, Corners.All);

                g.FillPath(brush, path);

                g.DrawLine(borderpen, new Point(SplitterRectangle.Left, SplitterRectangle.Top),
                    new Point(SplitterRectangle.Right, SplitterRectangle.Top) );
                g.DrawLine(borderpen, new Point(SplitterRectangle.Right, SplitterRectangle.Top),
                    new Point(SplitterRectangle.Right, SplitterRectangle.Bottom) );
                g.DrawLine(borderpen, new Point(SplitterRectangle.Right, SplitterRectangle.Bottom),
                    new Point(SplitterRectangle.Left, SplitterRectangle.Bottom) );

                if (this.EnteredFocus)
                {
                    Rectangle bounds = this.SplitterRectangle;
                    int athird = bounds.Height / 3;
                    int shiftlength = bounds.Width * 2 / 5;
                    int twothird = bounds.Height * 2 / 3;
                    int mid = (athird + twothird) / 2;

                    Point[] expandtriangle = new Point[]{
                        new Point(bounds.Right - (shiftlength), mid),
                        new Point(bounds.Right - (2 * shiftlength), mid - 5),
                        new Point(bounds.Right - (2 * shiftlength), mid + 5)
                    };

                    Point[] collapsetriangle = new Point[]{
                        new Point(bounds.Right - (2 * shiftlength), mid),
                        new Point(bounds.Right - (shiftlength), mid - 5),
                        new Point(bounds.Right - (shiftlength), mid + 5)
                    };

                    GraphicsPath p = new GraphicsPath();

                    p.StartFigure();

                    if (this.SplitterDistance == 0)
                    {
                        p.AddLines(expandtriangle);
                    }
                    else
                    {
                        p.AddLines(collapsetriangle);
                    }
                    p.CloseFigure();
                    g.FillPath(pen.Brush, p);
                    p.Reset();

                    p = this.GetCollapseLine(this.SplitterRectangle);
                    
                    g.DrawPath(pen, p);
                }

                pen.Dispose();
                brush.Dispose();
            }
        }


        private GraphicsPath GetCollapseLine(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            int athird = bounds.Height / 3;
            int shiftlength = bounds.Width * 1 / 5;
            int shift = bounds.Right - (shiftlength);
            int twothird = bounds.Height * 2 / 3;

            path.StartFigure();

            path.AddLine(shift, athird, shift, twothird);

            path.CloseFigure();

            return path;
        }
        #endregion

        #region MouseEvents
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            this.EnteredFocus = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            this.EnteredFocus = false;
            this.Invalidate();
        }


        void Splitter_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SplitterDistance == 0)
            {
                this.IsSplitterFixed = false;
                this.SplitterDistance = this.Parent.Width * 1 / 6;
                this.IsSplitterFixed = true;
            }
            else
            {
                this.IsSplitterFixed = false;
                this.SplitterDistance = 0;
                this.IsSplitterFixed = true;
            }
        }
        #endregion

        #region MenuStrip
        void menuStrip_Collapse(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                this.Splitter_MouseClick(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                item.Text = (item.Text.CompareTo("Collapse") == 0) ? "Expand" : "Collapse";
            }
        }
        #endregion
    }
}