using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileInteractions.Readers
{
    public class LinesReader : ILinesReader
    {
        public IEnumerable<string> ReadLinesFrom(StreamReader streamReader)
        {
            return ReadLines(streamReader)
                .SelectMany(line => Regex.Split(line, @"\P{L}+", RegexOptions.Compiled))
                .Select(word => word);
        }

        private IEnumerable<string> ReadLines(StreamReader streamReader)
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