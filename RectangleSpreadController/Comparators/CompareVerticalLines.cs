using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareVerticalLines : IEqualityComparer<VerticalCutLine>
    {
        public bool Equals(VerticalCutLine x, VerticalCutLine y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(VerticalCutLine obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
