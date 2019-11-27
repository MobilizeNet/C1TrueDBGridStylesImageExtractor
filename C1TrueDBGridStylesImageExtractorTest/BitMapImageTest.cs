﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C1TrueDBGridStylesImageExtractor;

namespace C1TrueDBGridStylesImageExtractorTest
{
    [TestClass]
    public class BitMapImageTest
    {
        [TestMethod]
        public void CreateFileHeaderTest()
        {
            // Arrange
            Image bmp = new BitMapImage();
            bmp.DataSize = 292;
            bmp.Data = new Byte[]{ 0x28, 0x00, 0x00, 0x00, 0x0c, 0x00, 0x00, 0x00 }; // ... don't write the whole file
            byte[] expectedResult = { 0x42, 0x4d, 0x32, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00 };
            // Act
            byte[] actualResult = bmp.CreateFileHeader();
            // Assert
            bool areEqual = expectedResult.SequenceEqual(actualResult);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ImageDataStartAddressTest()
        {
            // Arrange
            BitMapImage bmp = new BitMapImage();
            bmp.DataSize = 292;
            bmp.Data = new Byte[] { 0x28, 0x00, 0x00, 0x00, 0x0c, 0x00, 0x00, 0x00 }; // ... don't write the whole file
            byte[] expectedResult = { 0x36, 0x00, 0x00, 0x00 };
            // Act
            byte[] actualResult = bmp.ImageDataStartAddress();
            // Assert
            bool areEqual = expectedResult.SequenceEqual(actualResult);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void FileDataTest()
        {
            // Arrange
            BitMapImage bmp = new BitMapImage();
            bmp.DataSize = 56;
            bmp.Data = new Byte[] {   0x28, 0x00,
                0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x18, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC8, 0xD0,
                0xD4, 0xC8, 0xD0, 0xD4, 0x00, 0x00 };
            byte[] expectedResult = {
                0x42, 0x4D, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00, 0x28, 0x00,
                0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x18, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC8, 0xD0,
                0xD4, 0xC8, 0xD0, 0xD4, 0x00, 0x00, };
            // Act
            byte[] actualResult = bmp.FileData();
            // Assert
            bool areEqual = expectedResult.SequenceEqual(actualResult);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ExtensionTest()
        {
            // Arrange
            BitMapImage bmp = new BitMapImage();
            string expectedResult = "bmp";
            // Act
            string actualResult = bmp.Extension();
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
