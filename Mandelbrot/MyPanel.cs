using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Mandelbrot
{
    public class MyPanel : Panel
    {
        List<Bitmap> bitmaps;
        Color[] colors;
        int bitsPerColor = 2;
        ComplexDec start = new ComplexDec(-2.15m, 1.25m);
        ComplexDec stop = new ComplexDec(.75m, -1.35m);
        ComplexDec temp = new ComplexDec(0m, 1m);

        //Bitmap bitmap;
        public MyPanel(Size size) : base()
        {
            initColors();
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
                    int it = t.level(temp);
                    Color color = colors[limitColors(it)];

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

        private int limitColors(int it)
        {
            while (it >= colors.Length)
            {
                it = it - colors.Length + (int)Math.Pow(2, bitsPerColor);
            }
            return it;
        }

        private void initColors()
        {
            int pow = (int)Math.Pow(2, bitsPerColor);
            colors = new Color[pow * 7];
            int[,] paterns = { { 1, 0, 0 }, { 3, 1, 0 }, { 2, 3, 0 }, { 0, 3, 1 }, { 0, 2, 3 }, { 1, 0, 3 }, { 3, 0, 2 } };
            int[] rgb = { 0, 0, 0 };
            int pointer = 3, direction = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (paterns[i, j] == 0) rgb[j] = 0;
                    else if (paterns[i, j] == 3) rgb[j] = 255;
                    else { pointer = j; direction = paterns[i, j]; }
                }


                for (int j = 0; j < pow; j++)
                {
                    if (direction == 1)
                        rgb[pointer] = j << (8 - bitsPerColor);
                    else
                    {
                        if (j == 0)
                            rgb[pointer] = 255;
                        else
                            rgb[pointer] = (pow - j) << (8 - bitsPerColor);
                    }

                    colors[i * pow + j] = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    Console.WriteLine(colors[i * pow + j]);
                }
            }
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
