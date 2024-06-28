using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private const int radious = 3;
        private const int numb_point_for_curve = 50;



        private void Draw(Point[] p)
        {
            Graphics g;
            g = panel1.CreateGraphics();
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Red, 2);
            Pen curve_p = new Pen(Color.Blue, 2);

            foreach (Point point in p)
            {
                g.DrawEllipse(pen, point.X - radious, point.Y - radious, 2 * radious, 2 * radious);
            }

            Point[] curvePoints = new Point[numb_point_for_curve];
            for (int i = 0; i < numb_point_for_curve; i++)
            {
                float t = (float)i / (numb_point_for_curve - 1);
                curvePoints[i] = cubic_Bezier(t, p[0], p[1], p[2], p[3]);
            }
            g.DrawLines(curve_p, curvePoints);
        }

        private Point cubic_Bezier(float t, Point p0, Point p1, Point p2, Point p3)
        {
            float u = 1 - t;
            float t1 = t * t;
            float u1 = u * u;
            float u2 = u1 * u;
            float t2 = t1 * t;

            int x = (int)(u2 * p0.X + 3 * u1 * t * p1.X + 3 * u * t1 * p2.X + t2 * p3.X);
            int y = (int)(u2 * p0.Y + 3 * u1 * t * p1.Y + 3 * u * t1 * p2.Y + t2 * p3.Y);

            return new Point(x, y);
        }

        private void SierpinskiCarpet(int order, int x, int y, int width, int height)
        {
            Graphics g;
            g = panel1.CreateGraphics();

            if (order == 0)
            {
                g.FillRectangle(Brushes.Black, x, y, width, height);
            }
            else
            {
                int newWidth = width / 3;
                int newHeight = height / 3;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i != 1 || j != 1)
                        {
                            SierpinskiCarpet(order - 1, x + i * newWidth, y + j * newHeight, newWidth, newHeight);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g;
            g = panel1.CreateGraphics();
            g.Clear(Color.White);

            Point[] points = new Point[4];

            points[0] = new Point(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox5.Text));
            points[1] = new Point(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox6.Text));
            points[2] = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox7.Text));
            points[3] = new Point(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox8.Text));

            Pen pen = new Pen(Color.Black);


            Draw(points);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g;
            g = panel1.CreateGraphics();
            g.Clear(Color.White);

            int x, y, h, w, order;
            x = Convert.ToInt32(textBox9.Text);
            y = Convert.ToInt32(textBox10.Text);
            h = Convert.ToInt32(textBox11.Text);
            w = Convert.ToInt32(textBox12.Text);
            order = Convert.ToInt32(textBox13.Text);

            SierpinskiCarpet(order, x, y, w, h);
        }
    }
}
