using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RectangleSpreadController;

namespace SheetCutting
{
    public class Adapter
    {
        RectangleSorter r;
        public Image StartAssemblingRectangles()
        {
            r = new RectangleSorter();
            Image image = new Bitmap(1500, 500);
            using (Graphics formGraphics = Graphics.FromImage(image))
            {
                List<RectangleModel> sortedRectangles = r.AssembledRectangles();
                foreach (var rectangle in sortedRectangles)
                {
                    DrawRectangle(rectangle.Location, rectangle.Size, formGraphics);
                }
            }

            return image;
        }
        int i = 0;
        public static void DrawRectangle(Point location, Size size, Graphics formGraphics)
        {
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(GetRandomNumber(1, 255), GetRandomNumber(1, 255), GetRandomNumber(1, 255)));
            Rectangle rect = new Rectangle(location, size);
            formGraphics.DrawRectangle(new Pen(Color.Black, 0), rect);
            formGraphics.FillRectangle(myBrush, rect);
            myBrush.Dispose();

          
        }

        public void GenerateJson()
        {
            string path = @"C:\Users\PERMAR\source\repos\SheetCutting\"+RandomString(10)+".txt";
            if (!File.Exists(path))
            {
            //Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var rectangle in r.AssembledRectangles())
                    {
                        sw.WriteLine("Width: "+rectangle.Size.Width + "   Height: " + rectangle.Size.Height + "     POS: " + rectangle.Location.X + ", " + rectangle.Location.Y);
                    }
                }
            }
        }

        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[getrandom.Next(s.Length)]).ToArray());
        }

    }
}
