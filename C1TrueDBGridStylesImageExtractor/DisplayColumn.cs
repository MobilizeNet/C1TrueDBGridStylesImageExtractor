using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractor
{
    public class DisplayColumn
    {
        private List<StyleAssignment> stylesAssignments = new List<StyleAssignment>();
        public List<StyleAssignment> StylesAssignments
        {
            get { return stylesAssignments; }
            set { stylesAssignments = value; }
        }

        public XElement GenerateStyleInfo(Style[] allstyles, List<NamedStyle> namedStyles)
        {
            XElement dispCol = new XElement("displayColumn");
            XElement styles = new XElement("styles");
            foreach (StyleAssignment style in stylesAssignments)
            {
                styles.Add(Utilities.GenerateSummarizedStyle(allstyles, namedStyles, style));

            }
            dispCol.Add(styles);
            return dispCol;
        }
    }
}
