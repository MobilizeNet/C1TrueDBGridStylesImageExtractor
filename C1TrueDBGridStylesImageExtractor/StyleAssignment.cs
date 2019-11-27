using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class StyleAssignment
    {
        private int styleIndex;

        public int StyleIndex
        {
            get { return styleIndex; }
            set { styleIndex = value; }
        }

        private int namedStyleIndex;

        public int NamedStyleIndex
        {
            get { return namedStyleIndex; }
            set { namedStyleIndex = value; }
        }

        private int splitIndex;

        public int SplitIndex
        {
            get { return splitIndex; }
            set { splitIndex = value; }
        }

        private int displayColumnIndex;

        public int DisplayColumnIndex
        {
            get { return displayColumnIndex; }
            set { displayColumnIndex = value; }
        }

        private string styleName;

        public string StyleName
        {
            get { return styleName; }
            set { styleName = value; }
        }
    }
}
