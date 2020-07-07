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
            OrderRelatedElement[] orderRelatedElements = sheet.OrderRelatedElements;
            Point sheetLocation = sheet.Location;
            Size sheetSize = sheet.Size;

            List<HorizontalCutLine> horizontalLines = new List<HorizontalCutLine>();
            //get horizontal lines
            foreach (var item in orderRelatedElements)
            {
                HorizontalCutLine horizontalCutLine = new HorizontalCutLine(item.Location.Y);
                horizontalLines.Add(new HorizontalCutLine(item.Location.Y));
                horizontalLines.Add(new HorizontalCutLine(item.Location.Y + item.Size.Height));
            }

            List<VerticalCutLine> varticalLines = new List<VerticalCutLine>();
            //get vertical lines
            foreach (var item in orderRelatedElements)
            {
                varticalLines.Add(new VerticalCutLine(item.Location.X));
                varticalLines.Add(new VerticalCutLine(item.Location.X + item.Size.Width));
            }
            //filter out lines that are the same as borders
            List<VerticalCutLine> notValidVerticalLines = new List<VerticalCutLine>();
            notValidVerticalLines.Add(new VerticalCutLine(sheetLocation.X));
            notValidVerticalLines.Add(new VerticalCutLine(sheetLocation.X + sheetSize.Width));

            List<HorizontalCutLine> notValidHorizontalLines = new List<HorizontalCutLine>();
            notValidHorizontalLines.Add(new HorizontalCutLine(sheetLocation.Y));
            notValidHorizontalLines.Add(new HorizontalCutLine(sheetLocation.Y + sheetSize.Height));


            var hComparer = new CompareHorizontalLines();
            var vComparer = new CompareVerticalLines();

            List<HorizontalCutLine> horizontalResultList = horizontalLines.Except(notValidHorizontalLines, hComparer).ToList();
            List<VerticalCutLine> verticalResultList = varticalLines.Except(notValidVerticalLines, vComparer).ToList();

            //List<ICutLine> result = new List<ICutLine>();
            //result = horizontalResultList;
            //resultList = resultList.Distinct(new CompareHorizontalLines()).ToList();

            return resultList.ToArray();
            
            //TODO nie wiem czy dziala distinct poprawnie
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