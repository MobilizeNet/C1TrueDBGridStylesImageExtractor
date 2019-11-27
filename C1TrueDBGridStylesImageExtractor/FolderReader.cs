using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class FolderReader
    {

        private int totalImages = 0;
        public int TotalImages
        {
            get { return totalImages; }
            set { totalImages = value; }
        }

        private int totalFrx = 0;
        public int TotalFrx
        {
            get { return totalFrx; }
            set { totalFrx = value; }
        }

        // The amount of grids found
        private int totalImageBatches = 0;
        public int TotalImageBatches
        {
            get { return totalImageBatches; }
            set { totalImageBatches = value; }
        }

        private int totalGridWithImages = 0;
        public int TotalGridWithImages
        {
            get { return totalGridWithImages; }
            set { totalGridWithImages = value; }
        }

        private long elapsedTime;

        public long ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        public List<string> GetFrxFiles(string folderPath)
        {
            List<string> files = new List<string>();
            GetFrxFiles(files, folderPath);
            return files;
        }

        private void GetFrxFiles(List<string> files, string dir)
        {
            try
            {
                files.AddRange(Directory.GetFiles(dir).Where(file => file.ToUpper().EndsWith(".FRX")));
                foreach (string d in Directory.GetDirectories(dir))
                {
                    GetFrxFiles(files, d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void ProcessFrxFiles(string folderPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> frxFiles = GetFrxFiles(folderPath);
            TotalFrx = frxFiles.Count;
            if (TotalFrx == 0)
            {
                throw new ArgumentException("No files found to process");
            }
            foreach (string frx in frxFiles)
            {
                Console.WriteLine($"Processing {frx}");
                FrxReader frxReader = new FrxReader();
                frxReader.FrxFilePath = frx;
                frxReader.ReadBytesToArray();
                frxReader.ProcessFrx();
                totalImages += frxReader.TotalImages;
                frxReader.SaveImages();
                frxReader.SaveStyleInfo();
                totalImageBatches += frxReader.TotalImageBatches;
                totalGridWithImages += frxReader.TotalGridWithImages;
            }
            stopwatch.Stop();
            ElapsedTime = stopwatch.ElapsedMilliseconds;
        }

        public void SaveLog(string folderPath)
        {
            String timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string filePath = folderPath + @"\" + $"C1TrueDBGridStylesImageExtractor_{timestamp}.log";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                WriteLog(writer);
            }
        }
        public void WriteLog(TextWriter textWriter)
        {
            textWriter.WriteLine($"Elapsed time: {ElapsedTime}");
            textWriter.WriteLine($".FRX files found: {totalFrx}");
            textWriter.WriteLine($"Image batches/Grids found: {TotalImageBatches}");
            textWriter.WriteLine($"Grids with images: {TotalGridWithImages}");
            textWriter.WriteLine($"Images found: {TotalImages}");
        }
    }
}
