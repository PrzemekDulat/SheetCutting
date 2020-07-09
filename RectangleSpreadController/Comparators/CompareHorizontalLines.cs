using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareHorizontalLines : IEqualityComparer<ICutLine>
    {
        public bool Equals(HorizontalCutLine x, HorizontalCutLine y)
        {
            return x.YValue == y.YValue;
        }

        public bool Equals(ICutLine x, ICutLine y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(HorizontalCutLine obj)
        {
            return obj.YValue.GetHashCode();
        }

        public int GetHashCode(ICutLine obj)
        {
            throw new NotImplementedException();
        }
    }
}
