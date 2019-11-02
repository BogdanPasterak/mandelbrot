using System;
using System.Windows.Forms;
using System.Drawing;
namespace Mandelbrot
{
    public class MyPanelPixels : Panel
    {
        private ComplexDec center;
        private decimal scale;
        private Bitmap bitmap;

        public MyPanelPixels(Size size) : base()
        {
            center = new ComplexDec(-0.75m, 0);
            scale = 1m / (decimal)Math.Pow(2, 8);
            Console.WriteLine(scale);
            size.Width -= 10;
            size.Height -= 10;
            Size = size;
            Location = new Point(5, 5);
            bitmap = new Bitmap(size.Width, size.Height);

            ComplexDec start = new ComplexDec(center.real - (bitmap.Width / 2) * scale, center.imag + (bitmap.Height / 2) * scale);
            ComplexDec complex = new ComplexDec(center);
            ComplexDec z;
            for (int x = 0; x < bitmap.Width; x++)
            {
                complex.real = start.real + x * scale;
                for (int y = 0; y < bitmap.Height; y++)
                {
                    complex.imag = start.imag - y * scale;
                    z = new ComplexDec(complex);
                    bitmap.SetPixel(x, y, ColorsTable.GetColor(z.level(complex)));


                    if ( complex.real == 0 || complex.imag == 0 ) bitmap.SetPixel(x, y, Color.Black);
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
