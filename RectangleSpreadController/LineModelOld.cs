using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class LineModelOld 
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        private LineModelOld(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }
        public static LineModelOld CreateLine(Point startLocation, Point endLocation) => new LineModelOld(startLocation, endLocation);


    }
}
