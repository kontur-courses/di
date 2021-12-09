using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.Words.Reading
{
    public class WordsReader : IWordsReader
    {
        public IEnumerable<string> ReadWordsFrom(StreamReader streamReader)
        {
            return ReadWords(streamReader)
                .SelectMany(line => Regex.Split(line, @"\P{L}+", RegexOptions.Compiled))
                .Select(word => word);
        }

        private IEnumerable<string> ReadWords(StreamReader streamReader)
        {
            using (streamReader)
            {
                var line = streamReader.ReadLine();

                while (!string.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                    line = streamReader.ReadLine();
                }
            }
        }
    }
}