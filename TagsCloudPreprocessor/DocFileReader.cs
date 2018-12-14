using Xceed.Words.NET;

namespace TagsCloudPreprocessor
{
    public class DocFileReader : IFileReader
    {
        public string ReadFromFile(string path)
        {
            var text = DocX.Load(path).Text;
            return text;
        }
    }
}