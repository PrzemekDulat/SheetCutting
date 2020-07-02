using Microsoft.VisualStudio.TestTools.UnitTesting;
using RectangleSpreadController;
using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController.Tests
{
    [TestClass()]
    public class RectangleSorterTests
    {
        [TestMethod()]
        public void CreateRandomListOfRectanglesTest()
        {
            RectangleSorter rectangleSorter = new RectangleSorter();
            int[] values = new int[] { 0, 1, 2, 3, 4, 5 };
            foreach (var value in values)
            {
            var result = rectangleSorter.CreateRandomListOfRectangles(value);
            Assert.IsTrue(result.Count == value);
            }

        }
        [TestMethod()]
        public void CreateRandomListOfRectanglesTest_negativNumber()
        {
            RectangleSorter rectangleSorter = new RectangleSorter();
            int[] values = new int[] {-5,-2 };
            foreach (var value in values)
            {
                try
                {
                var result = rectangleSorter.CreateRandomListOfRectangles(value);
                Assert.Fail();

                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is ArgumentException);
                    
                }
            }

        }
    }
}