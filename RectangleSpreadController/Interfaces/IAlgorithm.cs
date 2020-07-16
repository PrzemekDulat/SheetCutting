using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController.Interfaces
{
    public interface IAlgorithm
    {
        List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains);
    }
}
