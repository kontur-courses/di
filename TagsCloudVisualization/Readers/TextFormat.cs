using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Readers
{
    public class TextFormat
    {
        public static readonly TextFormat Txt = new(".txt");
        public static readonly TextFormat Doc = new(".doc");
        public static readonly TextFormat Docx = new(".docx");
        public static readonly TextFormat Pdf = new(".pdf");

        private static readonly HashSet<TextFormat> RegisteredFormats = new() { Txt, Doc, Docx, Pdf };

        private readonly string value;

        public string Filter => $"({value.Substring(1)})|*{value.Substring(1)}";

        private TextFormat(string format)
        {
            value = format;
        }

        public static TextFormat? GetFormatByExtension(string fileExtension)
        {
            return RegisteredFormats.FirstOrDefault(x => x.value == fileExtension.ToLower());
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value;
        }
    }
}