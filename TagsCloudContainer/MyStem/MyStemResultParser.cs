using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.MyStem
{
    public class MyStemResultParser
    {
        public IEnumerable<(string, string)> GetPartsOfSpeechByResultOfNiCommand(string myStemResult,
            IEnumerable<string> words)
        {
            return words.Select(w => (w, GetPartOfSpeechForWord(myStemResult, w)));
        }

        public IEnumerable<(string, string)> GetInitialFormsByResultOfNiCommand(string myStemResult,
            IEnumerable<string> words)
        {
            return words.Select(w => (w, GetInitialFormForWord(myStemResult, w)));
        }

        private string GetPartOfSpeechForWord(string myStemResult, string word)
        {
            var partOfSpeechRegex = new Regex($@"(?:^|\s){word}{{.+?=(\w+)[,|=]");
            return GetInformationForWord(myStemResult, word, partOfSpeechRegex);
        }

        private string GetInitialFormForWord(string myStemResult, string word)
        {
            var initialFormRegex = new Regex($@"(?:^|\s){word}{{(\w+)");
            return GetInformationForWord(myStemResult, word, initialFormRegex);
        }

        private string GetInformationForWord(string myStemResult, string word, Regex informationRegex)
        {
            var match = informationRegex.Match(myStemResult);
            var matchGroups = match.Groups;
            if (matchGroups.Count < 2)
            {
                throw new InvalidOperationException($"{nameof(myStemResult)} does not contain result for {word}");
            }

            return matchGroups[1].Value;
        }
    }
}