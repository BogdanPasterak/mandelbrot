using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Mandelbrot
{
    public class MyPanel : Panel
    {
        List<Bitmap> bitmaps;
        ComplexDec start = new ComplexDec(-2.25m, 1.25m);
        ComplexDec stop = new ComplexDec(.75m, -1.25m);
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

            Console.WriteLine(size.ToString());

            bitmaps = new List<Bitmap>();
            for (int i = 0; i < size.Height / 3; i++)
            {
                bitmaps.Add(new Bitmap(size.Width, 3));
            }


            ComplexDec t;

            for (int y = 0; y < bitmaps.Count; y++)
            {
                for (int i = 0; i < size.Width / 3; i++)
                {
                    temp.real = start.real + (3m / (size.Width / 3)) * i;
                    temp.imag = start.imag - (2.5m / (size.Height / 3)) * y;
                    t = new ComplexDec(temp);
                    Color color = Color.Black;
                    int it = t.level(temp);
                    if (it == 1) color = Color.Aqua;
                    if (it == 2) color = Color.Blue;
                    if (it == 3) color = Color.Cyan;
                    if (it == 4) color = Color.Fuchsia;
                    if (it == 5) color = Color.Salmon;
                    if (it == 6) color = Color.Tan;
                    if (it == 7) color = Color.Yellow;
                    if (it > 7) color = Color.Orchid;
                    if (it > 63) color = Color.Red;

                    {
                        //Console.WriteLine(t.ToString());
                        bitmaps[y].SetPixel(i * 3, 0, color);
                        bitmaps[y].SetPixel(i * 3, 1, color);
                        bitmaps[y].SetPixel(i * 3, 2, color);
                        bitmaps[y].SetPixel(i * 3 + 1, 0, color);
                        bitmaps[y].SetPixel(i * 3 + 1, 1, color);
                        bitmaps[y].SetPixel(i * 3 + 1, 2, color);
                        bitmaps[y].SetPixel(i * 3 + 2, 0, color);
                        bitmaps[y].SetPixel(i * 3 + 2, 1, color);
                        bitmaps[y].SetPixel(i * 3 + 2, 2, color);
                    }
                    //bitmaps[1].SetPixel(i, 1, color);
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
