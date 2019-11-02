using System;
using System.Windows.Forms;
//using System.Windows;
using System.Drawing;
using System.Threading;


namespace Mandelbrot
{
    public class MyForm : Form
    {
        MyPanelPixels panel;
        Size mySize = new Size(1220, 990);

        public MyForm()
        {
            this.Text = "Mandelbrot";
            this.Size = mySize;
            panel = new MyPanelPixels(this.ClientSize);


            this.Controls.Add(panel);

        }

    }

}
