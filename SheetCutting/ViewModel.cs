using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RectangleSpreadController;
using RectangleSpreadController.Algorithms;
using RectangleSpreadController.Models;

namespace SheetCutting
{
    public class ViewModel
    {
        RectangleSorter rectangleSorter = new RectangleSorter();

        public void StartCuttingSheet(bool rotatable)
        {
            var result = rectangleSorter.CutExample(rectangleSorter.SampleDataSet(rotatable));
            var howToCut = rectangleSorter.GetCutLineAndSheets();
            
            DrawSheetOnScreen(howToCut);
        }



        List<Image> images = new List<Image>();

        //public void DoStuff()
        //{
        //    BestFitAlgorithm bestFitAlgorithm = new BestFitAlgorithm();
        //    bestFitAlgorithm.test();
        //}
        public List<Image> DrawSheetOnScreen(List<CutLineAndSheet> cutLinesAndSheets)
        {
            foreach (var item in cutLinesAndSheets)
            {
                Image precutImage = new Bitmap(1000, 1600);

                using (Graphics formGraphics = Graphics.FromImage(precutImage))
                {
                    DrawSheet(item.Sheet, formGraphics);
                    
                    foreach (var rectangle in item.Sheet.OrderRelatedElements)
                    {
                        DrawRectangle(rectangle ,formGraphics);
                    }

                    DrawLines(item, formGraphics, Color.Red, 4);
                }
                images.Add(precutImage);

            }

            return images;
        }

        public static int counter = 0;
        public Image GoThroughAllSteps()
        {
            if (counter >= images.Count)
            {
                counter--;
                MessageBox.Show("Koniec.");
            }
            else if (counter < 0)
            {
                counter++;
                MessageBox.Show("Przekroczyles limit.");
            }
            else
            {
                return images[counter];
            }

            return null;
        }
       
        public static void DrawRectangle(OrderRelatedElement rectangle, Graphics formGraphics)
        {
            Font myFont = new Font("Arial", 22);

            SolidBrush myBrush = new SolidBrush(Color.FromArgb(53,175,255));
            Rectangle rect = new Rectangle(rectangle.Location, rectangle.Size);

            formGraphics.DrawRectangle(new Pen(Color.Black, 2), rect);
            formGraphics.FillRectangle(myBrush, rect);
            formGraphics.DrawString(rectangle.OrderLine, myFont, Brushes.Black, rectangle.Location);

            myBrush.Dispose();
        }

        public static void DrawLines(CutLineAndSheet cutLineAndSheet, Graphics formGraphics, Color color, int width)
        {
            ICutLine line = cutLineAndSheet.Line;
            var sheetLocation = cutLineAndSheet.Sheet.Location;

            if (line.LineType == LineType.Horizontal)
            {
                Point firstPoint = new Point(0 + sheetLocation.X, line.Value + sheetLocation.Y);
                Point secondPoint = new Point(2000 + sheetLocation.X, line.Value + sheetLocation.Y);

                formGraphics.DrawLine(new Pen(color, width), firstPoint, secondPoint);
            }
            else if (line.LineType == LineType.Vertical)
            {
                Point firstPoint = new Point(line.Value + sheetLocation.X, 0 + sheetLocation.Y);
                Point secondPoint = new Point(line.Value + sheetLocation.X, 2000 + sheetLocation.Y);

                formGraphics.DrawLine(new Pen(color, width), firstPoint, secondPoint);
            }
        }

        public static void DrawSheet(Sheet sheet, Graphics formGraphics)
        {
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(200,200,200));
            Rectangle rect = new Rectangle(sheet.Location, sheet.Size);
            formGraphics.DrawRectangle(new Pen(Color.Black, 0), rect);
            formGraphics.FillRectangle(myBrush, rect);
            myBrush.Dispose();
        }

        public void GenerateTextInstruction()
        {
            rectangleSorter.PrintoutInstructionsHowToCut();
        }

        public int GetScore()
        {
            return rectangleSorter.CalculateScore();
        }

        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }

        //public void GenerateJson(List<OrderRelatedElement> rectangles)
        //{
        //    string path = @"C:\Users\PERMAR\source\repos\SheetCutting\" + RandomString(10) + ".txt";
        //    if (!File.Exists(path))
        //    {
        //        //Create a file to write to.
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            foreach (var rectangle in rectangles)
        //            {
        //                sw.WriteLine("Width: " + rectangle.Size.Width + "   Height: " + rectangle.Size.Height + "     POS: " + rectangle.Location.X + ", " + rectangle.Location.Y);
        //            }
        //        }
        //    }
        //}

    }
}
