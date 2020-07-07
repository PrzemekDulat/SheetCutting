using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class Waste : ISheet
    {
        public Point Location { get; }
        public Size Size { get;  }
        public Sheet Sheet { get; set; }

        public Waste(Sheet sheet, Point location, Size size)
        {
            Sheet = sheet;
            Location = location;
            Size = size;
        }
    }
}
