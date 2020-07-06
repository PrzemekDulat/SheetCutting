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
        public List<RectangleModel> AssembledRectangles()
        {
            return SetRectanglesLocation(SortRectanglesDecreasing(CreateRandomListOfRectangles(24)), 1500, 500);
        }

        public List<LineModel> AssembledLines()
        {
            return SetLinesLocation(AssembledRectangles());
        }

        private List<RectangleModel> SetRectanglesLocation(List<RectangleModel> rectangles, int stWidth, int sHeight)
        {
            int sheetWidth = stWidth;
            int sheetHeight = sHeight;
            List<int> borderRectangles = new List<int>() { 0 };
            int heightOffset = 0;
            int widthOffset = 0;

            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rectangles[i].Size.Height + heightOffset <= sheetHeight) //fits by height
                {
                    rectangles[i].Location = new Point(widthOffset, heightOffset);
                    heightOffset = heightOffset + rectangles[i].Size.Height;
                }
                else //offset to the right
                {
                    widthOffset = 0;
                    foreach (var bRectangle in borderRectangles)
                    {
                        widthOffset = widthOffset + rectangles[bRectangle].Size.Width;
                    }

                    borderRectangles.Add(i);
                    heightOffset = 0;
                    i--;
                }
            }

            return rectangles;
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
