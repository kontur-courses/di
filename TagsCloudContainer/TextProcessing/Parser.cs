using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.TextProcessing
{
    public static class Parser
    {
        public static Dictionary<string, List<string>> ParseToPartSpeechAndWords(string mystemText)
        {
            var partOfSpeechAndWords = new Dictionary<string, List<string>>();
            var regex = new Regex(@"\w+{(\w+=\w+).*}");
            foreach (var line in mystemText.Split("\r\n"))
            {
                var match = regex.Match(line);
                if (!match.Success) continue;
                var wordAndPartOfSpeech = match.Groups[1].Value.Split('=');
                if (partOfSpeechAndWords.ContainsKey(wordAndPartOfSpeech[1]))
                    partOfSpeechAndWords[wordAndPartOfSpeech[1]].Add(wordAndPartOfSpeech[0]);
                else
                    partOfSpeechAndWords[wordAndPartOfSpeech[1]] = new List<string> {wordAndPartOfSpeech[0]};
            }

            return partOfSpeechAndWords;
        }
    }
}