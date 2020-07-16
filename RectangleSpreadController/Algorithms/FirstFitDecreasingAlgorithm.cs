using RectangleSpreadController.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RectangleSpreadController.Algorithms
{
    public class FirstFitDecreasingAlgorithm : IAlgorithm
    {
        public List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains)
        {
            List<List<OrderRelatedElement>> resultRectangles = new List<List<OrderRelatedElement>>();
            List<OrderRelatedElement> fittingRectangles = new List<OrderRelatedElement>();
            List<OrderRelatedElement> rectangles = new List<OrderRelatedElement>();
            rectangles = sheet.OrderRelatedElements.ToList();

            int sheetHeight = sheet.Size.Height;
            int sheetWidth = sheet.Size.Width;

            List<OrderRelatedElement> firstsInColumns = new List<OrderRelatedElement>();
            int heightOffset = 0;
            int widthOffset = 0;
            

            firstsInColumns.Add(rectangles.First());


            foreach (OrderRelatedElement rectangle in rectangles)
            {
                if (rectangle.Size.Height <= CalculateRemainingHeight()) //fits by height
                {
                    rectangle.Location = new Point(widthOffset, heightOffset);
                    fittingRectangles.Add(rectangle);
                    heightOffset += rectangle.Size.Height;
                }
                else if (rectangle.Size.Width <= CalculateRemainingWidth())  //fits by width
                {
                    firstsInColumns.Add(rectangle);
                    heightOffset = 0;
                    rectangle.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangle.Size.Height;
                    fittingRectangles.Add(rectangle);
                }
                else// jesli sie nie miesci na sheecie/cutlimicie to wsyzstkie kolejne prostokaty ida na nastepnego sheeta
                {

                    firstsInColumns.Clear();
                    firstsInColumns.Add(rectangle);
                    resultRectangles.Add(fittingRectangles);
                    fittingRectangles = new List<OrderRelatedElement>() { rectangle };
                    heightOffset = 0;
                    widthOffset = 0;
                    rectangle.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangle.Size.Height;

                }

            }

            resultRectangles.Add(fittingRectangles);


            return resultRectangles;

            int CalculateRemainingHeight()
            {
                return sheetHeight - heightOffset;
            }

            int CalculateRemainingWidth()
            {
                widthOffset = 0;
                foreach (var bRectangle in firstsInColumns)
                {
                    widthOffset += bRectangle.Size.Width;
                }

                return sheetWidth - widthOffset;
            }
        }
    }
}
