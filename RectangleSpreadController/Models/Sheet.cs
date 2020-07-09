using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
                resultList.Add(new HorizontalCutLine(item.Location.Y));
                resultList.Add(new HorizontalCutLine(item.Location.Y + item.Size.Height));
            }

            //get vertical lines
            foreach (var item in orderRelatedElements)
            {
                resultList.Add(new VerticalCutLine(item.Location.X));
                resultList.Add(new VerticalCutLine(item.Location.X + item.Size.Width));
            }

            //filter out lines that are the same as borders
            List<ICutLine> notValidLines = new List<ICutLine>();
            notValidLines.Add(new VerticalCutLine(sheetLocation.X));
            notValidLines.Add(new VerticalCutLine(sheetLocation.X + sheetSize.Width));
            notValidLines.Add(new HorizontalCutLine(sheetLocation.Y));
            notValidLines.Add(new HorizontalCutLine(sheetLocation.Y + sheetSize.Height));


            var hComparer = new CompareHorizontalLines();
            var vComparer = new CompareVerticalLines();

            //List<HorizontalCutLine> horizontalResultList = horizontalLines.Except(notValidHorizontalLines, hComparer).ToList();
            //List<VerticalCutLine> verticalResultList = varticalLines.Except(notValidVerticalLines, vComparer).ToList();
            //horizontalLines.Where(v => !notValidHorizontalLines.Contains(v));
            //List<ICutLine> lineff = new List<ICutLine>();
            //resultList = resultList.Distinct(new CompareHorizontalLines()).ToList();

            List<ICutLine> resultList2 = RemoveLines(resultList, notValidLines);
            return resultList2.ToArray();
        }

        public static List<ICutLine> RemoveLines(List<ICutLine> resultList, List<ICutLine> notValidLines)
        {
            return resultList.Where(v => !notValidLines.Any(x => x.Equals(v))).ToList();
        }

        public ICutLine GetFirstValidCutLine(ICutLine[] cutLines)
        {
            return cutLines.FirstOrDefault(CheckIfLineIsValidCutLine) ?? throw new ArgumentException($"In argument {nameof(cutLines)} there is no valid cutline");
        }

        private static bool CheckIfLineIsValidCutLine(ICutLine cutLine)
        {
            //todo
            if (cutLine.LineType == LineType.Horizontal)
            {
                
            }
            throw new NotImplementedException();
        }

        private static ISheet[] CutHorizontal(Sheet sheet, HorizontalCutLine horizontal)
        {
            throw new NotImplementedException();
        }
        private static ISheet[] CutVertical(Sheet sheet, VerticalCutLine vertical)
        {
            //TODO return 2 new sheets with proper starting points and proper OrderRealtedElements
            throw new NotImplementedException();
        }
    }
}