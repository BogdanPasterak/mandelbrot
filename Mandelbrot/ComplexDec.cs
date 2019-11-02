using System;
namespace Mandelbrot
{
    public class ComplexDec
    {
        public decimal real;
        public decimal imag;

        public ComplexDec(decimal real, decimal imag)
        {
            this.real = real;
            this.imag = imag;
        }

        public ComplexDec(ComplexDec model)
        {
            real = model.real;
            imag = model.imag;
        }

        public ComplexDec iteralizacja(ComplexDec c) {
            return new ComplexDec(real * real - imag * imag + c.real, 2 * real * imag + c.imag);
        }

        public int level(ComplexDec c)
        {
            int level = 0;
            decimal rSquare, iSquare;

            while (level < 57)
            {
                rSquare = real * real;
                iSquare = imag * imag;
                if (rSquare + iSquare >= 4)
                    return level;
                imag = imag * real * 2 + c.imag;
                real = rSquare - iSquare + c.real;
                level++;
            }


            return level;
        }

        public override string ToString()
        {
            return "[ r: " + real + ", i: " + imag + " ]";
        }

        public bool isOver()
        {
            return real * real + imag * imag >= 4;
        }

    }
}
