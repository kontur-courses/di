using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(StreamReader reader, char[] separators)
        {
            var words = new List<string>();
            var line = reader.ReadLine();

            while (line != null)
            {
                words.AddRange(line.Split(separators).Where(word => word.Length > 0));
                line = reader.ReadLine();
            }

            return words;
        }
    }
}