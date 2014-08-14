namespace RFIDView
{
    partial class GPIOPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpioBox1 = new RFIDView.GPIOBox();
            this.SuspendLayout();
            // 
            // gpioBox1
            // 
            this.gpioBox1.BackColor = System.Drawing.Color.Transparent;
            this.gpioBox1.BottomColor = System.Drawing.SystemColors.ControlLight;
            this.gpioBox1.BoxCorners = RFIDView.Corners.None;
            this.gpioBox1.Location = new System.Drawing.Point(163, 24);
            this.gpioBox1.Name = "gpioBox1";
            this.gpioBox1.Size = new System.Drawing.Size(150, 150);
            this.gpioBox1.TabIndex = 0;
            this.gpioBox1.Title = "No Title";
            this.gpioBox1.TitleCorners = RFIDView.Corners.None;
            this.gpioBox1.TitleWidth = 0F;
            this.gpioBox1.UpperColor = System.Drawing.SystemColors.Control;
            // 
            // GPIOPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 186);
            this.Controls.Add(this.gpioBox1);
            this.Name = "GPIOPanel";
            this.Text = "GPIOPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private GPIOBox gpioBox1;




















    }
}