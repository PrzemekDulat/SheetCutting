using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareHorizontalLines : IEqualityComparer<HorizontalCutLine>
    {
        public bool Equals(HorizontalCutLine x, HorizontalCutLine y)
        {
            return x.YValue == y.YValue;
        }

        public int GetHashCode(HorizontalCutLine obj)
        {
            return obj.YValue.GetHashCode();
        }
    }
}
