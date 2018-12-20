using System.Collections.Generic;
using System.IO;


namespace TagsCloudVisualization.WordsFileReading
{
    public class LinesParser : IParser
    {
        public IEnumerable<string> ParseText(TextReader textReader)
        {
            using (textReader)
            {
                var nextLine = textReader.ReadLine();
                while (nextLine != null)
                {
                    var trimmedLine = nextLine.Trim();
                    if (trimmedLine != "")
                        yield return trimmedLine;
                    nextLine = textReader.ReadLine();
                }
            }
        }

        public string GetModeName()
        {
            return "lines";
        }
    }
}
