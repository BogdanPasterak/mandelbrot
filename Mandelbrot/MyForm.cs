using System;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;

namespace Mandelbrot
{
    public class MyForm : Form
    {
        Panel panel;
        public MyForm()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(920, 680);
            panel = new Panel();
            panel.BackColor = Color.Red;
            panel.Size = new Size(900, 600);
            //panel.Anchor = AnchorStyles.None;
            panel.Location = new Point(this.ClientSize.Width / 2 - panel.Width / 2, this.ClientSize.Height / 2 - panel.Height / 2);
            this.Controls.Add(panel);
        }
    }
}
