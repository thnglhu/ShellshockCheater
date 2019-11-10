using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShellshockCheater
{
    public partial class Form1 : Form
    {
        Game.Match match = new Game.Match();
        int angle = 90;
        int power = 50;
        int wind = 0;
        public Form1()
        {
            InitializeComponent();
            panel2.MouseWheel += MyMouseWheel;
        }

        private void MainMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void CanvasPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(Color.Green);
            Pen pen = new Pen(Color.Black, 3);
            Vector position = match.Position;
            g.FillEllipse(brush, position.x - 7, position.y - 7, 14, 14);
            float radian = Game.WeaponPredictor.Convert(angle);
            List<List<Vector>> paths = match.GetPaths(power / 100f, wind / 10f, radian, 60);
            foreach(List<Vector> path in paths)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    g.DrawLine(pen, path[i].x, path[i].y, path[i + 1].x, path[i + 1].y);
                }
            }
            
            Brush font_brush = new SolidBrush(Color.White);
            Font font = new Font("Arial", 16);
            int display = (angle + 360) % 360;
            if (display < 180)
            {
                display = (display > 90) ? (180 - display) : display;
            }
            else
            {
                display -= 180;
                display = (display > 90) ? (display - 180) : -display;
            }
            g.DrawString($"{power}, {display}", font, font_brush, position.x, position.y + 50);
            if (wind < 0.0f)
            {
                g.DrawString($"<< {-wind / 10}.{-wind % 10}", font, font_brush, 500, 50);
            }
            else if (wind > 0.0f)
            {
                g.DrawString($"{wind / 10}.{wind % 10} >>", font, font_brush, 500, 50);
            }

        }

        Vector init = new Vector();
        bool mouse_down = false;
        private void CanvasMouseDown(object sender, MouseEventArgs e)
        {
            init = new Vector(e.X, e.Y);
            mouse_down = true;
        }

        private void CanvasMouseMotion(object sender, MouseEventArgs e)
        {
            if (mouse_down)
            {
                Vector new_mouse = new Vector(e.X, e.Y);
                Vector vector = new_mouse - init;
                match.Position += vector;
                init = new_mouse;
                panel2.Invalidate();
            }
        }

        private void CanvasMouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false;
        }
        private void MyMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (!ctrl && !shift)
                {
                    angle += 1;
                    panel2.Invalidate();
                }
                else if (ctrl && !shift && power <= 99)
                {
                    power += 1;
                    panel2.Invalidate();
                }
                else if (!ctrl && shift)
                {
                    wind += 1;
                    panel2.Invalidate();
                }
            }
            else if (e.Delta < 0)
            {
                if (!ctrl && !shift)
                {
                    angle -= 1;
                    panel2.Invalidate();
                }
                else if (ctrl && !shift && power >= 1)
                {
                    power -= 1;
                    panel2.Invalidate();
                }
                else if (!ctrl && shift)
                {
                    wind -= 1;
                    panel2.Invalidate(); 
                }
            }
        }
        bool ctrl = false;
        bool shift = false;
        private void MainKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
            {
                ctrl = true;
                shift = false;
            }
            else if (e.KeyValue == 16)
            {
                ctrl = false;
                shift = true;
            }
        }

        private void MainKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
            {
                ctrl = false;
            }
            else if (e.KeyValue == 16)
            {
                shift = false;
            }
        }
    }
}
