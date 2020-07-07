namespace RectangleSpreadController
{
    public class VerticalCutLine : ICutLine
    {
        public double XValue { get; }

        public LineType LineType => LineType.Vertical;

        public VerticalCutLine(double xValue)
        {
            XValue = xValue;
        }
    }
}