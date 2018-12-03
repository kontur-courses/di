using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TagsCloudVisualization
{
    public class TextFileReader : IReader
    {
        private readonly string fileName;

        public TextFileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> ReadAllWords()
        {
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        yield break;
                    yield return line.Trim();
                }
            }
        }
    }
}
