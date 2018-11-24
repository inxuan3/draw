using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 凸壳
{
    public partial class Form1 : Form
    {
        int mline = -1;
        Point pt = new Point();
        Point swap = new Point();
        List<Point> s = new List<Point>();//平面点集
        


        public Form1()
        {
            InitializeComponent();
        }




        public void hull()
        {
            Graphics g = panel1.CreateGraphics();
            int n = s.Count-1;
            
            Point[] cs = new Point[50];
            double[] ang = new double[50];
            int m = 0; ;

            int i;
            for ( i = 0; i < n+1; i++) 
            {
                if (s[m].Y > s[i].Y) 
                    m = i;

            }
            g.DrawRectangle(Pens.Black, s[m].X - 2, s[m].Y - 2, 5, 5);

            cs[0] = s[m];

            swap = s[0];
            s[0] = s[m];
            s[m] = swap;





            for (i = 1; i < n+1; i++) 
                ang[i] = (s[i].X - s[0].X) / (float)Math.Sqrt(Math.Pow(s[i].X - s[0].X, 2) + Math.Pow(s[i].Y - s[0].Y, 2));
            for (i = 1; i < n+1; i++)
            {
                m = i;
                for (int j = i + 1; j <n+1; j++)
                    if (ang[m] < ang[j]) 
                        m = j;
                ang[n+1] = ang[i];
                ang[i] = ang[m];
                ang[m] = ang[n+1];
                swap = s[i];
                s[i] = s[m];
                s[m] = swap;
     
            }
 



            s.Add(s[0]);

            for (i = 1; i < n + 1; i++)
            {
                g.DrawLine(Pens.YellowGreen, s[0], s[i]);
            }

                g.DrawLine(Pens.Black, s[0], s[n]);
           

            int k;
            float temp;
            for (i = 1, k = 1; i < n+1; i++)
            {
                temp = (s[i].X - cs[k - 1].X) * (s[i + 1].Y - s[i].Y) - (s[i + 1].X - s[i].X) * (s[i].Y - cs[k - 1].Y);
                if (temp >= 0)
                {
                    cs[k] = s[i];
                   
                    for (int j = k; j > 1; j--)
                    {
                        temp = (cs[j].Y - cs[j - 1].Y) * (cs[j - 1].X - cs[j - 2].X) - (cs[j].X - cs[j - 1].X) * (cs[j - 1].Y - cs[j - 2].Y);
                        if (temp < 0)
                        {
                            cs[j - 1]= cs[j];

                            k--;
                        }
                        else
                            break;
                    }
                    k++;
                }
            }
            cs[k] = cs[0];


            for (i = 0; i < k; i++)
            {
                g.DrawLine(Pens.Yellow, cs[i], cs[i + 1]);
            }

   


        }
    
 
        



        private void 画点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mline = 1;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g=panel1.CreateGraphics();
            if (mline == 1)
            {
                Pen pen=new Pen (Brushes.Red);
                pt.X = e.X;
                pt.Y = e.Y;
                s.Add(pt);
                g.DrawRectangle(pen, e.X - 2, e.Y - 2, 5, 5);
            }
        }

        private void 绘制凸壳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hull();
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
            s.Clear();
        }
    }
}
