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
    class LensConstruction
    {
        int Length;
        List<Lens> Construction;

        public LensConstruction() 
        {
            Length = 0;
            Construction = new List<Lens>(5);
        }

        public int GetLength()
        {
            return Length;
        }

        public void ChangeProp(int LensNumber, int Index, int FD, int Pos, int Param)
        {
            bool Done = false;
            int i = 0;

            while (i < Length && !Done)
            {
                if (LensNumber == Construction[i].GetNumber())
                {
                    Construction[i].ChangeProp(Index, FD, Pos, Param);
                    Done = true;
                }
                i++;
            }
        }

        public void SortLens()
        {
            Construction.Sort((x, y) => x.GetPosition().CompareTo(y.GetPosition()));
        }

        public void AddNewLens(int Type, int FD, int P)
        {
            Lens NewLens = new Lens(Type, FD, P, Length);
            Construction.Add(NewLens);

            SortLens();

            Length++;
        }

        public void HideLens(int i)
        {
            for (int k = 0; k < Length; k++)
                if (Construction[k].GetNumber() == i - 1)
                    Construction[k].HideLens();
        }

        public void ShowLens(int i)
        {
            for (int k = 0; k < Length; k++)
                if (Construction[k].GetNumber() == i - 1)
                    Construction[k].ShowLens();
        }

        // отрисовка конструкции
        public void DrawLenses(Panel MainPanel, PaintEventArgs e)
        {
            for (int i = 0; i < Length; i++)
                Construction[i].Draw(MainPanel, e);
        }

        // отрисовка пути луча в сцене
        public void DrawBeamPath(Panel MainPanel, PaintEventArgs e, int ArrowTopX, int ArrowTopY, int ArrowBottomX,
                        int ArrowBottomY, bool OrientUp)
        {
            Image Image1;
            int Height = MainPanel.Height;
            int Axis = Height / 2;

            Graphics gr = e.Graphics;
            Pen Pen1 = new Pen(Color.White, 1);
            Pen Pen2 = new Pen(Color.White, 1);
            Pen DashPen1 = new Pen(Color.White, 1);
            DashPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Pen DashPen2 = new Pen(Color.White, 1);
            DashPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            int Focus = 1;

            for (int i = 0; i < Length; i++)
            {
                if (!Construction[i].IsHidden())  
                {
                if (Construction[i].GetLensType() == 1)
                    Focus = Construction[i].GetFocus();
                if (Construction[i].GetLensType() == 2)
                    Focus = -Construction[i].GetFocus();
                int Position = Construction[i].GetPosition();

                if (Focus != 0)
                {
                    double k1 = 0, b1 = 0, k2, b2;
                  
                    if (Position < ArrowTopX)
                    {
                        k1 = (ArrowTopY - Axis) * (1.0 / Focus);
                    }
                    else
                    {
                        k1 = (ArrowTopY - Axis) * (-1.0 / Focus);
                    }

                    b1 = ArrowTopY - k1 * Position;

                    k2 = (ArrowTopY - Axis) * (1.0 / (ArrowTopX - Position));
                    b2 = ArrowTopY - k2 * ArrowTopX;

                    Point PointArTop = new Point(ArrowTopX, ArrowTopY);
                    Point PointArTopProj = new Point(Position, ArrowTopY);
                    Point PointLensMid = new Point(Position, Axis);
                    Point PointArBot = new Point(ArrowBottomX, ArrowBottomY);
                    Point PointArBotProj = new Point(Position, ArrowBottomY);     
                    Point LeftFocusTop = new Point(Position + Focus, Axis);
                    if (ArrowTopX > Position)
                        LeftFocusTop = new Point(Position - Focus, Axis);
                    Point LeftFocusBot = new Point(Position + Focus, Axis);
                    if (ArrowBottomX > Position)
                        LeftFocusBot = new Point(Position - Focus, Axis);

                   Point Intersection1 = new Point();
                   if (Construction[i].GetLensType() != 1 ||
                       ArrowTopX != Position - Focus && ArrowTopX != Position + Focus)
                    {
                        if (k1 == 0 && k2 == 0)
                            if (ArrowTopX < Position)
                                Intersection1 = new Point(Position
                                                 + (int)(1.0 / (1.0 / Focus - 1.0 / Math.Abs(Position - ArrowTopX))), Axis);
                            else
                                if (ArrowTopX > Position)
                                    Intersection1 = new Point(Position
                                                 - (int)(1.0 / (1.0 / Focus - 1.0 / Math.Abs(Position - ArrowTopX))), Axis);
                                else
                                    Intersection1 = new Point(Position, Axis);
                        else
                            Intersection1 = new Point((int)((b1 - b2) / (k2 - k1)),
                                                       (int)((k2 * b1 - k1 * b2) / (k2 - k1)));

                        if (Position < ArrowBottomX)
                        {
                            k1 = (ArrowBottomY - Axis) * (1.0 / Focus);
                        }
                        else
                        {
                            k1 = (ArrowBottomY - Axis) * (-1.0 / Focus);
                        }

                        b1 = ArrowBottomY - k1 * Position;

                        k2 = (ArrowBottomY - Axis) * (1.0 / (ArrowBottomX - Position));
                        b2 = ArrowBottomY - k2 * ArrowBottomX;
                    }

                    Point Intersection2 = new Point();
                    if (Construction[i].GetLensType() != 1 ||
                        ArrowBottomX != Position - Focus && ArrowBottomX != Position + Focus)
                    {
                        if (k1 == 0 && k2 == 0)
                            if (ArrowTopX < Position)
                                Intersection2 = new Point(Position
                                            + (int)(1.0 / (1.0 / Focus - 1.0 / Math.Abs(Position - ArrowBottomX))), Axis);
                            else
                                if (ArrowTopX > Position)
                                    Intersection2 = new Point(Position
                                                - (int)(1.0 / (1.0 / Focus - 1.0 / Math.Abs(Position - ArrowBottomX))), Axis);
                                else
                                    Intersection2 = new Point(Position, Axis);
                        else
                            Intersection2 = new Point((int)((b1 - b2) / (k2 - k1)),
                                                    (int)((k2 * b1 - k1 * b2) / (k2 - k1)));
                    }

                    if (OrientUp)
                    {
                        gr.DrawLine(Pen1, PointArTop, PointArTopProj);
                        gr.DrawLine(Pen1, PointArTop, PointLensMid);

                        gr.DrawLine(Pen2, PointArBot, PointArBotProj);
                        gr.DrawLine(Pen2, PointArBot, PointLensMid);
                    }
                    else
                    {
                        gr.DrawLine(Pen2, PointArTop, PointArTopProj);
                        gr.DrawLine(Pen2, PointArTop, PointLensMid);

                        gr.DrawLine(Pen1, PointArBot, PointArBotProj);
                        gr.DrawLine(Pen1, PointArBot, PointLensMid);
                    }

                    if (Construction[i].GetLensType() != 1 ||
                        ArrowTopX != Position - Focus && ArrowTopX != Position + Focus &&
                        ArrowBottomX != Position - Focus && ArrowBottomX != Position + Focus)
                    {
                        if (Construction[i].GetLensType() == 1)
                        {
                            if (OrientUp)
                            {
                                gr.DrawLine(Pen1, PointArTopProj, Intersection1);
                                gr.DrawLine(Pen1, PointLensMid, Intersection1);

                                gr.DrawLine(Pen2, PointArBotProj, Intersection2);
                                gr.DrawLine(Pen2, PointLensMid, Intersection2);
                            }
                            else
                            {
                                gr.DrawLine(Pen2, PointArTopProj, Intersection1);
                                gr.DrawLine(Pen2, PointLensMid, Intersection1);

                                gr.DrawLine(Pen1, PointArBotProj, Intersection2);
                                gr.DrawLine(Pen1, PointLensMid, Intersection2);
                            }
                        }
                        if (Construction[i].GetLensType() == 2)
                        {
                            if (OrientUp)
                            {
                                gr.DrawLine(DashPen1, PointArTopProj, LeftFocusTop);
                                gr.DrawLine(DashPen2, PointArBotProj, LeftFocusBot);
                            }
                            else
                            {
                                gr.DrawLine(DashPen2, PointArTopProj, LeftFocusTop);
                                gr.DrawLine(DashPen1, PointArBotProj, LeftFocusBot);
                            }
                        }

                        // изменить позицию стрелки на позицию следующего изображения
                        ArrowTopX = Intersection1.X;
                        ArrowTopY = Intersection1.Y;

                        ArrowBottomX = Intersection2.X;
                        ArrowBottomY = Intersection2.Y;

                        bool IsReal = Construction[i].GetLensType() == 1;

                        Image1 = new Image(IsReal, ArrowTopX, ArrowTopY, ArrowBottomX, ArrowBottomY, OrientUp);

                        // 0 - промежуточное изображение, 1 - финальное
                        if (i == Length - 1)
                            Image1.DrawImage(MainPanel, e, 1);
                        else
                        {
                            if (Construction[i + 1].IsHidden())
                                Image1.DrawImage(MainPanel, e, 1);
                            else
                                Image1.DrawImage(MainPanel, e, 0);
                        }
                    }
                    else
                        break;
                }
            }
            }           
        }
    }
}
