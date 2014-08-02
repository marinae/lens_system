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
    class OpticalScene
    {
        LensConstruction LensC;
        Subject Arrow;
        bool BeamShowed;

        public OpticalScene()
        {
            LensC = new LensConstruction();
            Arrow = new Subject();
            BeamShowed = false;
        }

        public bool ShowBeam()
        {
            if (LensC.GetLength() > 0)
            {
                BeamShowed = true;
                return true;
            }
            else
            {
                MessageBox.Show("Добавьте линзы", "Нет линз", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public void HideBeam()
        {
            BeamShowed = false;
        }

        public void ChangeSubTopX(int NewTopX)
        {
            Arrow.ChangePosX1(NewTopX);
        }

        public void ChangeSubTopY(int NewTopY)
        {
            Arrow.ChangePosY1(NewTopY);
        }

        public void ChangeSubBotX(int NewBotX)
        {
            Arrow.ChangePosX2(NewBotX);
        }

        public void ChangeSubBotY(int NewBotY)
        {
            Arrow.ChangePosY2(NewBotY);
        }

        public void ChangeSubjectOrient(bool NewOrient)
        {
            Arrow.ChangeOrient(NewOrient);
        }

        public void ChangeInfinity(bool Inf)
        {
            Arrow.ChangeInf(Inf);
        }

        public void AddLens(int Type, int FD, int P)
        {
            LensC.AddNewLens(Type, FD, P);
        }

        public int GetLenghtConstr()
        {
            return LensC.GetLength();
        }

        public void ChangeLensProp(int LensNumber, int Index, int FD, int Pos, int Param)
        {
            LensC.ChangeProp(LensNumber - 1, Index, FD, Pos, Param);
            LensC.SortLens();
        }

        public void HideLens(int i)
        {
            LensC.HideLens(i);
        }

        public void ShowLens(int i)
        {
            LensC.ShowLens(i);
        }

        public void Paint(Panel MainPanel, PaintEventArgs e)
        {
            int Height = MainPanel.Size.Height;
            int Width = MainPanel.Size.Width;
            int Axis = Height / 2;

            Graphics gr = e.Graphics;
            Pen Pen1 = new Pen(Color.DimGray, 1);
            
            // рисуем ось
            Point Begin = new Point(0, Height / 2);
            Point End = new Point(MainPanel.Right, Height / 2);
            gr.DrawLine(Pen1, Begin, End);

            // рисуем засечки на оси
            for (int i = 0; i < Width; i += 10)
            {
                Point Point1 = new Point(i, Height / 2 - 2);
                Point Point2 = new Point(i, Height / 2 + 2);
                gr.DrawLine(Pen1, Point1, Point2);
            }
            
            LensC.DrawLenses(MainPanel, e);

            if (BeamShowed)
            {
                LensC.DrawBeamPath(MainPanel, e, Arrow.ArrowTopX(), Axis - Arrow.ArrowTopY(),
                    Arrow.ArrowBottomX(), Axis - Arrow.ArrowBottomY(), Arrow.GetOrient());
            }
            
            Arrow.Paint(MainPanel, e);
        }
    }
}
