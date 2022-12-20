using System;
using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using DeepMorphy.Model;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public static class BoringWordsDeleter
    {
        public static IEnumerable<string> DeleteBoringWords(IEnumerable<string> words, IUi settings)
        {
            var ignoreWords = settings.ExceptWords.Contains('+')
                ? settings.ExceptWords.Trim().Split('+').Select(str => str.Trim().Split(' ')[0])
                : Array.Empty<string>();
            var deleteWords = settings.ExceptWords.Contains('-')
                ? settings.ExceptWords.Trim().Split('-').Select(str => str.Trim().Split(' ')[0])
                : Array.Empty<string>();
            var ignorePartOfSpeech = settings.ExceptPartOfSpeech.Contains('+')
                ? settings.ExceptPartOfSpeech.Trim().Split('+').Select(str => str.Trim().Split(' ')[0])
                : Array.Empty<string>();
            var deletePartOfSpeech = settings.ExceptPartOfSpeech.Contains('-')
                ? settings.ExceptPartOfSpeech.Trim().Split('-').Select(str => str.Trim().Split(' ')[0])
                : Array.Empty<string>();

            var morph = new MorphAnalyzer();
            var results = morph.Parse(words as string[]).Where(word =>
                (CheckPartOfSpeech(word, ignorePartOfSpeech, deletePartOfSpeech) || ignoreWords.Contains(word.Text))
                && !deleteWords.Contains(word.Text)).Select(word => word.Text).ToArray();
            return results;
        }

        private static bool CheckPartOfSpeech(MorphInfo word, IEnumerable<string> ignore, IEnumerable<string> delete)
        {
            if (ignore.Contains(word.BestTag.GramsDic["чр"]))
                return true;
            if (delete.Contains(word.BestTag.GramsDic["чр"]))
                return false;
            return word.BestTag.GramsDic["чр"] != "мест" &&
                   word.BestTag.GramsDic["чр"] != "предл" &&
                   word.BestTag.GramsDic["чр"] != "союз" &&
                   word.BestTag.GramsDic["чр"] != "част" &&
                   word.BestTag.GramsDic["чр"] != "межд";
        }
    }
}