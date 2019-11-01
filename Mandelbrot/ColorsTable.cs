using System;
using System.Drawing;

namespace Mandelbrot
{
    public class ColorsTable
    {
        static int BIT_PER_COLOR;

        public ColorsTable(int bitsPerColor)
        {
            BIT_PER_COLOR = bitsPerColor;
        }

        public static Color GetColor()
        {
            if (BIT_PER_COLOR > 0) return Color.White;
            else return Color.Red;
        }
    }
}
