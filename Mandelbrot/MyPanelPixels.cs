﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public class MyPanelPixels : Panel
    {
        private ComplexDec center;
        private decimal scale;
        private Bitmap bitmap;
        private int line;
        Thread thread;

        public MyPanelPixels(Size size) : base()
        {
            center = new ComplexDec(-0.75m, 0m);
            scale = 1m / (decimal)Math.Pow(2, 8);
            Console.WriteLine(scale);
            size.Width -= 10;
            size.Height -= 10;
            Size = size;
            Location = new Point(5, 5);
            bitmap = new Bitmap(size.Width, size.Height);
            line = 0;
            Click += new EventHandler(MyClick);
            thread = new Thread(new ThreadStart(MyThreadFunction));
            Task.Delay(200).ContinueWith(t => thread.Start());
            //thread.Start();
            //drawLine();
        }

        private void MyThreadFunction()
        {
            try
            {
                if (File.Exists("mandelbrot.bmp"))
                {
                    bitmap = new Bitmap("mandelbrot.bmp");
                    Invalidate();
                    Console.WriteLine("Bitmap loaded");
                }
                else
                {
                    while (line < bitmap.Height)
                    {
                        drawLine();
                        Invalidate();
                    }

                    Console.WriteLine("Thread finish");
                    //bitmap.Save("mandelbrot.bmp", ImageFormat.Bmp);
                    //Console.WriteLine("Bitmap saved");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Thread exeption\n" + ex.StackTrace);
            }
        }

        void MyClick(object sender, EventArgs e)
        {
            thread.Abort();
            Console.WriteLine("BBB");
        }

        public void drawLine()
        {
            ComplexDec start = new ComplexDec(center.real - (bitmap.Width / 2) * scale, center.imag + (bitmap.Height / 2) * scale);
            ComplexDec complex = new ComplexDec(center);
            ComplexDec z;
            int y = line++;

            if ( y < bitmap.Height)
            {
                complex.imag = start.imag - y * scale;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    complex.real = start.real + x * scale;
                    z = new ComplexDec(complex);
                    bitmap.SetPixel(x, y, ColorsTable.GetColor(z.level(complex)));
                    //if ( complex.real == 0 || complex.imag == 0 ) bitmap.SetPixel(x, y, Color.Black);
                }
            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
    
}
