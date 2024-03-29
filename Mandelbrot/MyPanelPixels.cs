﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Mandelbrot
{
    public class MyPanelPixels : Panel
    {
        private ComplexDec center;
        private ComplexDec pointZero, curent, z;
        private decimal scale;
        private Bitmap bitmap;
        private TextBox textBox;
        //private int line;
        private Point mouseClick;
        //Thread thread;
        //private Thread t1, t2;
        private BackgroundWorker backgroundWorker;

        public MyPanelPixels(Size size) : base()
        {
            center = new ComplexDec(-0.75m, 0m);
            // dystance beteen pixels 
            scale = 1m / (decimal)Math.Pow(2, 8);
            // white frame 6 pixels (set size and location
            size.Width = size.Width - 12 - (size.Width % 3);
            size.Height = size.Height - 12 - (size.Height % 3);
            Size = size;
            Location = new Point(6, 6);
            // complex value in point (0, 0), center(-0.75m, 0m)
            pointZero = new ComplexDec(-0.75m - scale * (size.Width / 2), scale * (size.Height / 2));
            curent = new ComplexDec(0, 0);
            z = new ComplexDec(0, 0);
            // canvas to paint
            bitmap = new Bitmap(size.Width, size.Height);
            // control to display
            textBox = new TextBox();
            textBox.Width = 30;
            textBox.Text = "Start";
            this.Controls.Add(textBox);

            // counter to lines
            //line = 0;
            //Click += new EventHandler(MyClick);
            // old start
            //thread = new Thread(new ThreadStart(MyThreadFunction));
            //Task.Delay(200).ContinueWith(t => thread.Start());
            // new thread
            //t1 = new Thread(new ThreadStart(Thread1));
            //Task.Delay(200).ContinueWith(t => t1.Start());
            //thread.Start();
            //drawLine();
            // background worker draw picture in seperate thread
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Rectangle rect = new Rectangle(0, 0, Width, 3);
            bool endWork = false;
            int step = 1;
            int line = 0;
            Color color;

            Thread.Sleep(300);
            while (!endWork)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (step == 1)
                    {

                        for (int x = 0; x < Width; x += 3)
                        {
                            color = calculatePoint(x, line + 1);
                            bitmap.SetPixel(x, line, color);
                            bitmap.SetPixel(x + 1, line, color);
                            bitmap.SetPixel(x + 2, line, color);
                            bitmap.SetPixel(x, line + 1, color);
                            bitmap.SetPixel(x + 1, line + 1, color);
                            bitmap.SetPixel(x + 2, line + 1, color);
                            bitmap.SetPixel(x, line + 2, color);
                            bitmap.SetPixel(x + 1, line + 2, color);
                            bitmap.SetPixel(x + 2, line + 2, color);
                        }
                        rect.Y = line;
                        Invalidate(rect);
                        worker.ReportProgress(line);
                        line += 3;
                        if (line >= Height)
                        {
                            step++;
                            line = 0;
                        }
                    }
                    // calculate line
                    else if (step == 2)
                    {

                        for (int x = 0; x < Width; x += 3)
                        {
                            color = calculatePoint(x, line);
                            bitmap.SetPixel(x, line, color);
                            bitmap.SetPixel(x + 1, line, color);
                            bitmap.SetPixel(x, line + 1, color);
                            color = calculatePoint(x + 2, line + 2);
                            bitmap.SetPixel(x + 2, line + 2, color);
                            bitmap.SetPixel(x + 1, line + 2, color);
                            bitmap.SetPixel(x + 2, line + 1, color);
                        }
                        rect.Y = line;
                        Invalidate(rect);
                        worker.ReportProgress(line);
                        line += 3;
                        if (line >= Height)
                        {
                            step++;
                            line = 0;
                        }
                    }
                    else if (step == 3)
                    {
                        endWork = true;
                    }
                    // calculate line
                }
            }

        }

        private Color calculatePoint(int x, int y)
        {
            curent.imag = pointZero.imag - y * scale;
            curent.real = pointZero.real + x * scale;
            z.imag = curent.imag;
            z.real = curent.real;
            return ColorsTable.GetColor(z.level(curent));
        }

        private void calculateLine(int line)
        {
            // ComplexDec start = new ComplexDec(center.real - (bitmap.Width / 2) * scale, center.imag + (bitmap.Height / 2) * scale);
            //ComplexDec complex = new ComplexDec(center);
            //ComplexDec z;
            int y = line; // line++;

            if (y < bitmap.Height)
            {
                curent.imag = pointZero.imag - y * scale;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    curent.real = pointZero.real + x * scale;
                    z.imag = curent.imag;
                    z.real = curent.real;
                    bitmap.SetPixel(x, y, ColorsTable.GetColor(z.level(curent)));
                    //if ( complex.real == 0 || complex.imag == 0 ) bitmap.SetPixel(x, y, Color.Black);
                }
            }
        }

        void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textBox.Text = e.ProgressPercentage.ToString();
        }

        void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                textBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                textBox.Text = "Error";
                Console.WriteLine(e.Error.Message);
            }
            else
            {
                textBox.Text = "Done!";
            }
        }

        /*
        private void Thread1()
        {
            Console.WriteLine("Thread 1, line " + line);
            textBox.Text = line.ToString();
            line++; line++; line++;
            t2 = new Thread(new ThreadStart(Thread2));
            t2.Start();
            t1.Abort();
        }

        private void Thread2()
        {
            if (line == 10000)
            {
                Console.WriteLine("Thread 2, line " + line);
                textBox.Text = line.ToString();
                line++;
                //t1 = new Thread(new ThreadStart(Thread2));
                t1.Start();
            }
            t2.Abort();
        }

        private void MyThreadFunction()
        {
            try
            {
                if (File.Exists("mandelbrot_.bmp"))
                {
                    Bitmap source = new Bitmap("mandelbrot.bmp");
                    using (Graphics gr = Graphics.FromImage(bitmap))
                    {
                        Point point = new Point((source.Width - bitmap.Width) / 2, (source.Height - bitmap.Height) / 2);
                        Rectangle from = new Rectangle(point, bitmap.Size);
                        Rectangle to = new Rectangle(new Point(0, 0), bitmap.Size);
                        gr.DrawImage(source, to, from, GraphicsUnit.Pixel);
                    }
                    //bitmap = new Bitmap(new Bitmap("mandelbrot.bmp"), Size.Width, Size.Height);
                    Invalidate();
                    Console.WriteLine("Bitmap loaded");
                }
                else
                {
                    while (line < bitmap.Height)
                    {
                        drawLine();
                        Invalidate( new Rectangle(0, line -1, Width, 1));
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
        */
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseClick = new Point(e.X, e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //thread.Abort();
            if (Math.Abs(e.X - mouseClick.X) + Math.Abs(e.Y - mouseClick.Y) < 5)
            {
                mouseClick = e.Location;
                //thread = new Thread(new ThreadStart(MyThreadIncrease));
                //Task.Delay(200).ContinueWith(t => thread.Start());
                //thread.Start();
                backgroundWorker.CancelAsync();
            }

        }
        /*
        private void MyThreadIncrease()
        {
            // increase 3x (9)
            ComplexDec complex = new ComplexDec(center.real - scale * (Width / 2 - mouseClick.X),
                                                center.imag + scale * (Height / 2 - mouseClick.Y));
            //Point startOrigin = new Point(mouseClick.X - Width / 6, mouseClick.Y - Height / 6);
            Bitmap copy = new Bitmap(Width / 3, Height / 3);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                Point point = new Point(mouseClick.X - Width / 6, mouseClick.Y - Height / 6);
                Rectangle from = new Rectangle(point, copy.Size);
                Rectangle to = new Rectangle(new Point(0, 0), copy.Size);
                gr.DrawImage(bitmap, to, from, GraphicsUnit.Pixel);
            }
            Color color;
            for (int y = 0; y < copy.Height; y++)
            {
                for (int x = 0; x < copy.Width; x++)
                {
                    color = copy.GetPixel(x, y);
                    bitmap.SetPixel(x * 3, y * 3, color);
                    bitmap.SetPixel(x * 3 + 1, y * 3, color);
                    bitmap.SetPixel(x * 3 + 2, y * 3, color);
                    bitmap.SetPixel(x * 3, y * 3 + 1, color);
                    bitmap.SetPixel(x * 3 + 1, y * 3 + 1, color);
                    bitmap.SetPixel(x * 3 + 2, y * 3 + 1, color);
                    bitmap.SetPixel(x * 3, y * 3 + 2, color);
                    bitmap.SetPixel(x * 3 + 1, y * 3 + 2, color);
                    bitmap.SetPixel(x * 3 + 2, y * 3 + 2, color);
                }
            }
            Invalidate();
            // 2 step details (3,3,3)
            center = complex;
            scale /= 3;
            ComplexDec start = new ComplexDec(center.real - (bitmap.Width / 2) * scale, center.imag + (bitmap.Height / 2) * scale);
            ComplexDec z;
            for (int y = 0; y < Height; y += 3)
            {
                textBox.Text = y.ToString();
                complex.imag = start.imag - y * scale;
                for (int x = 0; x < Width; x += 3)
                {
                    complex.real = start.real + x * scale;
                    z = new ComplexDec(complex);
                    color = ColorsTable.GetColor(z.level(complex));
                    bitmap.SetPixel(x, y, color);
                    bitmap.SetPixel(x + 1, y, color);
                    bitmap.SetPixel(x, y + 1, color);
                }
                complex.imag = start.imag - (y + 2) * scale;
                for (int x = 0; x < Width; x += 3)
                {
                    complex.real = start.real + (x + 2) * scale;
                    z = new ComplexDec(complex);
                    color = ColorsTable.GetColor(z.level(complex));
                    bitmap.SetPixel(x + 2, y + 2, color);
                    bitmap.SetPixel(x + 1, y + 2, color);
                    bitmap.SetPixel(x + 2, y + 1, color);
                }
                Invalidate();
            }

            Console.WriteLine("Thread increase!!! " + complex);
            Console.WriteLine("click  " + mouseClick);
            Console.WriteLine((mouseClick.X - Width / 6).ToString() + "  " + (mouseClick.Y - Height / 6).ToString());

        }
        */
        public void drawLine()
        {
            ComplexDec start = new ComplexDec(center.real - (bitmap.Width / 2) * scale, center.imag + (bitmap.Height / 2) * scale);
            ComplexDec complex = new ComplexDec(center);
            ComplexDec z;
            int y = 0; // line++;

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
            //textBox.Text = y.ToString();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
    
}
