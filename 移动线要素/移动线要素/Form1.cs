using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 移动线要素
{
    public partial class Form1 : Form
    {
        int mi = -1;
        List<Point> p = new List<Point>();
        int n = -1;
        Point p1,p2;
        public Form1()
        {
            InitializeComponent();
        }


        public void move(Point p1, Point p2, Point f1,Point f2)
        {
            
            f1.X += p2.X - p1.X;
            f1.Y += p2.Y - p1.Y;
            f2.X += p2.X - p1.X;
            f2.Y += p2.Y - p1.Y;
            p[n] = f1;
            p[n + 1] = f2;
        }

        public void select(int x, int y)
        {
            
            double a, b, c, d,f;
            f = 10;
            Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Brushes.Black);
            pen.Width=2.5f;
            n = -1;
            for (int i = 0; i < p.Count; i+=2)
            {
                

                a = p[i].Y - p[i+1].Y;
                b = p[i+1].X - p[i].X;
                c = p[i].X * p[i+1].Y - p[i+1].X * p[i].Y;
                d = (Math.Abs(a * x + b *y + c)) / (Math.Sqrt(a * a + b * b));
                if (d < f)
                {
                    f = d;
                    
                    n = i;
                }
               
            }
            if(f<10)
             g.DrawLine(pen,p[n],p[n+1]);
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = panel1.CreateGraphics();
            if (mi == 1)
            {
                for (int i = 0; i <p.Count-1; i += 2)
                    g.DrawLine(Pens.Black, p[i], p[i + 1]);
                g.DrawLine(Pens.Black, p[p.Count-1], p2);
            }
            if (mi == 2)
            {
                for (int i = 0; i < p.Count; i += 2)
                    g.DrawLine(Pens.Black, p[i], p[i + 1]);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mi == 1)
            {
                p1.X = e.X;
                p1.Y = e.Y;
                p.Add(p1);
            }
            if (mi == 2)
            {
                select(e.X,e.Y);
                p1.X = e.X;
                p1.Y = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                if (mi == 1)
                {
                    p2.X = e.X;
                    p2.Y = e.Y;


                    panel1.Invalidate();
                }

            }
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (mi == 1)
                p.Add(p2);
            if (mi == 2)
            {
                Point[] f = new Point[2];
                p2.X = e.X;
                p2.Y = e.Y;
                f[0] = p[n];
                f[1] = p[n + 1];

                move(p1, p2, f[0], f[1]);
              
                panel1.Invalidate();
            }
        }

        private void 画线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mi = 1;
        }

        private void 移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mi = 2;
        }
    }
}
