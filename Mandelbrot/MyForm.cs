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
        Size mySize = new Size(1520, 1040);

        public MyForm()
        {
            this.Text = "Mandelbrot";
            //this.Size = mySize;
            WindowState = FormWindowState.Maximized;
            panel = new MyPanelPixels(this.ClientSize);
            panel = new MyPanelPixels(Screen.FromControl(this).WorkingArea.Size);

            this.Controls.Add(panel);
        }

    }

}
