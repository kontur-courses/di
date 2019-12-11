using Xceed.Words.NET;

namespace TagsCloudTextProcessing.Readers
{
    public class DocxTextReader : ITextReader
    {
        private readonly string path;
        public DocxTextReader(string path)
        {
            this.path = path;
        }
        public string ReadText()
        {
            using (var document = DocX.Load(path))
                return document.Text;
        }
    }
}