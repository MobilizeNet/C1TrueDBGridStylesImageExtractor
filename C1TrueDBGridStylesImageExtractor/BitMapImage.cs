using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class BitMapImage : Image
    {
        public override byte[] CreateFileHeader()
        {
            Byte[] header = new byte[14];
            //BM characters
            header[0] = 0x42;
            header[1] = 0x4D;
            // The data size and the 14 bytes of the file header
            Byte[] fileSize = BitConverter.GetBytes(DataSize + Constants.BMP_HEADER_SIZE);
            Array.Copy(fileSize, 0, header, 2, 4);
            // The start addresss of the image
            Array.Copy(ImageDataStartAddress(), 0, header, 10, 4);
            return header;
        }

        public override string Extension()
        {
            return Constants.BMP_EXTENSION;
        }

        public override byte[] FileData()
        {
            byte[] fileData = new byte[Constants.BMP_HEADER_SIZE + DataSize];
            Array.Copy(CreateFileHeader(), fileData, Constants.BMP_HEADER_SIZE);
            Array.Copy(Data, 0, fileData, 14, DataSize);
            return fileData;
        }

        public byte[] ImageDataStartAddress()
        {
            byte[] dibHeaderSize = new byte[4];
            Array.Copy(Data, dibHeaderSize, 4);
            int imageDataOffset = Utilities.ByteArrayToInt(dibHeaderSize) + Constants.BMP_HEADER_SIZE;
            return BitConverter.GetBytes(imageDataOffset);
        }


    }
}
