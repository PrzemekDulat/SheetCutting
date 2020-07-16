using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController
{
    public class AlgorithmConstrains
    {

        public int CutLineLimit { get; }

        public bool IsAbleToRotate { get; }

        private AlgorithmConstrains(bool isAbleToRotate, int cutLineLimit)
        {
            CutLineLimit = cutLineLimit;
            IsAbleToRotate = isAbleToRotate;
        }

        public static AlgorithmConstrains Create(bool isAbleToRotate, int cutLineLimit)
        {
            return new AlgorithmConstrains(isAbleToRotate, cutLineLimit);
        }

    }
}
