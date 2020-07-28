using RectangleSpreadController.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RectangleSpreadController.Algorithms
{
    public class BestFitAlgorithm : IAlgorithm
    {
        //public List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains)

        public List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains)
        {
            List<OrderRelatedElement> rectangles = sheet.OrderRelatedElements.ToList();
            List<List<OrderRelatedElement>> resultRectangles = new List<List<OrderRelatedElement>>();

            List<OrderRelatedElement> testList = new List<OrderRelatedElement>();

            //iterate through whole rectangle list to find best fitting rectangles for height of the sheet
            testList.AddRange(StartAlgotithm(sheet, constrains, rectangles));
            resultRectangles.Add(testList);
            return resultRectangles;
        }

        List<OrderRelatedElement> allArrangedRectangles = new List<OrderRelatedElement>();
        private List<OrderRelatedElement> StartAlgotithm(Sheet sheet, AlgorithmConstrains constrains, List<OrderRelatedElement> rectangles)
        {
            List<List<OrderRelatedElement>> result = ProduceWithoutRecursion(rectangles); //list of list of rectangles height
            List<OrderRelatedElement> bestRectangles = DetermineBestList(result, constrains.CutLineLimit, sheet.Size.Height); //rectangles that have best fit
            List<OrderRelatedElement> arrangedRectnagles = ArrangeRectangles(bestRectangles, sheet.Size.Width); // arranged rectangles
            allArrangedRectangles.AddRange(arrangedRectnagles);

            rectangles = rectangles.Except(bestRectangles).ToList();
            if (rectangles.Count > 0)
            {
                StartAlgotithm(sheet, constrains, rectangles);
            }
            return allArrangedRectangles;
        }
        bool firstTime = true;
        int counter = default;
        int widthOffset = default;
        int heightOffset = default;
        List<int> heightList = new List<int>() { 0 };
        private List<OrderRelatedElement> ArrangeRectangles(List<OrderRelatedElement> rectangles, int sheetWidth)
        {
            List<OrderRelatedElement> arrangedRectangles = new List<OrderRelatedElement>();

            heightOffset = heightList[counter]; // 0

            if (widthOffset + DetermineWidthOffset(rectangles) <= sheetWidth) //fits by width
            {
                foreach (var rectangle in rectangles)
                {
                    rectangle.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangle.Size.Height;
                    arrangedRectangles.Add(rectangle);
                }

                widthOffset += DetermineWidthOffset(rectangles);
                if (firstTime)
                {
                    heightList.Add(heightOffset);
                    firstTime = false;
                }
            }
            else // doesnt fit by width ==> width = 0, heightoffset = heightlist[0]
            {
                firstTime = true;
                counter++;
                widthOffset = 0;
                heightOffset = heightList[counter];

                foreach (var rectangle in rectangles)
                {
                    rectangle.Location = new Point(widthOffset, heightOffset);
                    heightOffset += rectangle.Size.Height;
                    arrangedRectangles.Add(rectangle);
                }
                widthOffset += DetermineWidthOffset(rectangles);

            }

            return arrangedRectangles;
        }

        private int DetermineWidthOffset(List<OrderRelatedElement> bestRectangles)
        {
            int xOffset = default;
            foreach (var rectangle in bestRectangles)
            {
                if (rectangle.Size.Width > xOffset)
                {
                    xOffset = rectangle.Size.Width;
                }
            }
            return xOffset;
        }

        private List<OrderRelatedElement> DetermineBestList(List<List<OrderRelatedElement>> listOfList, int desiredNumber, int sheetHeight)
        {
            List<OrderRelatedElement> resultList = new List<OrderRelatedElement>();
            Dictionary<List<OrderRelatedElement>, int> listOfRecntaglesAndHeightSum = new Dictionary<List<OrderRelatedElement>, int>();


            foreach (var rectangles in listOfList)
            {
                int sumHeight = 0;
                foreach (var rectangle in rectangles)
                {
                    sumHeight += rectangle.Size.Height;
                }

                listOfRecntaglesAndHeightSum.Add(rectangles, sumHeight);
            }

            int lastBiggestNumber = 0;
            int indexOfBiggestNumber = default;

            for (int i = 0; i < listOfRecntaglesAndHeightSum.Count; i++)
            {
                int number = listOfRecntaglesAndHeightSum.Values.ElementAt(i);
                if ((counter) * desiredNumber <= sheetHeight)
                {
                    if (number <= desiredNumber && number > lastBiggestNumber)
                    {
                        lastBiggestNumber = number;
                        indexOfBiggestNumber = i;
                        resultList = listOfRecntaglesAndHeightSum.Keys.ElementAt(i);
                    }

                }
                else
                {
                    if (number <= ((counter * desiredNumber) - sheetHeight) && number > lastBiggestNumber)
                    {
                        lastBiggestNumber = number;
                        indexOfBiggestNumber = i;
                        resultList = listOfRecntaglesAndHeightSum.Keys.ElementAt(i);
                    }
                }
            }

            return resultList;
        }

        public List<List<OrderRelatedElement>> ProduceWithoutRecursion(List<OrderRelatedElement> allValues)
        {
            var collection = new List<List<OrderRelatedElement>>();
            for (int counter = 0; counter < (1 << allValues.Count); ++counter)
            {
                List<OrderRelatedElement> combination = new List<OrderRelatedElement>();
                for (int i = 0; i < allValues.Count; ++i)
                {
                    if ((counter & (1 << i)) == 0)
                        combination.Add(allValues[i]);
                }
                
                // do something with combination
                collection.Add(combination);
            }
            return collection;
        }
    }
}
