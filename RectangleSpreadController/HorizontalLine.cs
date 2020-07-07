using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
   public class HorizontalCutLine : ICutLine
    {
        public double YValue { get; }

        public LineType LineType => LineType.Horizontal;

        public HorizontalCutLine(double yValue)
        {
            YValue = yValue;
        }
    }
}
