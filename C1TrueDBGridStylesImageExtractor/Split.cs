using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractor
{
    public class Split
    {
        private List<StyleAssignment> stylesAssignments = new List<StyleAssignment>();
        public List<StyleAssignment> StylesAssignments
        {
            get { return stylesAssignments; }
            set { stylesAssignments = value; }
        }

        private List<DisplayColumn> columns = new List<DisplayColumn>();
        public List<DisplayColumn> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public DisplayColumn getColumnAt(int index)
        {
            return Utilities.GetCreateListElement(this.columns, index);
        }

        public XElement GenerateStyleInfo(Style[] allstyles, List<NamedStyle> namedStyles)
        {
            XElement split = new XElement("split");
            XElement styles = new XElement("styles");
            foreach (StyleAssignment style in stylesAssignments)
            {
                styles.Add(Utilities.GenerateSummarizedStyle(allstyles, namedStyles, style));

            }
            split.Add(styles);
            XElement columnsXelement = new XElement("columns");
            for(int i = 0; i < columns.Count; i++)
            {
                XElement colX = columns[i].GenerateStyleInfo(allstyles, namedStyles);
                colX.Add(new XAttribute("idx", i));
                columnsXelement.Add(colX);
            }
            split.Add(columnsXelement);
            return split;
        }
    }
}
