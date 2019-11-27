using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C1TrueDBGridStylesImageExtractor;

namespace C1TrueDBGridStylesImageExtractorTest
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void IndexTestSetAndGet()
        {
            //Arrange
            Image img = new BitMapImage();
            img.Index = 3;
            int expectedResult = 3;
            //Act
            int actualResult = img.Index;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DataTestSetAndGet()
        {
            //Arrange
            Image img = new BitMapImage();
            img.Data = new Byte[]{ 0x01, 0x02, 0x03, 0x04, 0x05 };
            byte[] expectedResult = { 0x01, 0x02, 0x03, 0x04, 0x05 };
            //Act
            byte[] actualResult = img.Data;
            // Assert
            bool areEqual = expectedResult.SequenceEqual(actualResult);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DataSizeSetAndGet()
        {
            //Arrange
            Image img = new BitMapImage();
            img.DataSize = 230;
            int expectedResult = 230;
            //Act
            int actualResult = img.DataSize;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
