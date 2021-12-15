using System.IO;

namespace TagCloud.TextProcessing
{
    public class TxtFileProvider : IFileProvider
    {
        public string GetTxtFilePath(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"Файл по пути {path} не был найден");
            return path;
        }
    }
}