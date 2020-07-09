using System;

namespace RectangleSpreadController
{
    public class VerticalCutLine : ICutLine, IEquatable<VerticalCutLine>
    {
        public double XValue { get; }

        public LineType LineType => LineType.Vertical;

        public VerticalCutLine(double xValue)
        {
            XValue = xValue;
        }

        public bool Equals(VerticalCutLine other)
        {
            return XValue.Equals(other.XValue);
        }
    }
}