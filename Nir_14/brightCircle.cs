using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Nir_14
{
    class brightCircle
    {
        private List<int> X = new List<int>();
        private List<int> Y = new List<int>();
        private List<double> Bright = new List<double>();
        public int xc = 0, xp = 0, yp=0;
        private List<bool> it = new List<bool>();
        private int vectX, vectY;
        private bool vect_exist = false;
        public void setVector(int X, int Y)
        {
            vectX = X; vectY = Y;
        }
        public void addBright(int x, int y, double bright)
        {
            X.Add(x); Y.Add(y); Bright.Add(bright);
        }
        public void setV(bool vect)
        {
            vect_exist = vect;
        }
        public List<int> getPoint(int i)
        {
            List<int> temp = new List<int>();
            temp.Add(X[i]); temp.Add(Y[i]);
            return temp;
        }
        private double d(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        private double dist(int xi, int yi, int vx, int vy)
        {
            return Math.Abs(vy * xi - vx * yi) / Math.Sqrt(vx * vx + vy * vy);
        }
        public int configure()
        {
            if (!vect_exist)
            {
                double summ = 0;
                for (int i = 0; i < Bright.Count(); i++)
                {
                    summ += Bright[i];
                }
                double av = summ / Bright.Count;
                int j1 = 0; int j2 = 0;
                for (int i = 0; i < Bright.Count(); i++)
                {
                    if (Bright[i] > av)
                    {
                        j1++;
                    }
                    else j2++;
                }
                double temp = Bright[0];
                if (j1 < j2)
                {
                    for (int i = 0; i < Bright.Count(); i++)
                    {
                        if (Bright[i] > temp) temp = Bright[i];
                    }
                }
                else
                {
                    for (int i = 0; i < Bright.Count(); i++)
                    {
                        if (Bright[i] < temp && d(X[i], Y[i], xp, yp) > 1) temp = Bright[i];
                    }
                }

                return Bright.IndexOf(temp);
            }
            else
            {
                double summ = 0;
                for (int i = 0; i < Bright.Count(); i++)
                {
                    summ += Bright[i];
                }
                double av = summ / Bright.Count;
                int j1 = 0; int j2 = 0;
                for (int i = 0; i < Bright.Count(); i++)
                {
                    if (Bright[i] > av)
                    {
                        j1++;
                    }
                    else j2++;
                }
                double r = 0;
                int ind = 0;
                if(j1 < j2)
                {
                    for(int i = 0; i < Bright.Count; i++)
                    {
                        if (Bright[i] > av && dist(X[i], Y[i], vectX, vectY) < r && X[i] > xc)
                        {
                            ind = i;
                            r = dist(X[i], Y[i], vectX, vectY);
                        }
                    }
                }
                else
                {
                    r = Double.MaxValue;
                    for (int i = 0; i < Bright.Count; i++)
                    {
                        if (Bright[i] < av && dist(X[i], Y[i], vectX, vectY) < r && X[i] > xc)
                        {
                            ind = i;
                            r = dist(X[i], Y[i], vectX, vectY);
                        }
                    }
                }
                return ind;
            }
        }
    }
}
