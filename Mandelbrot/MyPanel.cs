using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mandelbrot
{
    public class MyPanel : Panel
    {
        public MyPanel() : base()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(Pens.Black, 0, 0, 100, 50);

            // not work, problem "gdi32.dll"
            //Color color = Color.White;
            //IntPtr hdc = e.Graphics.GetHdc();
            //uint colorRef = (uint)((color.B << 16) | (color.G << 8) | (color.R));
            //GDI.SetPixel(hdc, 1, 10, colorRef);
            //e.Graphics.ReleaseHdc(hdc);

        }

    }

    /*
    public class GDI
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool SetPixel(IntPtr hdc, int x, int y, uint crColor);
    }
    */

}
