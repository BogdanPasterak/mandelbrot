using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mandelbrot
{
    class MainClass
    {
        //static Graphics graphics;
        public static void Main(string[] args)
        {
            //Console.WriteLine("Start");

            Application.Run(new MyForm());
            //PrimToBinary ptb = new PrimToBinary();

            //Process process = Process.GetCurrentProcess();
            //MainClass.graphics = Graphics.


            ComplexDec z = new ComplexDec(0, 0);
            ComplexDec c = new ComplexDec(-0.25m, 0.75m);
            /*
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
            */           
 
        }
    }
}
