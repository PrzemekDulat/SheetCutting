using System;
using System.Linq;

namespace RectangleSpreadController
{
    public class VerticalCutLine : ICutLine, IEquatable<VerticalCutLine>
    {
        public int Value { get; }

        public LineType LineType => LineType.Vertical;

        public VerticalCutLine(int Value)
        {
            this.Value = Value;
        }

        public bool Equals(VerticalCutLine other)
        {
            return Value.Equals(other.Value);
        }
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case VerticalCutLine v:
                    return Equals(v);
            }
            return base.Equals(obj);
        }

        public bool IsPartOfRectangle(OrderRelatedElement rectangle, Sheet sheet)
        {

            return Value == rectangle.Location.X - sheet.Location.X || Value == rectangle.Location.X + rectangle.Size.Width - sheet.Location.X;
            
        }

        public int CountRectanglesWhereLineIsPartOf(OrderRelatedElement[] rectangles, Sheet sheet)
        {
            return rectangles.Where(x => IsPartOfRectangle(x,sheet)).Count();
        }

        public int Lenght(OrderRelatedElement[] rectangles, Sheet sheet)
        {
            int oneSideLength = 0;
            int secondSideLength = 0;

            foreach (var rectangle in rectangles)
            {
                if (Value == rectangle.Location.X - sheet.Location.X)
                {
                    oneSideLength += rectangle.Size.Height;
                }
                else if (Value == rectangle.Location.X + rectangle.Size.Width - sheet.Location.X)
                {
                    secondSideLength += rectangle.Size.Height;
                }
            }

            return oneSideLength > secondSideLength ? oneSideLength : secondSideLength;
        }
    }
}