using System;

namespace Mandelbrot
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start");
            //PrimToBinary ptb = new PrimToBinary();
            ComplexDec z = new ComplexDec(0, 0);
            ComplexDec c = new ComplexDec(-0.25m, 0.75m);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            z = z.Iteralizacja(c);
            Console.WriteLine(z);
            Console.WriteLine("End");

        }
    }
}
