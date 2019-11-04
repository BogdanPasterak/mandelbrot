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
            MyForm myForm = new MyForm();

            Application.Run(myForm);

            //PrimToBinary ptb = new PrimToBinary();
            //ThreeToBinary ptb = new ThreeToBinary();

            //Process process = Process.GetCurrentProcess();
            //MainClass.graphics = Graphics.


            Console.WriteLine("End");

        }
    }
}
