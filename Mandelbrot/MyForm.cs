using System;
using System.Windows.Forms;
//using System.Windows;
using System.Drawing;
using System.Threading;


namespace Mandelbrot
{
    public class MyForm : Form
    {
        MyPanel panel;
        Size mySize = new Size(920, 680);

        public MyForm()
        {
            this.Text = "Mandelbrot";
            this.Size = mySize;
            panel = new MyPanel(this.ClientSize);


            this.Controls.Add(panel);

        }

    }

}
