using System;
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

        public ICutLine[] GetCutLines(OrderRelatedElement[] orderRelatedElements)
        {
            //TODO create all cutlines from trectangles but filter out lines with that goes throu location or location plus size 
            throw new NotImplementedException();

        }

        public ICutLine GetFirstValidCutLine(ICutLine[] cutLines)
        {
            return cutLines.FirstOrDefault(CheckIfLineIsValidCutLine) ?? throw new ArgumentException($"In argument {nameof(cutLines)} there is no valid cutline");
        }

        private static bool CheckIfLineIsValidCutLine(ICutLine cutLine)
        {
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