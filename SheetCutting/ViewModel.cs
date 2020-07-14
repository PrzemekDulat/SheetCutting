using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RectangleSpreadController;
using RectangleSpreadController.Models;

namespace SheetCutting
{
    public class ViewModel
    {

        public void StartCuttingSheet()
        {
            RectangleSorter rectangleSorter = new RectangleSorter();

            var result = rectangleSorter.CutExample(rectangleSorter.SampleDataSet());
            var howToCut = rectangleSorter.GetCutLineAndSheets();

            DrawSheetOnScreen(howToCut);
        }

        public List<Image> DrawSheetOnScreen(List<CutLineAndSheet> cutLinesAndSheets)
        {
            List<Image> images = new List<Image>();

            foreach (var item in cutLinesAndSheets)
            {
                //TODO
            }

            return images;
        }
        //public Image StartAssemblingRectangles()
        //{
        //    var rectangleSorter = new RectangleSorter();
        //    var areaCreator = new AreaCreator();
        //    Image image = new Bitmap(1500, 500);
        //    using (Graphics formGraphics = Graphics.FromImage(image))
        //    {
        //        var sortedRectangles = rectangleSorter.GenerateRandomRectanglesAndPositionThem();
        //        var sortedLines = areaCreator.AssembledLines(sortedRectangles);
        //        var rectangleHorizontalLines = areaCreator.HorizontalRectangleLines(sortedRectangles);
        //        var rectangleVerticalLines = areaCreator.VerticalRectangleLines(sortedRectangles);

        //        var verticalCuttingLines = areaCreator.VerticalCuttingLines(sortedRectangles);
        //        var horizontalCuttingLines = areaCreator.HorizontalCuttingLines(sortedRectangles);
        //        foreach (var rectangle in sortedRectangles)
        //        {
        //            DrawRectangle(rectangle.Location, rectangle.Size, formGraphics);
        //        }

        //        //foreach (var line in sortedLines)
        //        //{
        //        //    DrawLines(line, formGraphics, Color.Red, 1);
        //        //}

        //        //foreach (var line in rectangleHorizontalLines)
        //        //{
        //        //    DrawLines(line, formGraphics, Color.Yellow, 3);
        //        //}

        //        //foreach (var line in rectangleVerticalLines)
        //        //{
        //        //    DrawLines(line, formGraphics, Color.Yellow, 3);
        //        //}
        //        //foreach (var line in verticalCuttingLines)
        //        //{
        //        //    DrawLines(line, formGraphics, Color.Black, 10);
        //        //}
        //        foreach (var line in horizontalCuttingLines)
        //        {
        //            DrawLines(line, formGraphics, Color.Red, 2);
        //        }


        //        var areas = areaCreator.CreateAreasBasedOnVerticalLines(verticalCuttingLines);
        //        var areasWithRectangles = areaCreator.DetermineRectangleInArea(areas, sortedRectangles);
        //        areaCreator.CutAreasHorizontaly(areasWithRectangles);
        //    }

        //    return image;
        //}

        //internal List<OrderRelatedElement> GenerateRandomRectanglesAndPositionThem()
        //{
        //    var rectangleSorter = new RectangleSorter();
        //    return  rectangleSorter.GenerateRandomRectanglesAndPositionThem();
        //}

        //int i = 0;
        //public static void DrawRectangle(Point location, Size size, Graphics formGraphics)
        //{
        //    SolidBrush myBrush = new SolidBrush(Color.FromArgb(GetRandomNumber(1, 255), GetRandomNumber(1, 255), GetRandomNumber(1, 255)));
        //    Rectangle rect = new Rectangle(location, size);
        //    formGraphics.DrawRectangle(new Pen(Color.Black, 0), rect);
        //    formGraphics.FillRectangle(myBrush, rect);
        //    myBrush.Dispose();
        //}

        //public static void DrawLines(LineModelOld line, Graphics formGraphics, Color color, int width)
        //{
        //    //SolidBrush myBrush = new SolidBrush(Color.FromArgb(GetRandomNumber(1, 255), GetRandomNumber(1, 255), GetRandomNumber(1, 255)));
        //    //Rectangle rect = new Rectangle(location, size);
        //    formGraphics.DrawLine(new Pen(color, width), line.StartLocation, line.EndLocation);
        //    //formGraphics.FillRectangle(myBrush, rect);
        //    //myBrush.Dispose();
        //}

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

        //private static readonly Random getrandom = new Random();
        //public static int GetRandomNumber(int min, int max)
        //{
        //    lock (getrandom)
        //    {
        //        return getrandom.Next(min, max);
        //    }
        //}

        //public static string RandomString(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    return new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[getrandom.Next(s.Length)]).ToArray());
        //}

    }
}
