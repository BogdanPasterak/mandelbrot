using System;
using System.Drawing;

namespace Mandelbrot
{
    public class ColorsTable
    {
        private static int BIT_PER_COLOR;
        private static Color[] COLORS;
        private static int RESOLUTION;

        private static void Initialize(int? bitsPerColor = null)
        {
            // default resolution 4 bits
            BIT_PER_COLOR = bitsPerColor ?? 3;
            RESOLUTION = (int)Math.Pow(2, BIT_PER_COLOR);
            COLORS = new Color[RESOLUTION * 7];

            int[,] paterns = { { 1, 0, 0 }, { 3, 1, 0 }, { 2, 3, 0 }, { 0, 3, 1 }, { 0, 2, 3 }, { 1, 0, 3 }, { 3, 0, 2 } };
            int[] rgb = { 0, 0, 0 };
            int restOfBits = 8 - BIT_PER_COLOR, pointer = 3, direction = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (paterns[i, j] == 0) rgb[j] = 0;
                    else if (paterns[i, j] == 3) rgb[j] = 255;
                    else { pointer = j; direction = paterns[i, j]; }
                }


                for (int j = 0; j < RESOLUTION; j++)
                {
                    if (direction == 1)
                        rgb[pointer] = j << restOfBits;
                    else
                    {
                        if (j == 0)
                            rgb[pointer] = 255;
                        else
                            rgb[pointer] = (RESOLUTION - j) << restOfBits;
                    }

                    COLORS[i * RESOLUTION + j] = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    //Console.WriteLine(COLORS[i * RESOLUTION + j]);
                }

            }
        }

        public static Color GetColor(int? number)
        {
            if (number == null)
                return Color.Black;

            if (BIT_PER_COLOR == 0)
                Initialize();

            while (number >= COLORS.Length)
            {
                number = number - COLORS.Length + RESOLUTION;
            }

            return COLORS[number ?? 0];
        }
    }
}
