using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class OrderRelatedElement : ISheet
    {
        public Point Location { get; set; }

        public Size Size { get; set; }

        public string OrderLine { get; }

        public OrderRelatedElement(Point location, Size size, string orderLine)
        {
            Location = location;
            Size = size;
            OrderLine = orderLine;
        }
    }
}

