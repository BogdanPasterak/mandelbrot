using System;
using System.Windows.Forms;
//using System.Windows;
using System.Drawing;

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
            mySize = this.ClientSize;
            mySize.Width -= 8;
            mySize.Height -= 8;
            panel = new MyPanel();
            panel.BackColor = Color.Red;
            panel.Size = mySize;
            //panel.Anchor = AnchorStyles.None;
            panel.Location = new Point(4, 4);

            //panel.SetColorPixel(Color.White, 1, 10);
            this.Controls.Add(panel);
        }
    }

}
