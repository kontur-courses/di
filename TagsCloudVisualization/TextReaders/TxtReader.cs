using System;
using System.IO;

namespace TagsCloudVisualization.TextReaders
{
    public class TxtReader : ITextReader
    {
        private FileStream stream;
        private string text;

        public string ReadText(string filePath)
        {
            try
            {
                stream = new FileStream(filePath, FileMode.Open);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Не удалось открыть файл", e);
            }
            using (var streamReader = new StreamReader(stream))
            {
                text = streamReader.ReadToEnd();
            }
            
            return text;
        }
    }
}