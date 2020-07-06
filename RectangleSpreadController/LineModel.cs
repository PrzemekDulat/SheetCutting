using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class LineModel 
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }

        private LineModel(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }
        public static LineModel CreateLine(Point startLocation, Point endLocation) => new LineModel(startLocation, endLocation);


    }
}
