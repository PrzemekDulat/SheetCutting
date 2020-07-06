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

        public List<LineModel> AssembledLines(List<RectangleModel> rectangles)
        {
            return SetLinesLocation(rectangles);
        }

        public List<LineModel> RectangleLines(List<RectangleModel> rectangles)
        {
            return DetermineLinesOfRectangles(rectangles);
        }

        public List<LineModel> OkayLines(List<RectangleModel> rectangles)
        {
            return CheckIfLinesIntersect(SetVerticalLinesLocation(rectangles, 500), DetermineLinesOfRectangles(rectangles));
        }
        private List<RectangleModel> SetupRectanglesLocation(List<RectangleModel> rectangles, int stWidth, int sHeight)
        {
            //int nieWiemJakToNazwac = 0;
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
            List<LineModel> allLines = SetVerticalLinesLocation(rectangles, 500);
            
            return allLines.Concat(SetHorizontalLinesLocation(rectangles)).ToList();
        }
        public List<LineModel> SetVerticalLinesLocation(List<RectangleModel> rectangles, int sheetHeight)
        {
            List<LineModel> lines = new List<LineModel>();

            foreach (var rectangle in rectangles)
            {
                Point startLocationFirstVerticalLine = new Point(rectangle.Location.X, 0);
                Point endLocationFirstVerticalLine = new Point(rectangle.Location.X, sheetHeight);
                LineModel firstVerticalLine = LineModel.CreateLine(startLocationFirstVerticalLine, endLocationFirstVerticalLine);

                Point startLocation = new Point(rectangle.Location.X + rectangle.Size.Width, 0);
                Point endLocation = new Point(rectangle.Location.X + rectangle.Size.Width, sheetHeight);
                LineModel secondVerticalLine = LineModel.CreateLine(startLocation, endLocation);


                lines.Add(firstVerticalLine);
                lines.Add(secondVerticalLine);
            }
            
            return lines;
        }
        public List<LineModel> SetHorizontalLinesLocation(List<RectangleModel> rectangles)
        {
            List<LineModel> lines = new List<LineModel>();

            foreach (var rectangle in rectangles)
            {

                Point startLocationFirstHorizontal = new Point(0, rectangle.Location.Y);
                Point endLocationFirstHorizontal = new Point(500, rectangle.Location.Y);
                LineModel firstHorizontalLine = LineModel.CreateLine(startLocationFirstHorizontal, endLocationFirstHorizontal);

                Point startLocationSecondHorizontal = new Point(0, rectangle.Location.Y + rectangle.Size.Height);
                Point endLocationSecondHorizontal = new Point(500, rectangle.Location.Y + rectangle.Size.Height);
                LineModel secondHorizontalLine = LineModel.CreateLine(startLocationSecondHorizontal, endLocationSecondHorizontal);

                lines.Add(firstHorizontalLine);
                lines.Add(secondHorizontalLine);
            }

            return lines;
        }

        public List<LineModel> DetermineLinesOfRectangles(List<RectangleModel> rectangles)
        {
            List<LineModel> lines = new List<LineModel>();
            foreach (var rectangle in rectangles)
            {

                Point startLocationFirstHorizontal = new Point(0, rectangle.Location.Y);
                Point endLocationFirstHorizontal = new Point(500, rectangle.Location.Y);
                LineModel firstHorizontalLine = LineModel.CreateLine(startLocationFirstHorizontal, endLocationFirstHorizontal);

                Point firstStartLocation = rectangle.Location;
                Point firstEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                LineModel firstLine = LineModel.CreateLine(firstStartLocation, firstEndLocation);
                
                Point secondStartLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);
                Point secondEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
                LineModel secondLine = LineModel.CreateLine(secondStartLocation, secondEndLocation);

                //LineModel thirdLine = new LineModel();
                //thirdLine.startLocation = rectangle.Location;
                //thirdLine.endLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);

                //LineModel fourthLine = new LineModel();
                //fourthLine.startLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                //fourthLine.endLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);

                lines.Add(firstLine);
                lines.Add(secondLine);
                //lines.Add(thirdLine);
                //lines.Add(fourthLine);
            }
            return lines;
        }

        //check if line 
        public List<LineModel> CheckIfLinesIntersect(List<LineModel> verticalLines, List<LineModel> horizontalRectangleLines)
        {
            List<LineModel> okLines = new List<LineModel>();
            foreach (var line in verticalLines) //check every vertical line if collides with any rectangle
            {
                bool isTrue = true;             //if it collides then FALSE else GREEN
                foreach (var horizontalRectangleLine in horizontalRectangleLines)
                {
                    if ((line.StartLocation.X > horizontalRectangleLine.StartLocation.X && line.StartLocation.X < horizontalRectangleLine.EndLocation.X))
                    {
                        isTrue = false;
                    }
                }
                if (isTrue)
                {
                    okLines.Add(line);
                }
            }


            okLines = okLines.Distinct(new CompareByStartingPoint()).ToList();


            return okLines;
        }

        public List<AreaModel> CreateAreasBasedOnLines(List<LineModel> okLines)
        {
            List<AreaModel> areas = new List<AreaModel>();


            for (int i = 0; i < okLines.Count - 1; i++)
            {
                AreaModel area = new AreaModel();

                area.Location = okLines[i].StartLocation;
                area.Size = new Size(okLines[i+1].StartLocation.X - okLines[i].StartLocation.X, okLines[i].EndLocation.Y);

                areas.Add(area);
            }


            return areas;
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
