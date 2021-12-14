using Xceed.Words.NET;

namespace TagsCloudVisualization.WordsProvider.FileReader
{
    internal class DocFileReader : IWordsReader
    {
        public bool IsSupportedFileExtension(string extension) => extension is ".doc" or ".docx";

        public string GetFileContent(string path)
        {
            using var document = DocX.Load(path);
            return document.Text;
        }
    }
}