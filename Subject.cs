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
    class Subject
    {
        protected int PositionTopX;
        protected int PositionTopY;
        protected int PositionBottomX;
        protected int PositionBottomY;
        protected bool OrientationUp;
        protected bool Infinity;

        public Subject()
        {
            PositionTopX = 80;
            PositionTopY = 50;
            PositionBottomX = 60;
            PositionBottomY = 0;
            OrientationUp = true;
            Infinity = false;
        }

        protected Subject(int PX1, int PY1, int PX2, int PY2, bool OU, bool I)
        {
            PositionTopX = PX1;
            PositionTopY = PY1;
            PositionBottomX = PX2;
            PositionBottomY = PY2;
            OrientationUp = OU;
            Infinity = I;
        }

        public void ChangePosX1(int P)
        {
            PositionTopX = P;
        }

        public void ChangePosY1(int P)
        {
            PositionTopY = P;
        }

        public void ChangePosX2(int P)
        {
            PositionBottomX = P;
        }

        public void ChangePosY2(int P)
        {
            PositionBottomY = P;
        }

        public int ArrowTopX()
        {
            return PositionTopX;
        }

        public int ArrowTopY()
        {
            return PositionTopY;
        }

        public int ArrowBottomX()
        {
            return PositionBottomX;
        }

        public int ArrowBottomY()
        {
            return PositionBottomY;
        }

        public bool GetOrient()
        {
            return OrientationUp;
        }

        public void ChangeOrient(bool OU)
        {
            if (OU)
                OrientationUp = false;
            else
                OrientationUp = true;
        }

        public void ChangeInf(bool Inf)
        {
            if (Inf)
                Infinity = true;
            else
                Infinity = false;
        }

        public bool GetInfinity()
        {
            return Infinity;
        }

        public void Paint(Panel MainPanel, PaintEventArgs e)
        {
            int Height = MainPanel.Size.Height;
            int Axis = Height / 2;

            Graphics gr = e.Graphics;
            Pen Pen1 = new Pen(Color.Black, 3);

            Point Top, Bottom, LeftTop, RightTop;
            int OldLeftX, OldLeftY, OldRightX, OldRightY;
            int Size = (int)(Math.Sqrt(Math.Pow(PositionTopX-PositionBottomX,2) + Math.Pow(PositionTopY-PositionBottomY,2)));
            double Tetta = Math.Atan((double)(PositionTopX-PositionBottomX) / (PositionTopY-PositionBottomY));

            if (OrientationUp)
            {
                Top = new Point(PositionTopX, Axis - PositionTopY);
                Bottom = new Point(PositionBottomX, Axis - PositionBottomY);
            }
            else
            {
                Top = new Point(PositionBottomX, Axis - PositionBottomY);
                Bottom = new Point(PositionTopX, Axis - PositionTopY);
            }

            OldLeftX = Size / 5;
            OldLeftY = -Size / 5;
            OldRightX = -Size / 5;
            OldRightY = -Size / 5;

            if (Top.Y < Bottom.Y || Top.Y == Bottom.Y && Top.X > Bottom.X)
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
