using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LensSystem
{
    class Image:Subject
    {
        bool Real;

        public Image(bool R, int PX1 = 80, int PY1 = 50, int PX2 = 60, int PY2 = 0, bool OU = true, bool I = false)
            : base(PX1, PY1, PX2, PY2, OU, I)
        {
            Real = R;
        }

        public void DrawImage(Panel MainPanel, PaintEventArgs e, int IsFinal)
        {
            int Height = MainPanel.Size.Height;
            int Axis = Height / 2;

            Graphics gr = e.Graphics;

            Pen Pen1 = new Pen(Color.DimGray, 3);
            if (IsFinal == 1)
                Pen1 = new Pen(Color.Red, 3);

            if (Real)
                Pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                Pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            Point Top, Bottom, LeftTop, RightTop;
            int OldLeftX, OldLeftY, OldRightX, OldRightY;
            int Size = (int)(Math.Sqrt(Math.Pow(PositionTopX - PositionBottomX, 2) + Math.Pow(PositionTopY - PositionBottomY, 2)));
            double Tetta = Math.Atan(- (double)(PositionTopX - PositionBottomX) / (PositionTopY - PositionBottomY));

            if (OrientationUp)
            {
                Top = new Point(PositionTopX, PositionTopY);
                Bottom = new Point(PositionBottomX, PositionBottomY);
            }
            else
            {
                Top = new Point(PositionBottomX, PositionBottomY);
                Bottom = new Point(PositionTopX, PositionTopY);
            }

            OldLeftX = Size / 5;
            OldLeftY = -Size / 5;
            OldRightX = -Size / 5;
            OldRightY = -Size / 5;


            if (Top.Y < Bottom.Y)
            {
                LeftTop = new Point(Top.X - (int)(OldLeftX * Math.Cos(Tetta) - OldLeftY * Math.Sin(Tetta)),
                                    Top.Y - (int)(OldLeftX * Math.Sin(Tetta) + OldLeftY * Math.Cos(Tetta)));
                RightTop = new Point(Top.X - (int)(OldRightX * Math.Cos(Tetta) - OldRightY * Math.Sin(Tetta)),
                                    Top.Y - (int)(OldRightX * Math.Sin(Tetta) + OldRightY * Math.Cos(Tetta)));
            }
            else
            {
                LeftTop = new Point(Top.X + (int)(OldLeftX * Math.Cos(Tetta) - OldLeftY * Math.Sin(Tetta)),
                                    Top.Y + (int)(OldLeftX * Math.Sin(Tetta) + OldLeftY * Math.Cos(Tetta)));
                RightTop = new Point(Top.X + (int)(OldRightX * Math.Cos(Tetta) - OldRightY * Math.Sin(Tetta)),
                                    Top.Y + (int)(OldRightX * Math.Sin(Tetta) + OldRightY * Math.Cos(Tetta)));
            }

            gr.DrawLine(Pen1, Top, Bottom);
            gr.DrawLine(Pen1, Top, LeftTop);
            gr.DrawLine(Pen1, Top, RightTop);
        }
    }
}
