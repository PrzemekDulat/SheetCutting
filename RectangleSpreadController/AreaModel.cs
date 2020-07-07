﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController
{
    public class AreaModel
    {
        public Size Size { get; set; }
        public Point Location { get; set; }
        public List<RectangleModel> Rectangles { get; set; } = new List<RectangleModel>();
        public List<WasteModel> Waste { get; set; } = new List<WasteModel>();
    }
}
