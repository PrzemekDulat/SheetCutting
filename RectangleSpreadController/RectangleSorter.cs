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
            return SetRectanglesLocation(SortRectanglesDecreasing(CreateRandomListOfRectangles(5)), 1500, 500);
        }

        private List<RectangleModel> SetRectanglesLocation(List<RectangleModel> rectangles, int stWidth, int sHeight)
        {
            int sheetWidth = stWidth;
            int sheetHeight = sHeight;
            bool firstIteration = false;
            List<int> borderRectangles = new List<int>() { 0 };
            List<int> columnRectangles = new List<int>();


            //for (int i = 0; i < rectangles.Count; i++)
            //{



            //    if (firstIteration)
            //    {
            //        //if (heightOffset + rectangles[i].Size.Height <= sheetHeight)
            //        //{
            //        //    rectangles[i-1].Location = new Point(widthOffset, heightOffset);

            //        //    heightOffset += rectangles[i].Size.Height;
            //        //    //rectangles[i].Location = new Point(widthOffset, heightOffset - rectangles[i].Size.Height);

            //        //}
            //        if (heightOffset + rectangles[i].Size.Height <= sheetHeight)
            //        {
            //            rectangles[i].Location = new Point(widthOffset, heightOffset);
            //            heightOffset += rectangles[i].Size.Height;
            //        }
            //        else
            //        {
            //            if (widthOffset + rectangles[i].Size.Width <= sheetWidth)
            //            {
            //                widthOffset = 0;

            //                foreach (var borderRectangle in borderRectangles)
            //                {
            //                    widthOffset += rectangles[borderRectangle].Size.Width;
            //                }

            //                borderRectangles.Add(i);
            //                heightOffset = 0 - rectangles[i - 1].Size.Height;

            //                i--;
            //            }
            //            else
            //            {
            //                //TODO
            //            }

            //        }

            //    }
            //    else
            //    {
            //        rectangles[i].Location = new Point(0, 0);
            //        heightOffset += rectangles[0].Size.Height;
            //    }

            //    firstIteration = true;
            //}
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
                    widthOffset = rectangles[0].Size.Width;
                    i--;
                    heightOffset = 0;
                    //pass = true;
                }
            }

            return rectangles;
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
