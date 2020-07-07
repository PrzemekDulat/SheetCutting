using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class LineM : ICutLine
    {
        public int Location { get; set; }

        public LineType LineType { get; set; }

        private LineM(LineType lineType, int location)
        {
            LineType = lineType;
            Location = location;
        }
        public static LineM CreateLine(LineType lineType, int location) => new LineM(lineType, location);

    }
}

