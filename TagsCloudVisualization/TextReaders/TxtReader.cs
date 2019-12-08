using System;
using System.IO;

namespace TagsCloudVisualization.TextReaders
{
    public class TxtReader : ITextReader
    {
        public string ReadText(string filePath)
        {
            FileStream stream;
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
                return streamReader.ReadToEnd();
            }
        }
    }
}