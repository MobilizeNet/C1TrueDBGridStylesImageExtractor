using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C1TrueDBGridStylesImageExtractor;

namespace C1TrueDBGridStylesImageExtractorTest
{
    [TestClass]
    public class StyleAssignmentTest
    {
        [TestMethod]
        public void StyleIndexTestSetAndGet()
        {
            //Arrange
            StyleAssignment styleAssignment = new StyleAssignment();
            styleAssignment.StyleIndex = 2;
            int expectedResult = 2;
            //Act
            int actualResult = styleAssignment.StyleIndex;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void NamedStyleIndexTestSetAndGet()
        {
            //Arrange
            StyleAssignment styleAssignment = new StyleAssignment();
            styleAssignment.NamedStyleIndex = 4;
            int expectedResult = 4;
            //Act
            int actualResult = styleAssignment.NamedStyleIndex;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SplitIndexTestSetAndGet()
        {
            //Arrange
            StyleAssignment styleAssignment = new StyleAssignment();
            styleAssignment.SplitIndex = 3;
            int expectedResult = 3;
            //Act
            int actualResult = styleAssignment.SplitIndex;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DisplayColumnIndexTestSetAndGet()
        {
            //Arrange
            StyleAssignment styleAssignment = new StyleAssignment();
            styleAssignment.DisplayColumnIndex = 1;
            int expectedResult = 1;
            //Act
            int actualResult = styleAssignment.DisplayColumnIndex;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void StyleNameTestSetAndGet()
        {
            //Arrange
            StyleAssignment styleAssignment = new StyleAssignment();
            styleAssignment.StyleName = "HeadingStyle";
            string expectedResult = "HeadingStyle";
            //Act
            string actualResult = styleAssignment.StyleName;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
