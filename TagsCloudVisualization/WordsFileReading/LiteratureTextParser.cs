using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsFileReading
{
    public class LiteratureTextParser : IParser
    {
        public IEnumerable<string> ParseText(TextReader textReader)
        {
            var nextLine = textReader.ReadLine();
            while (nextLine != null)
            {
                foreach (var word in ParseLine(nextLine))
                    yield return word;
                nextLine = textReader.ReadLine();
            }
        }

        private IEnumerable<string> ParseLine(string line)
        {
            var currentIndex = 0;

            while (currentIndex < line.Length)
            {
                var wordStartPos = line.SkipUntil(currentIndex, IsWordSymbol);
                var afterWordPos = line.SkipUntil(wordStartPos, ch => !IsWordSymbol(ch));

                if (wordStartPos < afterWordPos)
                    yield return line.Substring(wordStartPos, afterWordPos - wordStartPos);
                currentIndex = afterWordPos;
            }
        }

        public string GetModeName()
        {
            return "lit";
        }

        private bool IsWordSymbol(char ch)
        {
            return Char.IsLetter(ch);
        }
    }
}
