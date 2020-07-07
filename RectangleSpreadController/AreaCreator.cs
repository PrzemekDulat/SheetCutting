using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RectangleSpreadController
{
    public class AreaCreator
    {
        public List<LineModelOld> AssembledLines(List<OrderRelatedElement> rectangles)
        {
            return SetLinesLocation(rectangles);
        }
        public List<LineModelOld> HorizontalRectangleLines(List<OrderRelatedElement> rectangles)
        {
            return DetermineHorizontalLinesOfRectangles(rectangles);
        }
        public List<LineModelOld> VerticalRectangleLines(List<OrderRelatedElement> rectangles)
        {
            return DetermineVerticalLinesOfRectangles(rectangles);
        }
        public List<LineModelOld> VerticalCuttingLines(List<OrderRelatedElement> rectangles)
        {
            return CheckIfVerticalLinesIntersect(SetVerticalLinesLocation(rectangles, 500), DetermineHorizontalLinesOfRectangles(rectangles));
        }
        public List<LineModelOld> HorizontalCuttingLines(List<OrderRelatedElement> rectangles)
        {
            return CheckIfHorizontalLinesIntersect(SetHorizontalLinesLocation(rectangles, 1500), DetermineVerticalLinesOfRectangles(rectangles));
        }




        public List<LineModelOld> SetLinesLocation(List<OrderRelatedElement> rectangles)
        {
            List<LineModelOld> allLines = SetVerticalLinesLocation(rectangles, 500);

            return allLines.Concat(SetHorizontalLinesLocation(rectangles, 1500)).ToList();
        }
        public List<LineModelOld> SetVerticalLinesLocation(List<OrderRelatedElement> rectangles, int sheetHeight)
        {
            List<LineModelOld> lines = new List<LineModelOld>();

            foreach (var rectangle in rectangles)
            {
                Point startLocationFirstVerticalLine = new Point(rectangle.Location.X, 0);
                Point endLocationFirstVerticalLine = new Point(rectangle.Location.X, sheetHeight);
                LineModelOld firstVerticalLine = LineModelOld.CreateLine(startLocationFirstVerticalLine, endLocationFirstVerticalLine);

                Point startLocation = new Point(rectangle.Location.X + rectangle.Size.Width, 0);
                Point endLocation = new Point(rectangle.Location.X + rectangle.Size.Width, sheetHeight);
                LineModelOld secondVerticalLine = LineModelOld.CreateLine(startLocation, endLocation);

                lines.Add(firstVerticalLine);
                lines.Add(secondVerticalLine);
            }

            return lines;
        }
        public List<LineModelOld> SetHorizontalLinesLocation(List<OrderRelatedElement> rectangles, int sheetWidth)
        {
            List<LineModelOld> lines = new List<LineModelOld>();

            foreach (var rectangle in rectangles)
            {
                Point startLocationFirstHorizontal = new Point(0, rectangle.Location.Y);
                Point endLocationFirstHorizontal = new Point(sheetWidth, rectangle.Location.Y);
                LineModelOld firstHorizontalLine = LineModelOld.CreateLine(startLocationFirstHorizontal, endLocationFirstHorizontal);

                Point startLocationSecondHorizontal = new Point(0, rectangle.Location.Y + rectangle.Size.Height);
                Point endLocationSecondHorizontal = new Point(sheetWidth, rectangle.Location.Y + rectangle.Size.Height);
                LineModelOld secondHorizontalLine = LineModelOld.CreateLine(startLocationSecondHorizontal, endLocationSecondHorizontal);

                lines.Add(firstHorizontalLine);
                lines.Add(secondHorizontalLine);
            }

            return lines;
        }





        public List<LineModelOld> DetermineHorizontalLinesOfRectangles(List<OrderRelatedElement> rectangles)
        {
            List<LineModelOld> lines = new List<LineModelOld>();
            foreach (var rectangle in rectangles)
            {
                Point firstStartLocation = rectangle.Location;
                Point firstEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                LineModelOld firstLine = LineModelOld.CreateLine(firstStartLocation, firstEndLocation);

                Point secondStartLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);
                Point secondEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
                LineModelOld secondLine = LineModelOld.CreateLine(secondStartLocation, secondEndLocation);

                lines.Add(firstLine);
                lines.Add(secondLine);
            }
            return lines;
        }
        public List<LineModelOld> DetermineVerticalLinesOfRectangles(List<OrderRelatedElement> rectangles)
        {
            List<LineModelOld> lines = new List<LineModelOld>();
            foreach (var rectangle in rectangles)
            {
                Point firstStartLocation = rectangle.Location;
                Point firstEndLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);
                LineModelOld firstLine = LineModelOld.CreateLine(firstStartLocation, firstEndLocation);

                Point secondStartLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                Point secondEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
                LineModelOld secondLine = LineModelOld.CreateLine(secondStartLocation, secondEndLocation);

                lines.Add(firstLine);
                lines.Add(secondLine);
            }
            return lines;
        }




        public List<LineModelOld> CheckIfVerticalLinesIntersect(List<LineModelOld> verticalLines, List<LineModelOld> horizontalRectangleLines)
        {
            List<LineModelOld> verticalCuttingLines = new List<LineModelOld>();
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
                    verticalCuttingLines.Add(line);
                }
            }

            verticalCuttingLines = verticalCuttingLines.Distinct(new CompareByStartingPoint()).ToList();
            return verticalCuttingLines;
        }
        public List<LineModelOld> CheckIfHorizontalLinesIntersect(List<LineModelOld> horizontalLines, List<LineModelOld> verticalRectangleLines)
        {
            List<LineModelOld> horizontalCuttingLines = new List<LineModelOld>();
            foreach (var line in horizontalLines) //check every horizontal line if collides with any rectangle
            {
                bool isTrue = true;             //if it collides then FALSE else GREEN
                foreach (var verticalRectangleLine in verticalRectangleLines)
                {
                    if (line.StartLocation.Y > verticalRectangleLine.StartLocation.Y && line.StartLocation.Y < verticalRectangleLine.EndLocation.Y)
                    {
                        isTrue = false;
                    }

                }
                if (isTrue)
                {
                    horizontalCuttingLines.Add(line);
                }
            }

            horizontalCuttingLines = horizontalCuttingLines.Distinct(new CompareByStartingPoint()).ToList();
            return horizontalCuttingLines;
        }




        public List<AreaModel> CreateAreasBasedOnVerticalLines(List<LineModelOld> verticalCuttingLines)
        {
            List<AreaModel> areas = new List<AreaModel>();

            for (int i = 0; i < verticalCuttingLines.Count - 1; i++)
            {
                AreaModel area = new AreaModel();

                area.Location = verticalCuttingLines[i].StartLocation;
                area.Size = new Size(verticalCuttingLines[i + 1].StartLocation.X - verticalCuttingLines[i].StartLocation.X, verticalCuttingLines[i].EndLocation.Y);

                areas.Add(area);
            }
            return areas;
        }
        public List<AreaModel> CreateAreasBasedOnHorizontal(List<LineModelOld> horizontalCuttingLines)
        {
            List<AreaModel> areas = new List<AreaModel>();
            int widthOfPreviousArea = 0;
            for (int i = 0; i < horizontalCuttingLines.Count - 1; i++)
            {
                AreaModel area = new AreaModel();
                area.Location = new Point(/*width poprzedniej arei*/widthOfPreviousArea, default );
                area.Location = horizontalCuttingLines[i].StartLocation; // to trzeba poprawic, bo dla pierwszej arei to dziala ale dla drugiej znowu jest  (0,0)
                area.Size = new Size(horizontalCuttingLines[i].EndLocation.X - horizontalCuttingLines[i].StartLocation.X,horizontalCuttingLines[i + 1].StartLocation.Y - horizontalCuttingLines[i].StartLocation.Y);
                areas.Add(area);
            }
            widthOfPreviousArea = areas[0].Size.Width;
            return areas;
        }




        public List<AreaModel> DetermineRectangleInArea(List<AreaModel> areas, List<OrderRelatedElement> rectangles)
        {
            foreach (var area in areas)
            {
                foreach (var rectangle in rectangles)
                {
                    if ((rectangle.Location.X + rectangle.Size.Width) <= (area.Location.X + area.Size.Width)
                                            && (rectangle.Location.X) >= (area.Location.X)
                                            && (rectangle.Location.Y) >= (area.Location.Y)
                    && (rectangle.Location.Y + rectangle.Size.Height) <= (area.Location.Y + area.Size.Height))
                    {
                        area.Rectangles.Add(rectangle);
                    }
                }
            }
            return areas;
        }



        public void CutAreasHorizontaly(List<AreaModel> areas)
        {
            foreach (var area in areas)
            {
                var verticalRectangleLines = DetermineVerticalLinesOfRectangles(area.Rectangles); // mam linie prostokatow | z arei chck
                var horizontalLines = SetHorizontalLinesLocation(area.Rectangles, area.Size.Width); // chck
                var horizontalCuttingLines = CheckIfHorizontalLinesIntersect(horizontalLines, verticalRectangleLines); // chck

                var areasCutHorizontal = CreateAreasBasedOnHorizontal(horizontalCuttingLines); // stworz aree based on horizontal cutting lines
            }

            //CutAreasVerticaly(areas);
        }
        public void CutAreasVerticaly(List<AreaModel> areas)
        {



            //CutAreasHorizontaly(areas);
        }
    }
}
