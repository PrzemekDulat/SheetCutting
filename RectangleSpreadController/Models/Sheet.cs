using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RectangleSpreadController
{
    public class Sheet : ISheet
    {
        private Sheet(OrderRelatedElement[] orderRelatedElements, Point point, MaterialType materialType, Size size)
        {
            OrderRelatedElements = orderRelatedElements;
            Location = point;
            Size = size;
            MaterialType = materialType;
        }
        public Point Location { get; }
        public MaterialType MaterialType { get; }
        public OrderRelatedElement[] OrderRelatedElements { get; set; }
        public Size Size { get; }



        public static ISheet Create(Sheet sheet, OrderRelatedElement[] orderRelatedElements, MaterialType materialType, Point point, Size size)
        {
            if (orderRelatedElements.Length == 0) { return new Waste(sheet, default, default); }//TODO fill data
            if (orderRelatedElements.Length == 1 && orderRelatedElements.First().Size == size) { return orderRelatedElements.First(); }
            else { return new Sheet(orderRelatedElements, point, materialType, size); };

        }
        public static Sheet CreateNewSheet(OrderRelatedElement[] orderRelatedElements, MaterialType materialType, Size size)
        {
            return new Sheet(orderRelatedElements, new Point(0, 0), materialType, size);
        }



        public static ISheet[] CutSheet(Sheet sheet, ICutLine line)
        {
            ISheet[] sheets = new ISheet[] { default, default };
            switch (line)
            {
                case VerticalCutLine vertical:
                    sheets = CutVertical(sheet, vertical);
                    break;

                case HorizontalCutLine horizontal:
                    sheets = CutHorizontal(sheet, horizontal);
                    break;
            }
            return sheets;
        }
        public ICutLine[] GetCutLines(Sheet sheet)
        {
            List<ICutLine> resultList = new List<ICutLine>();

            OrderRelatedElement[] orderRelatedElements = sheet.OrderRelatedElements;
            Point sheetLocation = sheet.Location;
            Size sheetSize = sheet.Size;

            //get horizontal lines
            foreach (var item in orderRelatedElements)
            {
                resultList.Add(new HorizontalCutLine(item.Location.Y - sheet.Location.Y));
                resultList.Add(new HorizontalCutLine(item.Size.Height + (item.Location.Y - sheet.Location.Y)));
            }

            //get vertical lines
            foreach (var item in orderRelatedElements)
            {
                resultList.Add(new VerticalCutLine(item.Location.X - sheet.Location.X));
                resultList.Add(new VerticalCutLine(item.Size.Width + (item.Location.X - sheet.Location.X)));
            }

            //filter out lines that are the same as borders
            List<ICutLine> notValidLines = new List<ICutLine>();
            notValidLines.Add(new VerticalCutLine(0));
            notValidLines.Add(new VerticalCutLine(sheetSize.Width));
            notValidLines.Add(new HorizontalCutLine(0));
            notValidLines.Add(new HorizontalCutLine(sheetSize.Height));


            List<ICutLine> linesWithoutBorderLines = RemoveLines(resultList, notValidLines);

            List<ICutLine> distinctLines = linesWithoutBorderLines
            .GroupBy(m => new { m.LineType, m.Value })
            .Select(group => group.First())
            .ToList();

            return distinctLines.ToArray();
        }
        public static List<ICutLine> RemoveLines(List<ICutLine> resultList, List<ICutLine> notValidLines)
        {
            return resultList.Where(v => !notValidLines.Any(x => x.Equals(v))).ToList();
        }

        public ICutLine GetFirstValidCutLine(ICutLine[] cutLines, Sheet sheet)
        {
            //return cutLines.FirstOrDefault(x => CheckIfLineIsValidCutLine(x, sheet)) ?? throw new ArgumentException($"In argument {nameof(cutLines)} there is no valid cutline");
            return DetermineBestCutLine(sheet.OrderRelatedElements, CheckIfLineIsValidCutLine(cutLines, sheet), sheet) ?? throw new ArgumentException($"In argument {nameof(cutLines)} there is no valid cutline");
        }

        private static List<ICutLine> CheckIfLineIsValidCutLine(ICutLine[] cutLines, Sheet sheet)
        {
            List<ICutLine> validLines = new List<ICutLine>();

            var rectangles = sheet.OrderRelatedElements;
            bool cutLineFailedForSomeRectangle = false;
            foreach (var cutLine in cutLines)
            {
                cutLineFailedForSomeRectangle = false;
                foreach (var rectangle in rectangles)
                {
                    if (cutLine.LineType == LineType.Horizontal
                        && (cutLine.Value > (rectangle.Location.Y - sheet.Location.Y)
                        && cutLine.Value < (rectangle.Location.Y - sheet.Location.Y) + rectangle.Size.Height))
                    {
                        cutLineFailedForSomeRectangle = true;
                    }
                    else if (cutLine.LineType == LineType.Vertical
                        && (cutLine.Value > (rectangle.Location.X - sheet.Location.X)
                        && cutLine.Value < (rectangle.Location.X - sheet.Location.X) + rectangle.Size.Width))
                    {
                        cutLineFailedForSomeRectangle = true;
                    }
                }

                if (!cutLineFailedForSomeRectangle)
                {
                    validLines.Add(cutLine);
                }
            }
            //List<int> lengths = new List<int>();

            //foreach (var item in validLines)
            //{
            //    lengths.Add(item.Lenght(sheet.OrderRelatedElements, sheet));
            //}
            //TODO
            return validLines.Where(x => x.Lenght(sheet.OrderRelatedElements, sheet) <= 20000).ToList(); ;
        }

       

        public static ICutLine DetermineBestCutLine(OrderRelatedElement[] rectangles, List<ICutLine> validLines, Sheet sheet)
        {
            var test2 = validLines.Select(x => (x, x.CountRectanglesWhereLineIsPartOf(rectangles, sheet))).ToList();
            var test = validLines.OrderByDescending(x => x.CountRectanglesWhereLineIsPartOf(rectangles, sheet)).First();
            return test;
        }
    
        public static IOrderedEnumerable<KeyValuePair<ICutLine, int>> OrderByDescending(Dictionary<ICutLine, int> linesToNumberOfRectanglesMap)
        {
            return linesToNumberOfRectanglesMap.OrderByDescending(x => x.Value);
        }

        private static ISheet[] CutHorizontal(Sheet sheet, HorizontalCutLine horizontal)
        {
            Point locationForFirstSheet = sheet.Location;
            Size sizeForFirstSheet = new Size(sheet.Size.Width, horizontal.Value);
            ISheet[] returnSheets = new ISheet[2];
            Sheet[] sheets = new Sheet[2];

            sheets[0] = new Sheet(GetRectanglesOfNewSheet(sheet, locationForFirstSheet, sizeForFirstSheet), locationForFirstSheet, default, sizeForFirstSheet);

            Point locationForSecondSheet = new Point(sheet.Location.X, horizontal.Value + sheets[0].Location.Y);
            Size sizeForSecondSheet = new Size(sheet.Size.Width, sheet.Size.Height - horizontal.Value);

            sheets[1] = new Sheet(GetRectanglesOfNewSheet(sheet, locationForSecondSheet, sizeForSecondSheet), locationForSecondSheet, default, sizeForSecondSheet);

            for (int i = 0; i < sheets.Count(); i++)
            {//check if last element size equals to size of sheet, or if 0 elements then its a waste
                if (sheets[i].OrderRelatedElements.Count() == 1 && sheets[i].OrderRelatedElements[0].Size == sheets[i].Size)
                {
                    returnSheets[i] = sheets[i].OrderRelatedElements[0];
                }
                else if (sheets[i].OrderRelatedElements.Count() == 0)
                {
                    returnSheets[i] = new Waste(sheets[i], sheets[i].Location, sheets[i].Size);
                }
                else
                {
                    returnSheets[i] = sheets[i];
                }
            }
            return returnSheets;
        }
        private static OrderRelatedElement[] GetRectanglesOfNewSheet(Sheet sheet, Point location, Size size)
        {
            OrderRelatedElement[] rectangles = sheet.OrderRelatedElements;
            List<OrderRelatedElement> resultRectangles = new List<OrderRelatedElement>();
            foreach (var rectangle in rectangles)
            {
                if ((rectangle.Location.X + rectangle.Size.Width) <= (location.X + size.Width)
                                        && (rectangle.Location.X) >= (location.X)
                                        && (rectangle.Location.Y) >= (location.Y)
                && (rectangle.Location.Y + rectangle.Size.Height) <= (location.Y + size.Height))
                {
                    resultRectangles.Add(rectangle);
                }
            }

            return resultRectangles.ToArray();
        }
        private static ISheet[] CutVertical(Sheet sheet, VerticalCutLine vertical)
        {
            Point locationForFirstSheet = sheet.Location;
            Size sizeForFirstSheet = new Size(vertical.Value, sheet.Size.Height);
            ISheet[] returnSheets = new ISheet[2];

            Sheet[] sheets = new Sheet[2];

            sheets[0] = new Sheet(GetRectanglesOfNewSheet(sheet, locationForFirstSheet, sizeForFirstSheet), locationForFirstSheet, default, sizeForFirstSheet);

            Point locationForSecondSheet = new Point(vertical.Value + sheets[0].Location.X, sheet.Location.Y);
            Size sizeForSecondSheet = new Size(sheet.Size.Width - vertical.Value, sheet.Size.Height);

            sheets[1] = new Sheet(GetRectanglesOfNewSheet(sheet, locationForSecondSheet, sizeForSecondSheet), locationForSecondSheet, default, sizeForSecondSheet);

            for (int i = 0; i < sheets.Count(); i++)
            {//check if last element size equals to size of sheet, or if 0 elements then its a waste
                if (sheets[i].OrderRelatedElements.Count() == 1 && sheets[i].OrderRelatedElements[0].Size == sheets[i].Size)
                {
                    returnSheets[i] = sheets[i].OrderRelatedElements[0];
                }
                else if (sheets[i].OrderRelatedElements.Count() == 0)
                {
                    returnSheets[i] = new Waste(sheets[i], sheets[i].Location, sheets[i].Size);
                }
                else
                {
                    returnSheets[i] = sheets[i];
                }
            }
            return returnSheets;
        }
    }
}