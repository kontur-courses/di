using System;
using System.Collections.Generic;


namespace TagsCloudContainer
{
    internal interface IWordsCustomizer
    {
        string CustomizeWord(string word);
        void SetIgnoreFunc(Func<string, bool> ignoreFunc);
        void SetWordsToIgnore(IEnumerable<string> wordsToIgnore);
        void AddWordsToIgnore(IEnumerable<string> newWordsToIgnore);
    }
}