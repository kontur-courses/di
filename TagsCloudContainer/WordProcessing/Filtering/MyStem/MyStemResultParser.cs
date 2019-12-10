using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordProcessing.Filtering.MyStem
{
    public class MyStemResultParser
    {
        public IEnumerable<(string, string)> GetPartsOfSpeechByResultOfNiCommand(string myStemResult,
            IEnumerable<string> words)
        {
            return words.Select(w => (w, GetPartOfSpeechForWord(myStemResult, w)));
        }

        private string GetPartOfSpeechForWord(string myStemResult, string word)
        {
            var partOfSpeechRegex = new Regex($@"(?:^|\s){word}{{.+?=(\w+)[,|=]");
            var match = partOfSpeechRegex.Match(myStemResult);
            var matchGroups = match.Groups;
            if (matchGroups.Count < 2)
            {
                throw new InvalidOperationException($"{nameof(myStemResult)} does not contain result for {word}");
            }

            return matchGroups[1].Value;
        }
    }
}