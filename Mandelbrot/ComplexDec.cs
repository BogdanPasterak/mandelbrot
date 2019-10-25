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
    }
}
