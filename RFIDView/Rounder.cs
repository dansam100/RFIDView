using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace RFIDView
{
    [FlagsAttribute]
    public enum Corners
    {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = BottomLeft | BottomRight | TopLeft | TopRight
    }

    class Rounder
    {
        public static GraphicsPath GetRoundedBounds(Rectangle bounds, Corners corners)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = bounds.Width * 1 / 10;

            if (radius == 0)
                radius = 2;

            path.StartFigure();

            if (corners == Corners.None)
            {
                path.AddLine(bounds.Left, bounds.Top,
                    bounds.Right, bounds.Top); //top edge

                path.AddLine(bounds.Right, bounds.Top,
                    bounds.Right, bounds.Bottom); //right edge

                path.AddLine(bounds.Right, bounds.Bottom,
                    bounds.Left, bounds.Bottom); //bottom edge

                path.AddLine(bounds.Left, bounds.Bottom,
                    bounds.Left, bounds.Top); //left edge
            }
            else
            {
                if ((corners & Corners.TopLeft) == Corners.TopLeft)
                {
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90); //top left corner
                }
                else
                {
                    path.AddLine(bounds.Left, bounds.Top + radius, bounds.Left, bounds.Top); //left side edge
                    path.AddLine(bounds.Left, bounds.Top, bounds.Left + radius, bounds.Top); //top side edge
                }
                path.AddLine(bounds.Left + radius, bounds.Top, bounds.Right - radius, bounds.Top); //top edge


                if ((corners & Corners.TopRight) == Corners.TopRight)
                {
                    path.AddArc(bounds.Right - radius, bounds.Top, radius, radius, 270, 90); //top right corner
                }
                else
                {
                    path.AddLine(bounds.Right - radius, bounds.Top, bounds.Right, bounds.Top); //right edge
                    path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top + radius); //right edge
                }
                path.AddLine(bounds.Right, bounds.Top + radius, bounds.Right, bounds.Bottom - radius); //right edge

                if ((corners & Corners.BottomRight) == Corners.BottomRight)
                {
                    path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90); //bottom right corner
                }
                else
                {
                    path.AddLine(bounds.Right, bounds.Bottom - radius, bounds.Right, bounds.Bottom); //bottom edge
                    path.AddLine(bounds.Right, bounds.Bottom, bounds.Right - radius, bounds.Bottom); //bottom edge
                }

                path.AddLine(bounds.Right - radius, bounds.Bottom, bounds.Left + radius, bounds.Bottom); //bottom edge

                if ((corners & Corners.BottomLeft) == Corners.BottomLeft)
                {
                    path.AddArc(bounds.Left, bounds.Bottom - radius, radius, radius, 90, 90); //bottom left corner
                }
                else
                {
                    path.AddLine(bounds.Left + radius, bounds.Bottom, bounds.Left, bounds.Bottom); //left bottom edge
                    path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom - radius); //left bottom edge
                }

                path.AddLine(bounds.Left, bounds.Bottom - radius, bounds.Left, bounds.Top + radius); //left edge
            }
            path.CloseFigure();

            return path;
        }
    }
}
