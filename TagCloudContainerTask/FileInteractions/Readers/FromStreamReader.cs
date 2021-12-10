using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileInteractions.Readers
{
    public class FromStreamReader : ILinesReader
    {
        private readonly StreamReader streamReader;

        public FromStreamReader(StreamReader streamReader)
        {
            this.streamReader = streamReader;
        }

        public IEnumerable<string> ReadLines()
        {
            return ReadFromStream()
                .SelectMany(line => Regex.Split(line, @"\P{L}+", RegexOptions.Compiled))
                .Select(word => word);
        }

        private IEnumerable<string> ReadFromStream()
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