using Xceed.Words.NET;

namespace TagCloud.Reader.FormatReader
{
    public class DocxReader : IFormatReader
    {
        public string Format => "docx";

        public string Read(string fileName) => DocX.Load(fileName).Text;
    }
}