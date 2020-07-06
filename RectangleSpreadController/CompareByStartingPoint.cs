using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class CompareByStartingPoint : IEqualityComparer<LineModel>
    {
        public bool Equals(LineModel x, LineModel y)
        {
            return x.StartLocation == y.StartLocation;
        }

        public int GetHashCode(LineModel obj)
        {
            return  obj.StartLocation.GetHashCode();
        }
    }
}
