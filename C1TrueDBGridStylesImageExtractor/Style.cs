using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class Style
    {
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private int parentIndex;

        public int ParentIndex
        {
            get { return parentIndex; }
            set { parentIndex = value; }
        }

        private int foregroundPictureIndex;

        public int ForegroundPictureIndex
        {
            get { return foregroundPictureIndex; }
            set { foregroundPictureIndex = value; }
        }

        private int backgroundPictureIndex;

        public int BackgroundPictureIndex
        {
            get { return backgroundPictureIndex; }
            set { backgroundPictureIndex = value; }
        }

        private string fontName;

        public string FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }
    }
}
