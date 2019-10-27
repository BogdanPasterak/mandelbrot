using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mandelbrot
{
    public class MyPanel : Panel
    {

        public MyPanel(Size size) : base()
        {
            size.Width -= 10;
            size.Height -= 10;
            Size = size;
            Location = new Point(5, 5);

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(Pens.Black, 0, 0, 100, 50);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Color.Yellow;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (BackColor == Color.Red)
            {
                BackColor = Color.Blue;
            }
            else
            {
                BackColor = Color.Red;
            }
        }

    }


}
