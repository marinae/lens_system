using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

/*namespace LensSystem
{
    // рассеивающая линза (двояковогнутая)
    class ConcaveLens:Lens
    {
        public ConcaveLens(float FD, float P)
            : base(FD, P)
        {
        }

        public override void Draw(Panel MainPanel, PaintEventArgs e)
        {
            int Height = MainPanel.Size.Height;

            Graphics gr = e.Graphics;
            Pen Pen1 = new Pen(Color.Black, 2);

            Point Top1, Top2, Top3, Bottom1, Bottom2, Bottom3;

            Top1 = new Point(P - 3, 4);
            Top2 = new Point(P, 1);
            Top3 = new Point(P + 3, 4);
            Bottom1 = new Point(P - 3, Height - 4);
            Bottom2 = new Point(P, Height - 1);
            Bottom3 = new Point(P + 3, Height - 4);

            gr.DrawLine(Pen1, Top1, Top2);
            gr.DrawLine(Pen1, Top2, Top3);
            gr.DrawLine(Pen1, Top2, Bottom);
        }

        // виртуальный метод для расчета выходного изображения
        public override Image NextImage(Image PreviousImage)
        {
            return null;
        }
    }
}*/
