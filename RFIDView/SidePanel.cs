using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using RFIDView.Properties;

namespace RFIDView
{
    public enum ControlSize
    {
        Small,
        Medium,
        Large,
        Dock
    }

    public delegate void FilterTextChanged(object sender, string text);
    
    public partial class SidePanel : Panel
    {
        //List<SideControl> sideControls = null;

        Dictionary<Rectangle, ControlDefinition> cursorPoints = new Dictionary<Rectangle, ControlDefinition>();
        Dictionary<Rectangle, ControlDefinition> filterPoints = new Dictionary<Rectangle, ControlDefinition>();

        ControlDefinition vErrorControl, vEventsControl, clearFilterControl;
        public event ControlHandler ControlClicked;

        private bool viewEvents = true;
        private string currentFilter = string.Empty;

        private Viewer viewer;

        public SidePanel()
        {
            InitializeComponent();
            //this.sideControls = new List<SideControl>();
            vErrorControl = new ControlDefinition(typeof(LinkArea), "View Errors", null);
            vEventsControl = new ControlDefinition(typeof(LinkArea), "View Events", null);
            clearFilterControl = new ControlDefinition(typeof(Button), "Clear Filter", ClearFilterClicked);
        }


        private void ClearFilterClicked(object sender, ControlHandlerEventArgs e)
        {
            if(this.textBox.TextLength > 0)
                this.textBox.Text = string.Empty;
        }

        #region Paint
        //void parent_Paint(object sender, PaintEventArgs e)
        //{
        //    if (!DesignMode)
        //    {
        //        Graphics g = e.Graphics;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //        LinearGradientBrush brush = new LinearGradientBrush(e.ClipRectangle, SystemColors.ControlDarkDark,
        //            SystemColors.ControlDark, LinearGradientMode.Vertical);

        //        g.FillRectangle(brush, e.ClipRectangle);
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                GraphicsPath path = Rounder.GetRoundedBounds(this.Bounds, Corners.BottomLeft | Corners.TopLeft);

                LinearGradientBrush brush = new LinearGradientBrush(this.Bounds, SystemColors.ControlDark,
                    Color.White, LinearGradientMode.Horizontal);

                //LinearGradientBrush brush = new LinearGradientBrush(this.Bounds, Color.FromArgb(0, 191, 255), 
                //    Color.LightBlue, LinearGradientMode.Horizontal);

                Pen pen = new Pen(new SolidBrush(Color.White));

                g.DrawPath(pen, path);
                g.FillPath(brush, path);

                pen.Dispose();
                brush.Dispose();

                //int vOffset = this.Bounds.Bottom * 1 / 8;
                int vOffset = 80;
                int hOffset = this.Bounds.Right * 1 / 10;
                int width = this.Bounds.Right * 4 / 5;
                int length = this.Bounds.Bottom * 1 / 7;

                Rectangle bannerBounds = new Rectangle(hOffset, 10, width, 40);
                Rectangle displayBounds = new Rectangle(hOffset, vOffset, width, 70);
                //Rectangle filterBounds = new Rectangle(hOffset, 30 + displayBounds.Bottom, width, 2 * (length - 15));
                Rectangle filterBounds = new Rectangle(hOffset, 30 + displayBounds.Bottom, width, 60);

