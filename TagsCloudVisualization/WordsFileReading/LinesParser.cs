using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace TagsCloudVisualization.WordsFileReading
{
    public class LinesParser : IParser
    {
        public IEnumerable<string> ParseText(string text)
        {
            return Regex.Split(text, Environment.NewLine)
                .Where(w => w != "")
                .Select(w => w.Trim());
        }

        public string GetModeName()
        {
            return "lines";
        }
    }
}
