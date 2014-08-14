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
    public delegate void ControlHandler(object sender, ControlHandlerEventArgs e);
    

    public class ControlHandlerEventArgs
    {
        private object[] parameters;
        private ControlDefinition definition;

        public ControlHandlerEventArgs(ControlDefinition definition, params object[] parameters)
        {
            this.definition = definition;
            this.parameters = parameters;
        }

        public object[] Parameters
        {
            get{ return this.parameters;}
        }

        public ControlDefinition Definition
        {
            get { return this.definition; }
        }
    }

    public struct ControlDefinition
    {
        private string label;
        private Type type;
        private ControlHandler controlHandler;
        
        public ControlDefinition(Type type, string label, ControlHandler handler)
        {
            this.label = label;
            this.type = type;
            this.controlHandler = handler;
        }

        public string Label
        {
            get { return this.label; }
        }

        public Type Type
        {
            get { return this.type; }
        }

        public ControlHandler ControlHandler
        {
            get { return controlHandler; }
            set { controlHandler = value; }
        }
    }
    


    public partial class SideControl : Panel
    {
        private string header = "No Header";
        private ControlDefinition[] definitions;
        private Color borderColor = Color.Black;
        private Color mainColor = SystemColors.ControlLight;
        private Color fadeColor = SystemColors.ControlDark;
        private Color fontColor = Color.White;

        public SideControl()
        {
            InitializeComponent();

            //this.displayControl = new SideControl();
            //this.filterControl = new SideControl();

            ////
            ////displayControl
            ////
            //this.displayControl.Header = "Display";
            //this.displayControl.Definitions = new ControlDefinition[]{
            //    new ControlDefinition(typeof(RadioButton), "View Events"),
            //    new ControlDefinition(typeof(RadioButton), "View Errors")
            //};

            ////
            ////filterControl
            ////
            //this.filterControl.Header = "Filters";
            //this.filterControl.Definitions = new ControlDefinition[]{
            //    new ControlDefinition(typeof(ComboBox), "Filter Column: "),
            //    new ControlDefinition(typeof(TextBox), "By: ")
            //};

            //this.sidePanel.AddSideControl(this.displayControl, ControlSize.Medium);
            //this.sidePanel.AddSideControl(this.filterControl, ControlSize.Medium);
        }


        public void CreateControl(String header, params ControlDefinition[] definitions)
        {
            this.header = header;
            this.definitions = definitions;
            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!this.DesignMode)
            {
                Rectangle bounds = this.Bounds;

                Graphics g = e.Graphics;
                Pen pen = new Pen(new SolidBrush(this.borderColor));
                LinearGradientBrush brush = new LinearGradientBrush(this.Bounds,
                    SystemColors.ControlLight, SystemColors.ControlDark, LinearGradientMode.Vertical);

                int bannerOffset = 20;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle banner = new Rectangle(bounds.Location, new Size(bounds.Width - 5, bannerOffset));
                Rectangle bannerText = new Rectangle(bounds.X + 5, bounds.Y + 5, banner.Width, bannerOffset - 5);

                Rectangle body = new Rectangle(new Point(banner.Left, banner.Bottom),
                    new Size(bounds.Width - 5, bounds.Bottom - banner.Bottom)); 

                Point atPoint = new Point(bannerText.Width / 2, bannerText.Bottom * 2/3);

                GraphicsPath path = Rounder.GetRoundedBounds(banner, Corners.None);
                GraphicsPath path2 = Rounder.GetRoundedBounds(body, Corners.None);

                //path.AddPath(path2, true);

                g.FillPath(brush, path);
                g.DrawPath(pen, path);

                g.DrawString(header, this.Font, new SolidBrush(this.fontColor), atPoint, sf);
                g.Dispose();
            }
        }

        //Properties
        [ToolboxItem("Definitions")]
        public ControlDefinition[] Definitions
        {
            get { return this.definitions; }
            set { this.definitions = value; }
        }


        [ToolboxItem("Header")]
        public string Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        [ToolboxItem("BorderColor")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        [ToolboxItem("MainColor")]
        public Color MainColor
        {
            get { return this.mainColor; }
            set { this.mainColor = value; }
        }

        [ToolboxItem("FadeColor")]
        public Color FadeColor
        {
            get { return this.fadeColor; }
            set { this.fadeColor = value; }
        }

        [ToolboxItem("FontColor")]
        public Color FontColor
        {
            get { return this.fontColor; }
            set { this.fontColor = value; }
        }
    }
}