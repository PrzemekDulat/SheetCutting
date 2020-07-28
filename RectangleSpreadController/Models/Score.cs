using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RectangleSpreadController.Models
{
    public class Score
    {
        public int NumberOfSheets { get; set; }
        public int NumberOfCuts { get; set; }
        public int NumberOfWaste { get; set; }
        public List<int> WasteRatio { get; set; } = new List<int>();
    }
}
