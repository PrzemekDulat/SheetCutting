using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;


namespace RectangleSpreadController
{
    public class RectangleSorter
    {
        public List<OrderRelatedElement> GenerateRandomRectanglesAndPositionThem()
        {
            List<OrderRelatedElement> rectangles = CreateRandomListOfRectangles(8);
            List<OrderRelatedElement> rectanglesDesc = SortRectanglesDecreasing(rectangles);
            List<OrderRelatedElement> rectanglesPositioned = SetupRectanglesLocation(rectanglesDesc, 1500, 500);
            return rectanglesPositioned;
        }
        

        public void CutExample(OrderRelatedElement[] orderRelatedElements)
        {

            Sheet sheet = Sheet.CreateNewSheet(orderRelatedElements, default, new Size(1000, 1500));

            CutSheetIntoPieces(sheet);

        }

        private static ISheet[] CutSheetIntoPieces(Sheet sheet)
        {
            List<ISheet> result = new List<ISheet>();
            var cutLine = sheet.GetFirstValidCutLine(sheet.GetCutLines(sheet.OrderRelatedElements));
            var innerSheets = Sheet.CutSheet(sheet, cutLine);

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

        private List<OrderRelatedElement> SetupRectanglesLocation(List<OrderRelatedElement> rectangles, int stWidth, int sHeight)
        {
            int sheetWidth = stWidth;
            int sheetHeight = sHeight;
            List<OrderRelatedElement> firstsInColumns = new List<OrderRelatedElement>();
            int heightOffset = 0;
            int widthOffset = 0;
            firstsInColumns.Add(rectangles.First());
            foreach (OrderRelatedElement rectangleModel in rectangles)
            {
                if (rectangleModel.Size.Height <= CalculateRemainingHeight()) //fits by height
                {
                    rectangleModel.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangleModel.Size.Height;
                }
                else //offset to the right
                {
                    widthOffset = 0;
                    foreach (var bRectangle in firstsInColumns)
                    {
                        widthOffset += bRectangle.Size.Width;
                    }

                    firstsInColumns.Add(rectangleModel);
                    heightOffset = 0;
                    rectangleModel.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangleModel.Size.Height;
                }
            }

            return rectangles;

            int CalculateRemainingHeight()
            {
                return sheetHeight - heightOffset;
            }
        }

        private List<OrderRelatedElement> SortRectanglesDecreasing(List<OrderRelatedElement> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle.Size.Height > rectangle.Size.Width)
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
                OrderRelatedElement rectangle = new OrderRelatedElement();
                rectangle.Location = new Point(0, 0);
                rectangle.Size = new Size(GetRandomNumber(50, 200), GetRandomNumber(50, 200));
                rectangles.Add(rectangle);
            }
            return rectangles;
        }
        
        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }

    }
}
