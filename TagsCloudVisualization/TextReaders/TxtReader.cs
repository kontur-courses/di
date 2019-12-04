using System;
using System.IO;

namespace TagsCloudVisualization
{
    public class TxtReader : ITextReader
    {
        private readonly FileStream stream;
        private string text;

        public TxtReader(string fileName)
        {
            try
            {
                stream = new FileStream(GetTextsPath(fileName), FileMode.Open);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Не удалось открыть файл", e);
            }
        }
        
        public string GetText()
        {
            using (var streamReader = new StreamReader(stream))
            {
                text = streamReader.ReadToEnd();
            }
            
            return text;
        }

        private string GetTextsPath(string name)
        {
            var projectPath =
                Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            var textsFolderPath = Path.Combine(projectPath, "texts");
            
            return Path.HasExtension(name)
                ? Path.Combine(textsFolderPath, name)
                : Path.Combine(textsFolderPath, $"{name}.txt");
        }
    }
}