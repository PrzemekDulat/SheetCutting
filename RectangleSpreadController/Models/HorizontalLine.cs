using System.Linq;

namespace RectangleSpreadController
{
    public class HorizontalCutLine : ICutLine 
    {
        public int Value { get; }

        public LineType LineType => LineType.Horizontal;

        public HorizontalCutLine(int Value)
        {
            this.Value = Value;
        }

        public bool Equals(HorizontalCutLine other)
        {
            return Value.Equals(other.Value);
        }
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case HorizontalCutLine h:
                    return Equals(h);
            }
            return base.Equals(obj);
        }

        public bool IsPartOfRectangle(OrderRelatedElement rectangle, Sheet sheet)
        {
            return Value == rectangle.Location.Y - sheet.Location.Y || Value == rectangle.Location.Y + rectangle.Size.Height - sheet.Location.Y;
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
                if (Value == rectangle.Location.Y - sheet.Location.Y)
                {
                    oneSideLength += rectangle.Size.Width;
                }
                else if (Value == rectangle.Location.Y + rectangle.Size.Height - sheet.Location.Y)
                {
                    secondSideLength += rectangle.Size.Width;
                }
            }

            return oneSideLength > secondSideLength ? oneSideLength : secondSideLength;

        }
    }
}
