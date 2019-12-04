using System.Collections.Generic;
using System.IO;

namespace TagCloud
{
    public class FilterSettings
    {
        public readonly HashSet<string> BoringWords;
        public readonly HashSet<SpeechPart> SpeechParts;

        public FilterSettings(HashSet<string> boringWords, HashSet<SpeechPart> speechParts)
        {
            BoringWords = boringWords;
            SpeechParts = speechParts;
        }

        public static FilterSettings GetDefaultSettings() =>
            new FilterSettings(GetBoringWords(), new HashSet<SpeechPart>()
            {
                SpeechPart.Noun,
                SpeechPart.Verb,
                SpeechPart.Adjective,
                SpeechPart.Adverb,
                SpeechPart.Interjection
            });

        private static HashSet<string> GetBoringWords() =>
            new HashSet<string>(File.ReadAllLines($"{HelperMethods.GetProjectDirectory()}\\BoringWords.txt"));
    }
}
