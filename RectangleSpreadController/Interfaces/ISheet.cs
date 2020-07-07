using System.Drawing;

namespace RectangleSpreadController
{
    public interface ISheet
    {
        Point Location { get;  }
        Size Size { get;  }
    }
}