                MakeSideBanner(bannerBounds, e);
                if (Connector.Connected)
                {
                    MakeSideDisplayPanel(displayBounds, e);
                    MakeSideFilterPanel(filterBounds, e);
                }
                else this.textBox.Visible = false;
                this.textBox.Invalidate();
            }
        }

        private void MakeSideBanner(Rectangle bounds, PaintEventArgs e)
        {
            GraphicsPath path = Rounder.GetRoundedBounds(bounds, Corners.All);
            Graphics g = e.Graphics;
            Pen pen = new Pen(new SolidBrush(Color.White));
            g.SmoothingMode = SmoothingMode.HighQuality;
            Bitmap image = new Bitmap(Resources.logo, bounds.Size);
            g.DrawImage((Image)image, bounds);

            pen = new Pen(new SolidBrush(Color.Red));
            g.DrawRectangle(pen, bounds);
        }

        private void MakeSideDisplayPanel(Rectangle bounds, PaintEventArgs e)
        {
            GraphicsPath path = Rounder.GetRoundedBounds(bounds, Corners.All);
            Graphics g = e.Graphics;
            Pen pen = new Pen(new SolidBrush(Color.White));
            LinearGradientBrush brush = new LinearGradientBrush(this.Bounds,
                SystemColors.ControlLight, SystemColors.ControlDark, LinearGradientMode.Vertical);

            int bannerOffset = 20;
            int hstart = bounds.Left + bounds.Width / 8;
            int width = bounds.Width / 12;
            int shiftlength = 12;
            int vstart = bounds.Top + 20 + bounds.Height * 1 / 8;

            Font smallFont = new Font(FontFamily.GenericSerif, 9, FontStyle.Bold);

            Point[] square1 = new Point[]{
                        new Point(hstart, vstart),
                        new Point(hstart + width, vstart),
                        new Point(hstart + width, vstart + width),
                        new Point(hstart, vstart + width)
                    };

            //Point vEvents = new Point((bounds.Right + hstart + 5) / 2, vstart + width / 2 );
            Point vEvents = new Point(hstart + (2 * width), vstart + width / 2);
            Rectangle vEventBounds = new Rectangle(hstart - 2, vstart - width, bounds.Width - hstart, 2 * width);

            vstart += width + shiftlength;

            Point[] square2 = new Point[]{
                        new Point(hstart, vstart),
                        new Point(hstart + width, vstart),
                        new Point(hstart + width, vstart + width),
                        new Point(hstart, vstart + width)
                    };

            //Point vErrors = new Point((bounds.Right + hstart + 5) / 2, vstart + width / 2);
            Point vErrors = new Point(hstart + (2 * width), vstart + width / 2);
            Rectangle vErrorBounds = new Rectangle(hstart - 2, vstart - width, bounds.Width - hstart, 2 * width);

            GraphicsPath square1Path = new GraphicsPath();
            GraphicsPath square2Path = new GraphicsPath();

            path.StartFigure();
            path.AddLine(bounds.Left, bounds.Top + bannerOffset, bounds.Right, bounds.Top + bannerOffset);
            path.CloseFigure();

            square1Path.StartFigure();
            square1Path.AddLines(square1);
            square2Path.CloseFigure();

            square2Path.StartFigure();
            square2Path.AddLines(square2);
            square2Path.CloseFigure();

            cursorPoints.Clear();
            cursorPoints.Add(vEventBounds, vEventsControl);
            cursorPoints.Add(vErrorBounds, vErrorControl);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Rectangle banner = new Rectangle(bounds.Location, new Size(bounds.Width - 5, bannerOffset));
            Point atPoint = new Point(banner.Width * 2 / 3, banner.Bottom - 10);

            g.FillPath(brush, path);
            g.DrawPath(pen, path);

            //g.DrawString("Display", this.Font, new SolidBrush(Color.White), atPoint, sf);

            g.FillPath(new LinearGradientBrush(square1[0], square1[1], SystemColors.ControlLight,
                SystemColors.ControlLightLight), square1Path);

            g.DrawString("Display", this.Font, new SolidBrush(Color.Black), atPoint, sf);

            g.FillPath(new LinearGradientBrush(square2[0], square2[1], SystemColors.ControlLight,
                SystemColors.ControlLightLight), square2Path);

            if (viewEvents)
            {
                g.FillPath(new LinearGradientBrush(square1[0], square1[1], Color.Red,
                    Color.Blue), square1Path);
            }
            else
            {
                g.FillPath(new LinearGradientBrush(square2[0], square2[1], Color.Red,
                    Color.Blue), square2Path);
            }

            sf.Alignment = StringAlignment.Near;
            g.DrawPath(pen, square1Path);
            g.DrawString("View Events", smallFont, new SolidBrush(Color.Black), vEvents, sf);

            g.DrawPath(pen, square2Path);
            g.DrawString("View Errors", smallFont, new SolidBrush(Color.Black), vErrors, sf);
        }

        /// <summary>
        /// Make the filter panel on the left hand side
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="e"></param>
        private void MakeSideFilterPanel(Rectangle bounds, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();

            Graphics g = e.Graphics;
            Pen pen = new Pen(new SolidBrush(Color.Black));
            LinearGradientBrush brush = new LinearGradientBrush(this.Bounds,
                SystemColors.ControlLightLight, SystemColors.ControlLight, LinearGradientMode.Vertical);

            int bannerOffset = 20;
            int hstart = bounds.Left + bounds.Width / 8;
            int width = bounds.Width / 12;
            int shiftlength = 10;
            int itemsize = width + 17;
            int vstart = bounds.Top + 20 + width;

            if (viewer.Columns.Count > 0)
            {
                bounds.Height = bounds.Height + ((viewer.Columns.Count - 1) * (itemsize));
            }
            path = Rounder.GetRoundedBounds(bounds, Corners.All);

            path.StartFigure();
            path.AddLine(bounds.Left, bounds.Top + bannerOffset, bounds.Right, bounds.Top + bannerOffset);
            path.CloseFigure();

            Font smallFont = new Font(FontFamily.GenericSerif, 9, FontStyle.Bold);

            /*
            Point vFilter = new Point(bounds.Left, vstart + width / 2);
            Rectangle vFilterBounds = new Rectangle(hstart + 25, vstart - 5, bounds.Width * 2/3, 2 * width);

            vstart += width + shiftlength;

            Point vBy = new Point(bounds.Left, vstart + width + 5);
            Rectangle vByBounds = new Rectangle(hstart + 10, vstart, bounds.Width * 2/3, 2 * width);
            */

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Rectangle banner = new Rectangle(bounds.Location, new Size(bounds.Width - 5, bannerOffset));

            Point atPoint = new Point(banner.Width * 2 / 3, banner.Bottom - 10);

            g.FillPath(brush, path);
            g.DrawPath(pen, path);

            g.DrawString("Filters", this.Font, new SolidBrush(this.ForeColor), atPoint, sf);

            sf.Alignment = StringAlignment.Near;
            filterPoints.Clear();

            if (viewer.Columns.Count > 0)
            {
                if (currentFilter == string.Empty) this.currentFilter = viewer.Columns[0];
                foreach (string name in viewer.Columns)
                {
                    Point[] square = new Point[]{
                        new Point(hstart, vstart),
                        new Point(hstart + width, vstart),
                        new Point(hstart + width, vstart + width),
                        new Point(hstart, vstart + width)
                    };
                    GraphicsPath squarePath = new GraphicsPath();

                    Point vName = new Point(hstart + (2 * width), vstart + width / 2);
                    Rectangle vsquareBounds = new Rectangle(hstart - 2, vstart - width, bounds.Right - hstart, 2 * width);

                    path.StartFigure();
                    squarePath.AddLines(square);
                    path.CloseFigure();

                    g.FillPath(new LinearGradientBrush(square[0], square[1], SystemColors.ControlLight,
                    SystemColors.ControlLightLight), squarePath);

                    if (name == currentFilter)
                    {
                        g.FillPath(new LinearGradientBrush(square[0], square[1], Color.Red,
                            Color.Blue), squarePath);
                    }

                    g.DrawPath(pen, squarePath);
                    g.DrawString(name, smallFont, new SolidBrush(Color.Black), vName, sf);

                    filterPoints.Add(vsquareBounds, new ControlDefinition(typeof(LinkArea), name, null));

                    vstart += width + shiftlength;
                }
            }

            //vstart += width + shiftlength;

            Rectangle tBoxBounds = new Rectangle(hstart - 2, vstart, bounds.Right - hstart - 20, 30);
            Rectangle cBounds = new Rectangle(hstart + tBoxBounds.Width, vstart, 16, 20);

            this.textBox.Bounds = tBoxBounds;
            this.textBox.Visible = true;

            Bitmap bitmap = new Bitmap(Resources.redx, cBounds.Size);
            g.DrawImage((Image)bitmap, cBounds);

            this.cursorPoints.Add(cBounds, clearFilterControl);
            /*
            sf.Alignment = StringAlignment.Near;
            g.DrawString("Filter:", smallFont, new SolidBrush(Color.Black), vFilter, sf);

            sf.LineAlignment = StringAlignment.Far;
            g.DrawString("By:", smallFont, new SolidBrush(Color.Black), vBy, sf);

            this.comboBox.Bounds = vFilterBounds;
            this.comboBox.Visible = true;
            this.textBox.Bounds = vByBounds;
            this.textBox.Visible = true;
            */
        }
        #endregion

        #region MouseOverrides
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool changedcursor = false;
            if (Connector.Connected)
            {
                foreach (KeyValuePair<Rectangle, ControlDefinition> kvp in this.cursorPoints)
                {
                    if (kvp.Key.Contains(e.Location))
                    {
                        this.Cursor = Cursors.Hand;
                        changedcursor = true;
                        break;
                    }
                    else
                        this.Cursor = Cursors.Default;
                }
                if (!changedcursor)
                {
                    foreach (KeyValuePair<Rectangle, ControlDefinition> kvp in this.filterPoints)
                    {
                        if (kvp.Key.Contains(e.Location))
                        {
                            this.Cursor = Cursors.Hand;
                            break;
                        }
                        else
                            this.Cursor = Cursors.Default;
                    }
                }
            }
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Connector.Connected)
            {                
                foreach (KeyValuePair<Rectangle, ControlDefinition> kvp in this.cursorPoints)
                {
                    if (kvp.Key.Contains(e.Location))
                    {
                        if (this.ControlClicked != null)
                        {
                            if (kvp.Value.Label == "View Errors")
                            {
                                currentFilter = (viewEvents) ? string.Empty : currentFilter;
                                viewEvents = false;
                            }
                            else
                            {
                                currentFilter = (viewEvents) ? currentFilter : string.Empty;
                                viewEvents = true;
                            }

                            if (kvp.Value.ControlHandler != null)
                                kvp.Value.ControlHandler(this, null);

                            this.Invalidate();
                            this.ControlClicked(this, new ControlHandlerEventArgs(kvp.Value, null));
                            this.textBox.Clear();
                        }
                        break;
                    }
                }
                foreach (KeyValuePair<Rectangle, ControlDefinition> kvp in this.filterPoints)
                {
                    if (kvp.Key.Contains(e.Location))
                    {
                        currentFilter = kvp.Value.Label;
                        this.Invalidate();
                        if (this.ControlClicked != null)
                        {
                            this.ControlClicked(this, new ControlHandlerEventArgs(kvp.Value, null));
                        }
                        this.textBox.InvokeFilterChanged();
                        break;
                    }
                }
            }
        }
        #endregion

        #region junk
        //public List<SideControl> SideControls
        //{
        //    get { return this.sideControls; }
        //    set { this.sideControls = value; }
        //}

        //public void AddSideControl(SideControl control, ControlSize size)
        //{
        //    int vOffset = this.Bounds.Bottom * 1 / 10;
        //    int hOffset = this.Bounds.Right * 1 / 10;
        //    int width = this.Bounds.Right * 4 / 5;
        //    int length = this.Bounds.Bottom * 1 / 6;

        //    switch (size)
        //    {
        //        case ControlSize.Small:
        //            length = this.Bounds.Bottom * 1 / 9;
        //            break;
        //        case ControlSize.Large:
        //            length = 2 * length;
        //            break;
        //        case ControlSize.Medium:
        //            length = this.Bounds.Bottom * 1 / 6;
        //            break;
        //        case ControlSize.Dock:
        //            control.Dock = DockStyle.Fill;
        //            break;
        //    }


        //    Rectangle displayBounds = new Rectangle(hOffset, controlStart, width, length);
        //    control.Bounds = displayBounds;

        //    controlStart += vOffset + displayBounds.Bottom;

        //    this.sideControls.Add(control);
        //    this.Controls.Add(control);
        //}
        #endregion

        #region Properties
        /// <summary>
        /// Selected filter
        /// </summary>
        public string Filter
        {
            get { return this.currentFilter; }
        }

        public event FilterTextChanged FilterChanged
        {
            add{ this.textBox.FilterChanged += value; }
            remove { this.textBox.FilterChanged -= value; }
        }
        #endregion

        internal void SetParent(Viewer rFIDViewUI)
        {
            this.viewer = rFIDViewUI;
        }
    }
}