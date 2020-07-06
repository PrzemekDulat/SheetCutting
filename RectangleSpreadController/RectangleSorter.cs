using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace RectangleSpreadController
{
    public class RectangleSorter
    {
        public List<RectangleModel> GenerateRandomRectanglesAndPositionThem()
        {
            List<RectangleModel> rectangles = CreateRandomListOfRectangles(24);
            List<RectangleModel> rectanglesDesc = SortRectanglesDecreasing(rectangles);
            List<RectangleModel> rectanglesPositioned = SetupRectanglesLocation(rectanglesDesc, 1500, 500);
            return rectanglesPositioned;
        }

        public List<LineModel> AssembledLines(List<RectangleModel> rectangles)
        {
            return SetLinesLocation(rectangles);
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
        public List<LineModel> SetLinesLocation(List<RectangleModel> rectangles)
        {
            List<LineModel> lines = new List<LineModel>();

            //foreach (var rectangle in rectangles)
            //{
            //    LineModel line = new LineModel();
            //    line.startLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
            //    line.endLocation = new Point(rectangle.Location.X + rectangle.Size.Width, 500);

            //    line.startLocation = new Point(rectangle.Size.Width, rectangle.Size.Height);
            //    line.endLocation = new Point(rectangle.Size.Width, 500);
            //    lines.Add(line);
            //}
            LineModel line = new LineModel();
            line.startLocation = new Point(rectangles[0].Size.Width,0);
            line.endLocation = new Point(rectangles[0].Size.Width, 1500);

            //line.startLocation = new Point(100, 0);
            //line.endLocation = new Point(100, 500);
            lines.Add(line);

            return lines;
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
