using System.IO;
using Xceed.Words.NET;

namespace TagsCloudVisualization.TextProcessing.TextReader
{
    public class TextReader
    {
        public static string ReadAllText(string path)
        {
            if (!File.Exists(path))
                throw new IOException("File you specified does not exist");

            return GetDocumentText(path);
        }

        private static string GetDocumentText(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return extension switch
            {
                ".doc" => LoadDocumentWithStream(fileName),
                ".docx" => LoadDocumentWithStream(fileName),
                ".txt" => File.ReadAllText(fileName),
                _ => throw new IOException($"Files with extension {extension} doesn't support")
            };
        }
        
        private static string LoadDocumentWithStream(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var document = DocX.Load(fs);
            return document.Text;
        }
    }
}