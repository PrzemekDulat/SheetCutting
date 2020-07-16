namespace RectangleSpreadController
{
    public interface ICutLine
    {
        LineType LineType { get; }

        int Value { get; }

        bool IsPartOfRectangle(OrderRelatedElement rectangle, Sheet sheet);
        int CountRectanglesWhereLineIsPartOf(OrderRelatedElement[] rectangle, Sheet sheet);
        int Lenght(OrderRelatedElement[] rectangle, Sheet sheet);
    }
}