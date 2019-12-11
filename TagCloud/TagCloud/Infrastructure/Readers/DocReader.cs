using Xceed.Words.NET;

namespace TagCloud
{
    public class DocReader : IReader
    {
        public string ReadAllText(string pathToFile)
        {
            using (var document = DocX.Load(pathToFile))
                return document.Text;
        }
    }
}
