using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class TextHandler
    {
        private readonly IDullWordsEliminator dullWordsEliminator;
        private static readonly Regex wordPattern = new Regex(@"\b[a-zA-Z]+", RegexOptions.Compiled);
        private readonly ITextReader textReader;

        public TextHandler(ITextReader textReader, IDullWordsEliminator dullWordsEliminator)
        {
            this.dullWordsEliminator = dullWordsEliminator;
            this.textReader = textReader;
        }

        public Dictionary<string, int> GetWordsFrequencyDict()
        {
            var result = new Dictionary<string, int>();
            foreach (var line in textReader.GetLines())
            {
                foreach (Match match in wordPattern.Matches(line))
                {
                    var currentWord = match.Value.ToLower();
                    if (!dullWordsEliminator.IsDull(currentWord))
                        result[currentWord] = result.ContainsKey(currentWord) ? result[currentWord] + 1 : 1;
                }
            }
            return result;
        }
    }
}