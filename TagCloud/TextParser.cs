using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud
{
    public class TextParser :ITextParcer
    {
        private readonly ITextReader textReader;
        private readonly IWordChanger wordChanger;
        private readonly IWordParser wordParser;

        public TextParser(ITextReader reader, IWordChanger wordChanger, IWordParser wordParser)
        {
            textReader = reader;
            this.wordChanger = wordChanger;
            this.wordParser = wordParser;
        }

        public List<string> TryGetWordsFromText()
        {
            var text = textReader.TryReadText();
            if (text == null) return null;
            var notLetterRegexp = @"[^\'`\-A-Za-z]";
            return Regex.Split(text, notLetterRegexp)
                .Where(s => s.Length > 0).ToList();
        }


        public Dictionary<string, int> ParseWords(List<string> words)
        {
            var resultDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordParser.IsValidWord(word)) continue;
                var changedWord = wordChanger.ChangeWord(word);
                if (resultDictionary.ContainsKey(changedWord))
                    resultDictionary[changedWord]++;
                else
                    resultDictionary.Add(changedWord, 1);
            }
            return resultDictionary;
        }
    }
}