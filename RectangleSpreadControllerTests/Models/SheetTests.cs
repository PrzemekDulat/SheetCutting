using Microsoft.VisualStudio.TestTools.UnitTesting;
using RectangleSpreadController;
using System;
using System.Collections.Generic;
using System.Text;

namespace RectangleSpreadController.Tests
{
    [TestClass()]
    public class SheetTests
    {
        [TestMethod()]
        public void RemoveLinesTest()
        {

            List<ICutLine> ResultList = new List<ICutLine>() { new VerticalCutLine(4), new VerticalCutLine(5) };
            List<ICutLine> NotValidLines = new List<ICutLine>() { new VerticalCutLine(4) };
           var result = Sheet.RemoveLines(ResultList, NotValidLines);
            Assert.IsTrue(result.Count == 1);
        }
    }
}