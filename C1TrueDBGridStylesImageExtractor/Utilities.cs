using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C1TrueDBGridStylesImageExtractor
{
    public class Utilities
    {
        public static int ByteArrayToInt(Byte[] array)
        {
            // Make sure is a 4 byte array
            Byte[] aux = new Byte[4];
            Array.Copy(array, aux, array.Length);
            return BitConverter.ToInt32(aux, 0);
        }

        public static string ByteArrayToString(Byte[] array)
        {
            byte[] arrayTrimmed = array.TakeWhile(b => b != 0).ToArray();
            return Encoding.UTF8.GetString(arrayTrimmed, 0, arrayTrimmed.Length);
        }

        // Taken from https://stackoverflow.com/questions/283456/byte-array-pattern-search
        public static IEnumerable<int> PatternAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    yield return i;
                }
            }
        }

        public static int FirstPositionAfterPattern(byte[] source, byte[] pattern, int sourceStart)
        {
            for(int i = sourceStart; i < source.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    return i + pattern.Length;
                }
            }
            throw new ArgumentException("Finished reading the file");
        }

        public static T GetCreateListElement<T>(List<T> list, int index) where T : new()
        {
            // Gets an element according to an index and creates an element if it's out of bounds
            while (index >= list.Count())
            {
                list.Add(new T());
            }
            return list[index];
        }

        public static XElement GenerateSummarizedStyle(Style[] allStyles, List<NamedStyle> namedStyles, StyleAssignment styleAssignment)
        {
            XElement styleXelement = new XElement(styleAssignment.StyleName);
            int namedStyleIdx = styleAssignment.NamedStyleIndex;
            int styleIdx = styleAssignment.StyleIndex;
            if (allStyles[styleIdx].BackgroundPictureIndex != -1)
            {
                styleXelement.Add(new XAttribute("BackgroundPictureIndex", allStyles[styleIdx].BackgroundPictureIndex));
            }
            else if (namedStyleIdx != -1 && namedStyles[namedStyleIdx - 1].Style.BackgroundPictureIndex != -1)
            {
                styleXelement.Add(new XAttribute("BackgroundPictureIndex", namedStyles[namedStyleIdx - 1].Style.BackgroundPictureIndex));
            }
            if (allStyles[styleIdx].ForegroundPictureIndex != -1)
            {
                styleXelement.Add(new XAttribute("ForegroundPictureIndex", allStyles[styleIdx].ForegroundPictureIndex));
            }
            else if (namedStyleIdx != -1 && namedStyles[namedStyleIdx - 1].Style.ForegroundPictureIndex != -1)
            {
                styleXelement.Add(new XAttribute("ForegroundPictureIndex", namedStyles[namedStyleIdx - 1].Style.ForegroundPictureIndex));
            }
            if (allStyles[styleIdx].FontName != "")
            {
                styleXelement.Add(new XAttribute("FontName", allStyles[styleIdx].FontName));
            }
            else if (namedStyleIdx != -1 && namedStyles[namedStyleIdx -1].Style.FontName != "")
            {
                styleXelement.Add(new XAttribute("FontName", namedStyles[namedStyleIdx - 1].Style.FontName));
            }
            return styleXelement;

        }
    }
}
