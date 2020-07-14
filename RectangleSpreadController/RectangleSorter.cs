using RectangleSpreadController.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace RectangleSpreadController
{
    public class RectangleSorter
    {
        //public List<OrderRelatedElement> GenerateRandomRectanglesAndPositionThem()
        //{
        //    List<OrderRelatedElement> rectangles = CreateRandomListOfRectangles(8);
        //    List<OrderRelatedElement> rectanglesDesc = SortRectanglesDecreasing(rectangles);
        //    List<OrderRelatedElement> rectanglesPositioned = SetupRectanglesLocation(rectanglesDesc, 1500, 500);
        //    return rectanglesPositioned;
        //}


        public OrderRelatedElement[] SampleDataSet()
        {
            OrderRelatedElement[] orderRelatedElements = new OrderRelatedElement[5];

            orderRelatedElements[0] = new OrderRelatedElement(new Point(0, 0), new Size(200, 300), "No.1");
            orderRelatedElements[1] = new OrderRelatedElement(new Point(0, 300), new Size(200, 300), "No.2");
            orderRelatedElements[2] = new OrderRelatedElement(new Point(600, 500), new Size(200, 300), "No.3");
            orderRelatedElements[3] = new OrderRelatedElement(new Point(800, 1200), new Size(200, 300), "No.4"); 
            orderRelatedElements[4] = new OrderRelatedElement(new Point(0, 1200), new Size(200, 300), "No.5");

            return orderRelatedElements;
        }

        public ISheet[] CutExample(OrderRelatedElement[] orderRelatedElements)
        {
            Sheet sheet = Sheet.CreateNewSheet(orderRelatedElements, default, new Size(1000, 1500));

            //PrintoutInstructionsHowToCut();

            return CutSheetIntoPieces(sheet);
        }
        static List<CutLineAndSheet> cutListResult = new List<CutLineAndSheet>();
        public List<CutLineAndSheet> GetCutLineAndSheets()
        {
            return cutListResult;
        }
        private static ISheet[] CutSheetIntoPieces(Sheet sheet)
        {
            List<ISheet> result = new List<ISheet>();

            var cutLine = sheet.GetFirstValidCutLine(sheet.GetCutLines(sheet), sheet);

            var innerSheets = Sheet.CutSheet(sheet, cutLine);

            cutListResult.Add(CutLineAndSheet.Create(cutLine, sheet, innerSheets));

            foreach (var item in innerSheets)
            {
                switch (item)
                {
                    case Waste waste:
                        result.Add(waste);
                        break;
                    case OrderRelatedElement orderElement:
                        result.Add(orderElement);
                        break;
                    case Sheet innerSheet:
                        result.AddRange(CutSheetIntoPieces(innerSheet));
                        break;
                }

            }

            return result.ToArray();
        }

        public void PrintoutInstructionsHowToCut()
        {
            int counter = 1;
            string path = @"C:\Users\DULPRZ\Source\Repos\PrzemekDulat\SheetCutting" + RandomString(10) + ".txt";
            if (!File.Exists(path))
            {
                //Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var item in cutListResult)
                    {
                        if (item.Line.LineType == LineType.Horizontal)
                        {
                            sw.WriteLine(counter + ".Cut sheet horizontal on Value: " + item.Line.Value);
                        }
                        else if(item.Line.LineType == LineType.Vertical)
                        {
                            sw.WriteLine(counter + ".Rotate left");
                            sw.WriteLine(1+counter + ".Cut sheet horizontal on Value: " + item.Line.Value);
                            counter++;
                        }
                        counter++;

                        foreach (var tye in item.OutputSheets)
                        {
                            if (tye.GetType() == typeof(OrderRelatedElement))
                            {
                                sw.WriteLine("   -Element: " + item.Sheet.OrderRelatedElements[0].OrderLine);
                                if (item.Sheet.OrderRelatedElements.Count() == 1)
                                {
                                    counter = 1;

                                }
                            }
                            else if (tye.GetType() == typeof(Waste))
                            {
                                sw.WriteLine("   -Waste");
                            }

                        }
                        
                    }
                }
            }

            Process.Start(path);
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

        //private List<OrderRelatedElement> SetupRectanglesLocation(List<OrderRelatedElement> rectangles, int stWidth, int sHeight)
        //{
        //    int sheetWidth = stWidth;
        //    int sheetHeight = sHeight;
        //    List<OrderRelatedElement> firstsInColumns = new List<OrderRelatedElement>();
        //    int heightOffset = 0;
        //    int widthOffset = 0;
        //    firstsInColumns.Add(rectangles.First());
        //    foreach (OrderRelatedElement rectangleModel in rectangles)
        //    {
        //        if (rectangleModel.Size.Height <= CalculateRemainingHeight()) //fits by height
        //        {
        //            rectangleModel.Location = new Point(widthOffset, heightOffset);
        //            heightOffset += rectangleModel.Size.Height;
        //        }
        //        else //offset to the right
        //        {
        //            widthOffset = 0;
        //            foreach (var bRectangle in firstsInColumns)
        //            {
        //                widthOffset += bRectangle.Size.Width;
        //            }

        //            firstsInColumns.Add(rectangleModel);
        //            heightOffset = 0;
        //            rectangleModel.Location = new Point(widthOffset, heightOffset);
        //            heightOffset += rectangleModel.Size.Height;
        //        }
        //    }

        //    return rectangles;

        //    int CalculateRemainingHeight()
        //    {
        //        return sheetHeight - heightOffset;
        //    }
        //}
        //private List<OrderRelatedElement> SortRectanglesDecreasing(List<OrderRelatedElement> rectangles)
        //{
        //    foreach (var rectangle in rectangles)
        //    {
        //        if (rectangle.Size.Height > rectangle.Size.Width)
        //        {
        //            Size tmpSize = new Size();
        //            tmpSize = rectangle.Size;
        //            rectangle.Size = new Size(tmpSize.Height, tmpSize.Width);
        //        }
        //    }
        //    List<OrderRelatedElement> sortedRectangles = rectangles.OrderByDescending(o => o.Size.Width).ToList();

        //    return sortedRectangles;
        //}
        //public List<OrderRelatedElement> CreateRandomListOfRectangles(int numberOfRectangles)
        //{
        //    if (numberOfRectangles < 0) { throw new ArgumentException(nameof(numberOfRectangles)); }

        //    List<OrderRelatedElement> rectangles = new List<OrderRelatedElement>();

        //    for (int i = 0; i < numberOfRectangles; i++)
        //    {
        //        OrderRelatedElement rectangle = new OrderRelatedElement();
        //        rectangle.Location = new Point(0, 0);
        //        rectangle.Size = new Size(GetRandomNumber(50, 200), GetRandomNumber(50, 200));
        //        rectangles.Add(rectangle);
        //    }
        //    return rectangles;
        //}

        //private static readonly Random getrandom = new Random();
        //public static int GetRandomNumber(int min, int max)
        //{
        //    lock (getrandom)
        //    {
        //        return getrandom.Next(min, max);
        //    }
        //}

    }
}
