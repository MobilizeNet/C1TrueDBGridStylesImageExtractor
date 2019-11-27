using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C1TrueDBGridStylesImageExtractor;
using System.Collections.Generic;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractorTest
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void ByteArrayToIntTest()
        {
            // Arrange
            Byte[] array = { 0x24, 0x01 };
            int expectedResult = 292;
            // Act
            int actualResult = Utilities.ByteArrayToInt(array);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ByteArrayToStringTest()
        {
            // Arrange
            Byte[] array = { 0x47, 0x72, 0x69, 0x64, 0x00, 0x00, 0x00 };
            string expectedResult = "Grid";
            // Act
            string actualResult = Utilities.ByteArrayToString(array);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void PatternAtTest()
        {
            // Arrange
            Byte[] array = { 0x42, 0x4d, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00 };
            Byte[] pattern = { 0x00, 0x36, 0x00 };
            int expectedResult = 9;
            // Act
            int actualResult = Utilities.PatternAt(array, pattern).First();
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FirstPositionAfterPatternTest()
        {
            // Arrange
            Byte[] array = { 0x42, 0x4d, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00 };
            Byte[] pattern = { 0x32, 0x01, 0x00 };
            int expectedResult = 5;
            // Act
            int actualResult = Utilities.FirstPositionAfterPattern(array, pattern, 0);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetCreateListElementListTest()
        {
            //Arrange
            int expectedResult = 3;
            List<Split> list = new List<Split>();
            //Act
            Utilities.GetCreateListElement<Split>(list, 2);
            int actualResult = list.Count;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GenerateSummarizedStyleTest1()
        {
            //Arrange
            Style style1 = new Style();
            style1.BackgroundPictureIndex = 2;
            style1.FontName = "Arial";
            Style[] allStyles = { style1 };
            StyleAssignment styleAssign = new StyleAssignment();
            styleAssign.StyleIndex = 0;
            styleAssign.StyleName = "HeadingStyle";
            string expectedResult = "2";
            //Act
            XElement element = Utilities.GenerateSummarizedStyle(allStyles, new List<NamedStyle>(), styleAssign);
            string actualResult = element.Attribute("BackgroundPictureIndex").Value;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GenerateSummarizedStyleTest2()
        {
            //Arrange
            Style style1 = new Style();
            style1.FontName = "Arial";
            Style[] allStyles = { style1 };
            StyleAssignment styleAssign = new StyleAssignment();
            styleAssign.StyleIndex = 0;
            styleAssign.StyleName = "HeadingStyle";
            string expectedResult = "Arial";
            //Act
            XElement element = Utilities.GenerateSummarizedStyle(allStyles, new List<NamedStyle>(), styleAssign);
            string actualResult = element.Attribute("FontName").Value;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GenerateSummarizedStyleTest3()
        {
            //Arrange
            Style style1 = new Style();
            style1.FontName = "";
            Style style2 = new Style();
            style2.FontName = "MS Serif";
            Style[] allStyles = { style1, style2 };
            StyleAssignment styleAssign = new StyleAssignment();
            styleAssign.StyleIndex = 0;
            styleAssign.StyleName = "HeadingStyle";
            styleAssign.NamedStyleIndex = 1;
            NamedStyle namedStyle = new NamedStyle();
            namedStyle.Style = style2;
            string expectedResult = "MS Serif";
            List<NamedStyle> namedStyles = new List<NamedStyle>();
            namedStyles.Add(namedStyle);
            //Act
            XElement element = Utilities.GenerateSummarizedStyle(allStyles, namedStyles, styleAssign);
            string actualResult = element.Attribute("FontName").Value;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GenerateSummarizedStyleTest4()
        {
            //Arrange
            Style style1 = new Style();
            style1.FontName = "Arial";
            Style style2 = new Style();
            style2.FontName = "MS Serif";
            Style[] allStyles = { style1, style2 };
            StyleAssignment styleAssign = new StyleAssignment();
            styleAssign.StyleIndex = 0;
            styleAssign.StyleName = "HeadingStyle";
            styleAssign.NamedStyleIndex = 1;
            NamedStyle namedStyle = new NamedStyle();
            namedStyle.Style = style2;
            string expectedResult = "Arial";
            List<NamedStyle> namedStyles = new List<NamedStyle>();
            namedStyles.Add(namedStyle);
            //Act
            XElement element = Utilities.GenerateSummarizedStyle(allStyles, namedStyles, styleAssign);
            string actualResult = element.Attribute("FontName").Value;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
