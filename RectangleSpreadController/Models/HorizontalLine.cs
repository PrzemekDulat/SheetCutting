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
    }
}
