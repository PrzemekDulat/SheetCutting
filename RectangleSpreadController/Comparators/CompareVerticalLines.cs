using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareVerticalLines : IEqualityComparer<VerticalCutLine>
    {
        public bool Equals(VerticalCutLine x, VerticalCutLine y)
        {
            return x.XValue == y.XValue;
        }

        public int GetHashCode(VerticalCutLine obj)
        {
            return obj.XValue.GetHashCode();
        }
    }
}
