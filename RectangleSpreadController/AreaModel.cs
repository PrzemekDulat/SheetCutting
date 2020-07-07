using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class AreaModel
    {
        public Size Size { get; set; }
        public Point Location { get; set; }
        public List<OrderRelatedElement> Rectangles { get; set; } = new List<OrderRelatedElement>();
        public List<Waste> Waste { get; set; } = new List<Waste>();
    }
}
