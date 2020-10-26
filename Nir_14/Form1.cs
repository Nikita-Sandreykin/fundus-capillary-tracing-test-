using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Nir_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        brightCircle brightCircle = new brightCircle();
        double[,] bright;
        Bitmap image;
        int radius = 6;
        int xp=0, yp=0;
        int x0 = 6;
        int y0 = 24;
        bool check = false;
        int vx = 0; int vy = 0;
        int iter = 5;
        private void button1_Click(object sender, EventArgs e)
        {
            iter = Convert.ToInt32(textBox3.Text);
            image = new Bitmap("in.bmp");
            bright = new double[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    bright[x, y] = image.GetPixel(x, y).GetBrightness();
                }
            }
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            for (int j = 0; j < iter; j++)
            {
                brightCircle = new brightCircle();
                brightCircle.xc = x0;
                brightCircle.xp = xp; brightCircle.yp = yp;
                brightCircle.setV(check);
                if(check)
                {
                    brightCircle.setVector(vx, vy);
                }
                int yd = radius;
                int xd = 0;
                int d = 2 - 2 * radius;
                while (xd <= yd)
                {
                    image.SetPixel(x0 + xd, y0 - yd, Color.Black); brightCircle.addBright(x0 + xd, y0 - yd, bright[x0 + xd, y0 - yd]);
                    image.SetPixel(x0 + xd, y0 + yd, Color.Black); brightCircle.addBright(x0 + xd, y0 + yd, bright[x0 + xd, y0 + yd]);
                    image.SetPixel(x0 - xd, y0 + yd, Color.Black); brightCircle.addBright(x0 - xd, y0 - yd, bright[x0 - xd, y0 - yd]);
                    image.SetPixel(x0 - xd, y0 - yd, Color.Black); brightCircle.addBright(x0 - xd, y0 + yd, bright[x0 - xd, y0 + yd]);
                    image.SetPixel(x0 + yd, y0 + xd, Color.Black); brightCircle.addBright(x0 + yd, y0 + xd, bright[x0 + yd, y0 + xd]);
                    image.SetPixel(x0 - yd, y0 + xd, Color.Black); brightCircle.addBright(x0 - yd, y0 + xd, bright[x0 - yd, y0 + xd]);
                    image.SetPixel(x0 + yd, y0 - xd, Color.Black); brightCircle.addBright(x0 + yd, y0 - xd, bright[x0 + yd, y0 - xd]);
                    image.SetPixel(x0 - yd, y0 - xd, Color.Black); brightCircle.addBright(x0 - yd, y0 - xd, bright[x0 - yd, y0 - xd]);
                    if (d < 0)
                        d += (2 * xd) + 3;
                    else
                    {
                        d += (2 * (xd - yd)) + 5;
                        yd -= 1;
                    }
                    xd++;

                }
                Graphics t = Graphics.FromImage(image);
                int Xn = brightCircle.getPoint(brightCircle.configure())[0];
                int Yn = brightCircle.getPoint(brightCircle.configure())[1];
                t.DrawLine(new Pen(Color.Red, 3), new Point(x0, y0), new Point(Xn, Yn));
                vx = x0 - Xn;
                vy = y0 - Yn;
                x0 = Xn;
                y0 = Yn;
                xp = Xn; yp = Yn;
                image.SetPixel(Xn, Yn, Color.Red);
                check = true;
                this.pictureBox1.Image = image;
            }
        }
    }
}
