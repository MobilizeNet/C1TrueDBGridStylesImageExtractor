using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public abstract class Image
    {

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        private int dataSize;

        public int DataSize
        {
            get { return dataSize; }
            set { dataSize = value; }
        }

        public abstract byte[] CreateFileHeader();

        public abstract byte[] FileData();

        public abstract string Extension();

    }
}
