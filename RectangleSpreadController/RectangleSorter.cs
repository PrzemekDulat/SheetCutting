using RectangleSpreadController.Algorithms;
using RectangleSpreadController.Interfaces;
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
        public List<List<OrderRelatedElement>> SampleDataSet(bool rotatable)
        {
            OrderRelatedElement[] orderRelatedElements = new OrderRelatedElement[6];

            //orderRelatedElements[0] = new OrderRelatedElement(new Point(0, 0), new Size(200, 300), "No.1234567-123");
            //orderRelatedElements[1] = new OrderRelatedElement(new Point(0, 300), new Size(200, 300), "No.2234567-123");
            //orderRelatedElements[2] = new OrderRelatedElement(new Point(600, 500), new Size(200, 300), "No.3234567-123");
            //orderRelatedElements[3] = new OrderRelatedElement(new Point(800, 1200), new Size(200, 300), "No.4234567-123");
            //orderRelatedElements[4] = new OrderRelatedElement(new Point(123, 1240), new Size(300, 200), "No.5234567-123");
            //orderRelatedElements[5] = new OrderRelatedElement(new Point(0, 600), new Size(200, 300), "No.6234567-123");

            return SortedRectanglesByAlgorithm(new FirstFitDecreasingAlgorithm());
        }
        public List<List<OrderRelatedElement>> SortedRectanglesByAlgorithm(IAlgorithm algorithm)
        {
            AlgorithmConstrains algorithmConstrains = AlgorithmConstrains.Create(true, 1500);

            Size sheetSize = new Size(1000,1500);
            
            Sheet sheet = Sheet.CreateNewSheet(SortRectanglesDecreasing(CreateRandomListOfRectangles(25), algorithmConstrains.IsAbleToRotate).ToArray(), default, sheetSize);
            
            return algorithm.ExecuteAlgorithm(sheet, algorithmConstrains);
        }
        public List<List<ISheet>> CutExample(List<List<OrderRelatedElement>> listOfrectangles)
        {
           

            List<List<ISheet>> cuttedSheets = new List<List<ISheet>>();
            foreach (var rectangles in listOfrectangles)
            {
                Sheet sheet = Sheet.CreateNewSheet(rectangles.ToArray(), default, new Size(1000, 1500));

                //PrintoutInstructionsHowToCut();
                cuttedSheets.Add(CutSheetIntoPieces(sheet).ToList());
            }
            return cuttedSheets;

            throw new ArgumentException($"There are no rectangles");
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
            bool rotatedLeft = false;
            if (!File.Exists(path))
            {
                //Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var item in cutListResult)
                    {
                        if (item.Line.LineType == LineType.Horizontal)
                        {
                            if (rotatedLeft)
                            {
                                sw.WriteLine(counter + ".Rotate right");
                                rotatedLeft = false;
                            }
                            sw.WriteLine(counter + ".Cut sheet horizontal on Value: " + item.Line.Value);
                        }
                        else if (item.Line.LineType == LineType.Vertical)
                        {
                            if (!rotatedLeft)
                            {
                                sw.WriteLine(counter + ".Rotate left");
                                rotatedLeft = true;

                            }
                            sw.WriteLine(1 + counter + ".Cut sheet horizontal on Value: " + item.Line.Value);
                            counter++;
                        }
                        counter++;

                        foreach (var outputSheet in item.OutputSheets)
                        {
                            if (outputSheet.GetType() == typeof(OrderRelatedElement))
                            {
                                sw.WriteLine("   -Element: " + item.Sheet.OrderRelatedElements[0].OrderLine);
                                rotatedLeft = false;
                                if (item.Sheet.OrderRelatedElements.Count() == 1)
                                {
                                    counter = 1;

                                }
                            }
                            else if (outputSheet.GetType() == typeof(Waste))
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

        private List<OrderRelatedElement> SortRectanglesDecreasing(List<OrderRelatedElement> rectangles, bool isAbleToRotate)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle.Size.Height > rectangle.Size.Width && isAbleToRotate)
                {
                    Size tmpSize = new Size();
                    tmpSize = rectangle.Size;
                    rectangle.Size = new Size(tmpSize.Height, tmpSize.Width);
                }
            }
            List<OrderRelatedElement> sortedRectangles = rectangles.OrderByDescending(o => o.Size.Width).ToList();

            return sortedRectangles;
        }

        public List<OrderRelatedElement> CreateRandomListOfRectangles(int numberOfRectangles)
        {
            if (numberOfRectangles < 0) { throw new ArgumentException(nameof(numberOfRectangles)); }

            List<OrderRelatedElement> rectangles = new List<OrderRelatedElement>();

            for (int i = 0; i < numberOfRectangles; i++)
            {
                OrderRelatedElement rectangle = new OrderRelatedElement(new Point(0, 0), new Size(200,300), "No.1234567");
                rectangles.Add(rectangle);
            }
            return rectangles;
        }

    }
}
