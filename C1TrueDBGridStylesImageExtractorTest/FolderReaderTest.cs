using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C1TrueDBGridStylesImageExtractor;
using System.IO;

namespace C1TrueDBGridStylesImageExtractorTest
{
    [TestClass]
    public class FolderReaderTest
    {
        [TestMethod]
        public void TotalImagesTestDefaultValue()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            int expectedResult = 0;
            //Act
            int actualResult = folderReader.TotalImages;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalImagesTestSetAndGet()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.TotalImages = 4;
            int expectedResult = 4;
            //Act
            int actualResult = folderReader.TotalImages;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalFormsTestDefaultValue()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            int expectedResult = 0;
            //Act
            int actualResult = folderReader.TotalFrx;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalFormsTestSetAndGet()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.TotalFrx = 6;
            int expectedResult = 6;
            //Act
            int actualResult = folderReader.TotalFrx;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalImageBatchesTestDefaultValue()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            int expectedResult = 0;
            //Act
            int actualResult = folderReader.TotalImageBatches;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalImageBatchesTestSetAndGet()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.TotalImageBatches = 3;
            int expectedResult = 3;
            //Act
            int actualResult = folderReader.TotalImageBatches;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalGridWithImagesTestDefaultValue()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            int expectedResult = 0;
            //Act
            int actualResult = folderReader.TotalGridWithImages;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalGridWithImagesTestSetAndGet()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.TotalGridWithImages = 3;
            int expectedResult = 3;
            //Act
            int actualResult = folderReader.TotalGridWithImages;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ElapsedTimeTestSetAndGet()
        {
            //Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.ElapsedTime = 23451;
            long expectedResult = 23451;
            //Act
            long actualResult = folderReader.ElapsedTime;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void WriteLogTest()
        {
            // Arrange
            FolderReader folderReader = new FolderReader();
            folderReader.ElapsedTime = 12042;
            folderReader.TotalFrx = 3;
            folderReader.TotalImageBatches = 7;
            folderReader.TotalGridWithImages = 5;
            folderReader.TotalImages = 13;
            StringWriter strWr = new StringWriter();
            string expectedResult = "Elapsed time: 12042\r\n" +
            ".FRX files found: 3\r\n" +
            "Image batches/Grids found: 7\r\n" +
            "Grids with images: 5\r\n" +
            "Images found: 13\r\n";
            // Act
            folderReader.WriteLog(strWr);
            string actualResult = strWr.ToString();
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
