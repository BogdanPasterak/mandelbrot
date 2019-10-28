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

        public ComplexDec Iteralizacja(ComplexDec c) {
            return new ComplexDec(real * real - imag * imag + c.real, 2 * real * imag + c.imag);
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
