using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 曲线
{
    public partial class Form1 : Form
    {
        int mline = -1;
        List<Point> s = new List<Point>();
        Point pt = new Point();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void 画离散店ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mline = 1;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            if (mline == 1)
            {
                pt.X = e.X;
                pt.Y = e.Y;
                s.Add(pt);
                g.DrawRectangle(Pens.Red, e.X - 2, e.Y - 2, 5, 5);
            }
        }

        private void 生成曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mline == 1 && s.Count > 2)
                paint();

        }


        public void paint()
        {
            int i,j,n;
            n=s.Count-1;
            Graphics g = panel1.CreateGraphics();
            double t = new  double();
            Point []pp=new Point[20];

            for (i = 0; i < n; i++)
                g.DrawLine(Pens.Blue, s[i], s[i + 1]);
            for (i = 0; i < n -1; i++)
            {
                for (j = 0, t = 0; t < 1; t += 0.1)
                {

                    pp[j]=hor(i, t);
                    j++;
                }
                for (j = 0; j < 10; j++)
                {
                    g.DrawLine(Pens.Black, pp[j], pp[j + 1]);
                }
            }

        }
        public Point hor(int index,double t)
        {
            Point axu = new Point();
            axu.X = (int)((0.5 * (t - 1) * (t - 1) * s[index].X) + (0.5 * (((-2) * t * t) + (2 * t) + 1) * s[index + 1].X) + (0.5 * t * t * s[index+2].X));
            axu.Y= (int)((0.5 * (t - 1) * (t - 1) * s[index].Y) + (0.5 * (((-2) * t * t) + (2 * t) + 1) * s[index + 1].Y) + (0.5 * t * t * s[index+2].Y));
            return axu;
        }


    }
}
