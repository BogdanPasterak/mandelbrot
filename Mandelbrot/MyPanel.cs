using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Mandelbrot
{
    public class MyPanel : Panel
    {
        List<Bitmap> bitmaps;
        ComplexDec start = new ComplexDec(-2.25m, 1m);
        ComplexDec stop = new ComplexDec(.75m, -1m);
        ComplexDec temp = new ComplexDec(0m, 1m);

        //Bitmap bitmap;
        public MyPanel(Size size) : base()
        {
            int wDiv = size.Width % 3;
            int hDiv = size.Height % 3;
            size.Width -= 12 + wDiv;
            size.Height -= 12 + hDiv;
            Size = size;
            Location = new Point(6 + wDiv / 2, 6 + hDiv / 2);

            bitmaps = new List<Bitmap>();
            for (int i = 0; i < size.Height / 3; i++)
            {
                bitmaps.Add(new Bitmap(size.Width, 3));
            }


            ComplexDec t = new ComplexDec(0, 0);
            for (int y = 0; y < bitmaps.Count; y++)
            {
                for (int i = 0; i < size.Width / 3; i++)
                {
                    temp.real = start.real + (3m / (size.Width / 3)) * i;
                    temp.imag = start.imag - (2m / (size.Height / 3)) * y;
                    t.real = temp.real;
                    t.imag = temp.imag;
                    t = t.Iteralizacja(temp);
                    t = t.Iteralizacja(temp);
                    t = t.Iteralizacja(temp);
                    t = t.Iteralizacja(temp);
                    if (t.isOver())
                   {
                        //Console.WriteLine(t.ToString());
                        bitmaps[y].SetPixel(i * 3, 0, Color.Black);
                        bitmaps[y].SetPixel(i * 3, 1, Color.Black);
                        bitmaps[y].SetPixel(i * 3, 2, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 1, 0, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 1, 1, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 1, 2, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 2, 0, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 2, 1, Color.Black);
                        bitmaps[y].SetPixel(i * 3 + 2, 2, Color.Black);
                    }
                    //bitmaps[1].SetPixel(i, 1, Color.Black);
                }
            }

            /*
            bitmap = new Bitmap(size.Width, 3);

            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(Color.Pink))
            {
                gfx.FillRectangle(brush, 0, 0, 50, 3);
            }
            Console.WriteLine(size.ToString());
            */
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(Pens.Black, 0, 0, 100, 50);
            for (int i = 0; i < bitmaps.Count; i++)
                e.Graphics.DrawImage(bitmaps[i], 0, i * 3);
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
