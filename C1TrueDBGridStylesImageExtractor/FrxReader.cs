using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractor
{
    public class FrxReader
    {
        private int readerIndex = 0;
        public int ReaderIndex
        {
            get { return readerIndex; }
            set { readerIndex = value; }
        }

        private List<TrueDBGrid> grids = new List<TrueDBGrid>();
        public List<TrueDBGrid> Grids
        {
            get { return grids; }
            set { grids = value; }
        }

        private string frxFilePath;
        public string FrxFilePath
        {
            get { return frxFilePath; }
            set { frxFilePath = value; }
        }

        private byte[] frxFile;
        public byte[] FrxFile
        {
            get { return frxFile; }
            set { frxFile = value; }
        }

        private int totalImages = 0;
        public int TotalImages
        {
            get { return totalImages; }
            set { totalImages = value; }
        }

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

        public void ProcessFrx()
        {
            try
            {
                while (readerIndex < FrxFile.Length)
                {
                    MoveIndexNextStartStylesImage();
                    TrueDBGrid grid = new TrueDBGrid();
                    totalImageBatches++;
                    grid.Images = ProcessImagesBatch();
                    grid.AllStyles = ProcessStylesBatch();
                    ProcessStyleAssignmentsBatch(grid);
                    ReadNamedStyleInfoBatch(grid);
                    if (grid.Images.Count > 0)
                    {
                        totalGridWithImages++;
                    }
                    MoveIndexNextStartGridName();
                    grid.Name = ProcessGridName();
                    Grids.Add(grid);
                    Console.WriteLine($"This images are from the grid {grid.Name}");
                    
                }
            } catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Image> ProcessImagesBatch()
        {
            List<Image> imagesBatch = new List<Image>();
            int numberImages = Utilities.ByteArrayToInt(ReadBytes(4));
            totalImages += numberImages;
            Console.WriteLine($"Found a batch with {numberImages} image(s)");
            for (int i = 0; i < numberImages; i++)
            {
                Console.WriteLine($"Processing image {i + 1}");
                imagesBatch.Add(ProcessImageBytes());
            }
            return imagesBatch;
        }

        public Image ProcessImageBytes()
        {
            Image img = new BitMapImage();
            img.Index = Utilities.ByteArrayToInt(ReadBytes(4));
            img.DataSize = Utilities.ByteArrayToInt(ReadBytes(4));
            img.Data = ReadBytes(img.DataSize);
            readerIndex += 4; // ignore 4 dummy bytes that are between images
            return img;
        }

        public Style[] ProcessStylesBatch()
        {
            int totalStyles = Utilities.ByteArrayToInt(ReadBytes(4));
            Style[] styles = new Style[totalStyles];
            for(int i = 0; i < totalStyles; i++)
            {
                Style styleAux = ProcessStyleBytes();
                styles[styleAux.Index] = styleAux;
            }
            return styles;
        }

        public Style ProcessStyleBytes()
        {
            Style style = new Style();
            style.Index = Utilities.ByteArrayToInt(ReadBytes(4));
            style.ParentIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            readerIndex += 24; // Unknown bytes. They are not necessary
            style.FontName = Utilities.ByteArrayToString(ReadBytes(32));
            style.ForegroundPictureIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            style.BackgroundPictureIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            readerIndex += 4;
            return style;
        }

        public void ProcessStyleAssignmentsBatch(TrueDBGrid grid)
        {
            int totalAssignments = Utilities.ByteArrayToInt(ReadBytes(4));
            readerIndex += 20; // ignore the first batch. It's always empty
            int i = 1;
            foreach(string styleName in Constants.GridSplitStyleAssignmentOrder)
            {
                StyleAssignment styleAssignment = ReadStyleAssignmentInfo(styleName);
                grid.StylesAssignments.Add(styleAssignment);
                if(styleAssignment.SplitIndex != 0 || styleAssignment.DisplayColumnIndex != 0)
                {
                    throw new ArgumentException("Unexpected index found");
                }
                i++;
            }
            while(i < totalAssignments)
            {
                StyleAssignment styleAssignmentInfo = ReadStyleAssignmentInfo(Constants.GridSplitStyleAssignmentOrder[0]);
                i++;
                if (styleAssignmentInfo.DisplayColumnIndex == 0)
                {
                    grid.getSplitAt(styleAssignmentInfo.SplitIndex - 1).StylesAssignments.Add(styleAssignmentInfo);
                    for (int j = 1; j < Constants.GridSplitStyleAssignmentOrder.Length; j++)
                    {
                        StyleAssignment splitStyleAsg = ReadStyleAssignmentInfo(Constants.GridSplitStyleAssignmentOrder[j]);
                        grid.getSplitAt(splitStyleAsg.SplitIndex - 1).StylesAssignments.Add(splitStyleAsg);
                        i++;
                    }
                }
                else
                {
                    styleAssignmentInfo.StyleName = Constants.DisplayColumnsStyleAssignmentOrder[0];
                    grid.getSplitAt(styleAssignmentInfo.SplitIndex - 1).getColumnAt(styleAssignmentInfo.DisplayColumnIndex - 1).StylesAssignments.Add(styleAssignmentInfo);
                    for (int j = 1; j < Constants.DisplayColumnsStyleAssignmentOrder.Length; j++)
                    {
                        StyleAssignment colStyleAsg = ReadStyleAssignmentInfo(Constants.DisplayColumnsStyleAssignmentOrder[j]);
                        grid.getSplitAt(colStyleAsg.SplitIndex - 1).getColumnAt(colStyleAsg.DisplayColumnIndex - 1).StylesAssignments.Add(colStyleAsg);
                        i++;
                    }
                }
            }
        }

        public StyleAssignment ReadStyleAssignmentInfo(string styleName)
        {
            StyleAssignment styleAssignment = new StyleAssignment();
            readerIndex += 4; // Unknown bytes
            styleAssignment.StyleIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            styleAssignment.SplitIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            styleAssignment.DisplayColumnIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            styleAssignment.NamedStyleIndex = Utilities.ByteArrayToInt(ReadBytes(4));
            styleAssignment.StyleName = styleName;
            return styleAssignment;
        }

        public void ReadNamedStyleInfo(TrueDBGrid grid, int idx)
        {
            NamedStyle namedStyle = new NamedStyle();
            namedStyle.Name = Utilities.ByteArrayToString(ReadBytes(26));
            readerIndex += 6;
            namedStyle.Style = grid.AllStyles[Utilities.ByteArrayToInt(ReadBytes(4))];
            namedStyle.Index = idx;
            grid.NamedStyles.Add(namedStyle);
        }

        public void ReadNamedStyleInfoBatch(TrueDBGrid grid)
        {
            int totalNamedStyles = Utilities.ByteArrayToInt(ReadBytes(4));
            for(int i = 0; i < totalNamedStyles; i++)
            {
                ReadNamedStyleInfo(grid, i + 1);
            }
        }

        public string ProcessGridName()
        {
            int nameSize = Utilities.ByteArrayToInt(ReadBytes(4)) - 1; // Don't include the null character at the end
            return Utilities.ByteArrayToString(ReadBytes(nameSize));
        }

        public void MoveIndexNextStartStylesImage()
        {
            readerIndex = Utilities.FirstPositionAfterPattern(FrxFile, Constants.IMAGES_START_BYTES, readerIndex);
        }

        public void MoveIndexNextStartGridName()
        {
            readerIndex = Utilities.FirstPositionAfterPattern(FrxFile, Constants.GRID_NAME_START_BYTES, readerIndex);
        }

        public byte[] ReadBytes(int n)
        {
            byte[] bytes = new byte[n];
            Array.Copy(FrxFile, readerIndex, bytes, 0, n);
            readerIndex += n;
            return bytes;
        }

        public void SaveImages()
        {
            if(totalImages > 0)
            {
                string formName = Path.GetFileNameWithoutExtension(frxFilePath);
                string frxFolder = Path.GetDirectoryName(frxFilePath);
                foreach (TrueDBGrid grid in Grids)
                {
                    foreach (Image img in grid.Images)
                    {
                        string outputFolder = $"{frxFolder}\\{formName}\\{grid.Name}";
                        Directory.CreateDirectory(outputFolder);
                        File.WriteAllBytes($"{outputFolder}\\{img.Index}.{img.Extension()}", img.FileData());
                    }
                }
            }
        }

        public void SaveStyleInfo()
        {
            string formName = Path.GetFileNameWithoutExtension(frxFilePath);
            string frxFolder = Path.GetDirectoryName(frxFilePath);
            string outputFilePath = $"{frxFolder}\\{formName}.xml";
            XElement root = new XElement("form");
            root.Add(new XAttribute("name", formName));
            XDocument document = new XDocument(root);
            foreach (TrueDBGrid grid in Grids)
            {
                root.Add(grid.GenerateStyleInfo());
            }
            document.Save(outputFilePath);
        }

        public void ReadBytesToArray()
        {
            FrxFile = File.ReadAllBytes(FrxFilePath);
        }
    }
}
