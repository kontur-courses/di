using Xceed.Words.NET;

namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class DocFileReader : IFileReader
    {
        public string Read(string filename)
        {
            using var document = DocX.Load(filename);
            return document.Text;
        }

        public bool CanRead(string extension) => extension == "docx" || extension == "doc";
    }
}