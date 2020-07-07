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
        public List<RectangleModel> GenerateRandomRectanglesAndPositionThem()
        {
            List<RectangleModel> rectangles = CreateRandomListOfRectangles(8);
            List<RectangleModel> rectanglesDesc = SortRectanglesDecreasing(rectangles);
            List<RectangleModel> rectanglesPositioned = SetupRectanglesLocation(rectanglesDesc, 1500, 500);
            return rectanglesPositioned;
        }
        
        private List<RectangleModel> SetupRectanglesLocation(List<RectangleModel> rectangles, int stWidth, int sHeight)
        {
            int sheetWidth = stWidth;
            int sheetHeight = sHeight;
            List<RectangleModel> firstsInColumns = new List<RectangleModel>();
            int heightOffset = 0;
            int widthOffset = 0;
            firstsInColumns.Add(rectangles.First());
            foreach (RectangleModel rectangleModel in rectangles)
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

        private List<RectangleModel> SortRectanglesDecreasing(List<RectangleModel> rectangles)
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
            List<RectangleModel> sortedRectangles = rectangles.OrderByDescending(o => o.Size.Width).ToList();

            return sortedRectangles;
        }
        public List<RectangleModel> CreateRandomListOfRectangles(int numberOfRectangles)
        {
            if (numberOfRectangles < 0) { throw new ArgumentException(nameof(numberOfRectangles)); }
       
            List<RectangleModel> rectangles = new List<RectangleModel>();

            for (int i = 0; i < numberOfRectangles; i++)
            {
                RectangleModel rectangle = new RectangleModel();
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
