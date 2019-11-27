using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractor
{
    public class TrueDBGrid
    {
        private List<Image> images = new List<Image>();
        public List<Image> Images
        {
            get { return images; }
            set { images = value; }
        }

        private Style[] allStyles;
        public Style[] AllStyles
        {
            get { return allStyles; }
            set { allStyles = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<StyleAssignment> stylesAssignments = new List<StyleAssignment>();
        public List<StyleAssignment> StylesAssignments
        {
            get { return stylesAssignments; }
            set { stylesAssignments = value; }
        }

        private List<Split> splits = new List<Split>();

        public List<Split> Splits
        {
            get { return splits; }
            set { splits = value; }
        }

        private List<NamedStyle> namedStyles = new List<NamedStyle>();

        public List<NamedStyle> NamedStyles
        {
            get { return namedStyles; }
            set { namedStyles = value; }
        }

        public Split getSplitAt(int index)
        {
            return Utilities.GetCreateListElement(this.splits, index);
        }

        public XElement GenerateStyleInfo()
        {
            XElement grid = new XElement("grid");
            grid.Add(new XAttribute("name", this.Name));
            XElement styles = new XElement("styles");
            foreach(StyleAssignment style in stylesAssignments)
            {
                styles.Add(Utilities.GenerateSummarizedStyle(AllStyles, namedStyles, style));
                
            }
            grid.Add(styles);
            XElement splitsXelements = new XElement("splits");
            for(int i = 0; i < Splits.Count; i++)
            {
                XElement splitX = splits[i].GenerateStyleInfo(allStyles, namedStyles);
                splitX.Add(new XAttribute("idx", i));
                splitsXelements.Add(splitX);
            }
            grid.Add(splitsXelements);
            return grid;
        }
    }

}
