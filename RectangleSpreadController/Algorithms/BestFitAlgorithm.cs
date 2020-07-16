using RectangleSpreadController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RectangleSpreadController.Algorithms
{
    public class BestFitAlgorithm : IAlgorithm
    {
        //public List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains)
        public List<List<OrderRelatedElement>> ExecuteAlgorithm(Sheet sheet, AlgorithmConstrains constrains)
        {
            List<OrderRelatedElement> rectangles = sheet.OrderRelatedElements.ToList();
            List<OrderRelatedElement> fittedRectangles = new List<OrderRelatedElement>();
            List<List<OrderRelatedElement>> resultRectangles = new List<List<OrderRelatedElement>>();

            //iterate through whole rectangle list to find best fitting rectangles for height of the sheet
            foreach (var rectangle in rectangles)
            {

            }


           
            //another row (by the width of the najszerszego rectangla)

            return resultRectangles;
        }
        public void test()
        {
            List<int> set = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = ProduceWithoutRecursion(set);
            //These best rectangles that fit remove from original list and place them on sheet, then move to 
            foreach (var item in result)
            {
                //item.Sum();
                //foreach (var i in item)
                //{
                //    i.ToString();
                //}
            }
        }

        public List<int> ProduceWithoutRecursion(List<int> allValues)
        {
            var collection = new List<int>();
            for (int counter = 0; counter < (1 << allValues.Count); ++counter)
            {
                List<int> combination = new List<int>();
                for (int i = 0; i < allValues.Count; ++i)
                {
                    if ((counter & (1 << i)) == 0)
                        combination.Add(allValues[i]);
                }
                ;
                // do something with combination
                collection.Add(combination.Sum());
            }
            return collection;
        }
    }
}
