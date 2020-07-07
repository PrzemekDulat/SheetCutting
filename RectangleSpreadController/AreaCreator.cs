using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RectangleSpreadController
{
    public class AreaCreator
    {
        public List<LineModel> AssembledLines(List<RectangleModel> rectangles)
        {
            return SetLinesLocation(rectangles);
        }
        public List<LineModel> HorizontalRectangleLines(List<RectangleModel> rectangles)
        {
            return DetermineHorizontalLinesOfRectangles(rectangles);
        }
        public List<LineModel> VerticalRectangleLines(List<RectangleModel> rectangles)
        {
            return DetermineVerticalLinesOfRectangles(rectangles);
        }
        public List<LineModel> VerticalCuttingLines(List<RectangleModel> rectangles)
        {
            return CheckIfVerticalLinesIntersect(SetVerticalLinesLocation(rectangles, 500), DetermineHorizontalLinesOfRectangles(rectangles));
        }
        public List<LineModel> HorizontalCuttingLines(List<RectangleModel> rectangles)
        {
            return CheckIfHorizontalLinesIntersect(SetHorizontalLinesLocation(rectangles, 1500), DetermineVerticalLinesOfRectangles(rectangles));
        }




        public List<LineModel> SetLinesLocation(List<RectangleModel> rectangles)
        {
            List<LineModel> allLines = SetVerticalLinesLocation(rectangles, 500);

            return allLines.Concat(SetHorizontalLinesLocation(rectangles, 1500)).ToList();
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
        public List<LineModel> SetHorizontalLinesLocation(List<RectangleModel> rectangles, int sheetWidth)
        {
            List<LineModel> lines = new List<LineModel>();

            foreach (var rectangle in rectangles)
            {
                Point startLocationFirstHorizontal = new Point(0, rectangle.Location.Y);
                Point endLocationFirstHorizontal = new Point(sheetWidth, rectangle.Location.Y);
                LineModel firstHorizontalLine = LineModel.CreateLine(startLocationFirstHorizontal, endLocationFirstHorizontal);

                Point startLocationSecondHorizontal = new Point(0, rectangle.Location.Y + rectangle.Size.Height);
                Point endLocationSecondHorizontal = new Point(sheetWidth, rectangle.Location.Y + rectangle.Size.Height);
                LineModel secondHorizontalLine = LineModel.CreateLine(startLocationSecondHorizontal, endLocationSecondHorizontal);

                lines.Add(firstHorizontalLine);
                lines.Add(secondHorizontalLine);
            }

            return lines;
        }





        public List<LineModel> DetermineHorizontalLinesOfRectangles(List<RectangleModel> rectangles)
        {
            List<LineModel> lines = new List<LineModel>();
            foreach (var rectangle in rectangles)
            {
                Point firstStartLocation = rectangle.Location;
                Point firstEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                LineModel firstLine = LineModel.CreateLine(firstStartLocation, firstEndLocation);

                Point secondStartLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);
                Point secondEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
                LineModel secondLine = LineModel.CreateLine(secondStartLocation, secondEndLocation);

                lines.Add(firstLine);
                lines.Add(secondLine);
            }
            return lines;
        }
        public List<LineModel> DetermineVerticalLinesOfRectangles(List<RectangleModel> rectangles)
        {
            List<LineModel> lines = new List<LineModel>();
            foreach (var rectangle in rectangles)
            {
                Point firstStartLocation = rectangle.Location;
                Point firstEndLocation = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Size.Height);
                LineModel firstLine = LineModel.CreateLine(firstStartLocation, firstEndLocation);

                Point secondStartLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y);
                Point secondEndLocation = new Point(rectangle.Location.X + rectangle.Size.Width, rectangle.Location.Y + rectangle.Size.Height);
                LineModel secondLine = LineModel.CreateLine(secondStartLocation, secondEndLocation);

                lines.Add(firstLine);
                lines.Add(secondLine);
            }
            return lines;
        }




        public List<LineModel> CheckIfVerticalLinesIntersect(List<LineModel> verticalLines, List<LineModel> horizontalRectangleLines)
        {
            List<LineModel> verticalCuttingLines = new List<LineModel>();
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
        public List<LineModel> CheckIfHorizontalLinesIntersect(List<LineModel> horizontalLines, List<LineModel> verticalRectangleLines)
        {
            List<LineModel> horizontalCuttingLines = new List<LineModel>();
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




        public List<AreaModel> CreateAreasBasedOnVerticalLines(List<LineModel> verticalCuttingLines)
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
        public List<AreaModel> CreateAreasBasedOnHorizontal(List<LineModel> horizontalCuttingLines)
        {
            List<AreaModel> areas = new List<AreaModel>();
            int widthOfPreviousArea = 0;
            for (int i = 0; i < horizontalCuttingLines.Count - 1; i++)
            {
                AreaModel area = new AreaModel();
                area.Location = new Point(/*width poprzedniej arei*/widthOfPreviousArea, );
                area.Location = horizontalCuttingLines[i].StartLocation; // to trzeba poprawic, bo dla pierwszej arei to dziala ale dla drugiej znowu jest  (0,0)
                area.Size = new Size(horizontalCuttingLines[i].EndLocation.X - horizontalCuttingLines[i].StartLocation.X,horizontalCuttingLines[i + 1].StartLocation.Y - horizontalCuttingLines[i].StartLocation.Y);
                areas.Add(area);
            }
            widthOfPreviousArea = areas[0].Size.Width;
            return areas;
        }




        public List<AreaModel> DetermineRectangleInArea(List<AreaModel> areas, List<RectangleModel> rectangles)
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
