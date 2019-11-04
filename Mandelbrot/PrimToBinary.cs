using System;
using System.Collections.Generic;

namespace Mandelbrot
{
    public class PrimToBinary : List<int>
    {
        public PrimToBinary()
        {
            //this.Add(2);
            int max = 515;
            for (int i = 2; i <= max; i++)
            {
                bool isPrimary = true;
                for (int j = 0; j < this.Count; j++)
                {
                    if (i % this[j] == 0)
                        isPrimary = false;
                }
                if (isPrimary)
                {
                    Console.WriteLine(Convert.ToString(i,2).PadLeft(10,'-') + "  " + i);
                    this.Add(i);
                }
            }
        }
    }
    public class ThreeToBinary : List<int>
    {
        public ThreeToBinary()
        {
            //this.Add(2);
            int max = 515;
            for (int i = 3; i <= max; i *= 3)
            {
                Console.WriteLine( i + "  " + Convert.ToString(i, 2).PadLeft(10, '-'));
            }
        }
    }
}
