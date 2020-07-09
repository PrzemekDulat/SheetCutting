using Microsoft.VisualStudio.TestTools.UnitTesting;
using RectangleSpreadController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RectangleSpreadController.Tests
{
    [TestClass()]
    public class SheetTests
    {
        public OrderRelatedElement[] SampleDataSet()
        {
            OrderRelatedElement[] orderRelatedElements = new OrderRelatedElement[4];

            orderRelatedElements[0] = new OrderRelatedElement(new Point(0, 0), new Size(200, 300), "No.1");
            orderRelatedElements[1] = new OrderRelatedElement(new Point(0, 300), new Size(200, 300), "No.2");
            orderRelatedElements[2] = new OrderRelatedElement(new Point(600, 500), new Size(200, 300), "No.3");
            orderRelatedElements[3] = new OrderRelatedElement(new Point(800, 1200), new Size(200, 300), "No.4");

            return orderRelatedElements;
        }

        public Sheet SampleSheet(OrderRelatedElement[] orderRelatedElements)
        {
            return Sheet.CreateNewSheet(orderRelatedElements, default, new Size(1000, 1500));
        }

        [TestMethod()]
        public void RemoveLinesTest()
        {
            List<ICutLine> ResultList = new List<ICutLine>() { new VerticalCutLine(4), new VerticalCutLine(5) };
            List<ICutLine> NotValidLines = new List<ICutLine>() { new VerticalCutLine(4) };
            var result = Sheet.RemoveLines(ResultList, NotValidLines);
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void TestAllLines()
        {
            //sheet.GetFirstValidCutLine(
            Sheet sheet = SampleSheet(SampleDataSet());
            var cutlines = sheet.GetCutLines(SampleSheet(SampleDataSet()));
            Assert.IsTrue(cutlines.Count() == 8);
        }

        [TestMethod()]
        public void TestValidLine()
        {
            Sheet sheet = SampleSheet(SampleDataSet());
            var validLines = new List<ICutLine>();
            var cutLines = sheet.GetCutLines(SampleSheet(SampleDataSet()));

            foreach (var cutLine in cutLines)
            {
                ICutLine[] oneLine = new ICutLine[1];
                oneLine[0] = cutLine;
                validLines.Add(sheet.GetFirstValidCutLine(oneLine, sheet));
            }

            Assert.IsTrue(validLines.Count == 5);
        }
    }
}