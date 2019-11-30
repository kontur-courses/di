using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudVisualization
{
    public class TextReader
    {
        private readonly FileStream stream;
        private readonly string text;
        
        public TextReader(string fileName)
        {
            try
            {
                stream = new FileStream(GetTextsPath(fileName), FileMode.Open);
                text = new StreamReader(stream).ReadToEnd();
                stream.Close();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Не удалось открыть файл", e);
            }
        }

        public IEnumerable<string> GetWords()
        {
            return text.Split("\n");
        }

        private string GetTextsPath(string name)
        {
            var directoryName =
                new StringBuilder(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))))
                    .Append(@"\texts\");

            return Path.HasExtension(name)
                ? directoryName.Append(name).ToString()
                : directoryName.Append(name).Append(".txt").ToString();
        }
    }
}