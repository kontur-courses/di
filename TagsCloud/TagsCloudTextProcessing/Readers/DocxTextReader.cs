using Xceed.Words.NET;

namespace TagsCloudTextProcessing.Readers
{
    public class DocxTextReader : ITextReader
    {
        public string ReadText(string path)
        {
            using (var document = DocX.Load(path))
                return document.Text;
        }
    }
}