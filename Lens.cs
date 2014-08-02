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
    class Lens
    {
        int FocalDistance;
        int Position;
        int Type;
        int Number;
        bool Hidden;

        public Lens(int T, int FD, int P, int N)
        {
            Type = T;
            FocalDistance = FD;
            Position = P;
            Number = N;
            Hidden = false;
        }

        public int GetNumber()
        {
            return Number;
        }

        public int GetFocus()
        {
            return FocalDistance;
        }

        public void HideLens()
        {
            Hidden = true;
        }

        public void ShowLens()
        {
            Hidden = false;
        }

        public bool IsHidden()
        {
            return Hidden;
        }

        public void Draw(Panel MainPanel, PaintEventArgs e)
        {
            int i = Number;
            int Height = MainPanel.Size.Height;

            Graphics gr = e.Graphics;
            Pen Pen1 = new Pen(Color.DimGray, 2);
            Pen Pen2 = new Pen(Color.DimGray, 3);
            SolidBrush Brush1 = new SolidBrush(Color.DimGray);
            String drawString = (i+1).ToString();
            Font drawFont = new Font("Microsoft Sans Serif", 10.0f);

            if (Hidden)
            {
                Pen1 = new Pen(Color.CadetBlue, 1);
                Pen2 = new Pen(Color.CadetBlue, 2);
            }
            
                Point Top1, Top2, Top3, Bottom1, Bottom2, Bottom3;
                Point RightFocus1 = new Point(Position + FocalDistance, Height / 2 - 5);
                Point RightFocus2 = new Point(Position + FocalDistance, Height / 2 + 5);
                Point LeftFocus1 = new Point(Position - FocalDistance, Height / 2 - 5);
                Point LeftFocus2 = new Point(Position - FocalDistance, Height / 2 + 5);

                // собирающая
                Top1 = new Point(Position - 5, 5);
                Top2 = new Point(Position, 0);
                Top3 = new Point(Position + 5, 5);
                Bottom1 = new Point(Position - 5, Height - 6);
                Bottom2 = new Point(Position, Height - 1);
                Bottom3 = new Point(Position + 5, Height - 6);

                // рассеивающая
                if (Type == 2)
                {
                    Top1 = new Point(Position - 5, 0);
                    Top2 = new Point(Position, 5);
                    Top3 = new Point(Position + 5, 0);
                    Bottom1 = new Point(Position - 5, Height - 1);
                    Bottom2 = new Point(Position, Height - 6);
                    Bottom3 = new Point(Position + 5, Height - 1);
                }

                gr.DrawLine(Pen1, Top1, Top2);
                gr.DrawLine(Pen1, Top2, Top3);
                gr.DrawLine(Pen1, Top2, Bottom2);
                gr.DrawLine(Pen1, Bottom1, Bottom2);
                gr.DrawLine(Pen1, Bottom2, Bottom3);
                gr.DrawLine(Pen2, RightFocus1, RightFocus2);
                gr.DrawLine(Pen2, LeftFocus1, LeftFocus2);            

            gr.DrawString(drawString, drawFont, Brush1, Position + 5, Height - 20);
        }

        public void ChangeProp(int Index, int FD, int Pos, int Param)
        {
            if (Param == 1)
                Type = Index;
            if (Param == 2)
                FocalDistance = FD;
            if (Param == 3)
                Position = Pos;
        }

        public int GetPosition()
        {
            return Position;
        }

        public int GetLensType()
        {
            return Type;
        }
    }
}
