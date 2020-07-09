using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareHorizontalLines : IEqualityComparer<HorizontalCutLine>
    {
        public bool Equals(HorizontalCutLine x, HorizontalCutLine y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(HorizontalCutLine obj)
        {
            return obj.Value.GetHashCode();
        }

    }
}
