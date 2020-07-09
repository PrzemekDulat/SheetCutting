using System;

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
    }
}