using System;
using System.IO;
using System.Text;

namespace TagsCloudVisualization.TextReaders
{
    public class TxtReader : ITextReader
    {
        public string ReadText(string filePath, Encoding encoding)
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
            using (var streamReader = new StreamReader(stream, encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}