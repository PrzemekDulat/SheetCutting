using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheetCutting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image = new Bitmap(200, 150);
            using (Graphics formGraphics = Graphics.FromImage(image))
            {
                Color backgroundColor = Color.Red;
                Point location = new Point(20, 20);
                Size size = new Size(50, 50);
                Color borderColor = Color.Black;

                DrawRectangle(location, size, formGraphics, borderColor, backgroundColor);
            }
            pictureBox1.Image = image;
        }

        private static void DrawRectangle(Point location, Size size, Graphics formGraphics, Color borderColor, Color backgroundColor)
        {
            SolidBrush myBrush = new SolidBrush(backgroundColor);
            Rectangle rect = new Rectangle(location, size);
            formGraphics.DrawRectangle(new Pen(borderColor, 1), rect);
            formGraphics.FillRectangle(myBrush, rect);
            myBrush.Dispose();
        }
    }
}
