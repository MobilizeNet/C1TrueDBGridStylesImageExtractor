using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class NamedStyle
    {
        private Style style = new Style();

        public Style Style
        {
            get { return style; }
            set { style = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}
