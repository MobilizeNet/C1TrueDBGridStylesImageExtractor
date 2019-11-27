using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1TrueDBGridStylesImageExtractor
{
    public class Constants
    {
        public const int BMP_HEADER_SIZE = 14;
        public readonly static  byte[] IMAGES_START_BYTES = { 0x55, 0x53, 0x74, 0x79, 0x6c, 0x65, 0x01, 0x05 };
        public readonly static byte[] GRID_NAME_START_BYTES = { 0x90, 0xd0, 0x03, 0x00, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public const string BMP_EXTENSION = "bmp";
        public readonly static string[] GridSplitStyleAssignmentOrder = {
            "Style",
            "CaptionStyle",
            "HeadingStyle",
            "FootingStyle",
            "InactiveStyle",
            "SelectedStyle",
            "EditorStyle",
            "HighLightRowStyle",
            "EvenRowStyle",
            "OddRowStyle"
        };
        public readonly static string[] DisplayColumnsStyleAssignmentOrder =
        {
            "Style",
            "HeadingStyle",
            "FootingStyle",
            "EditorStyle"
        };
    }
}
