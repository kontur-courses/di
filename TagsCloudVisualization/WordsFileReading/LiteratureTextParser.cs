using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordsFileReading
{
    public class LiteratureTextParser : IParser
    {
        public IEnumerable<string> ParseText(string text)
        { 
            var currentIndex = 0;

            while (currentIndex < text.Length)
            {
                var wordStartPos = text.SkipUntil(currentIndex, IsWordSymbol);
                var afterWordPos = text.SkipUntil(wordStartPos, ch => !IsWordSymbol(ch));

                yield return text.Substring(wordStartPos, afterWordPos - wordStartPos);
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
