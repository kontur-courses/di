using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using NHunspell;

namespace TagCloud
{
    public class StemWordsCounter: IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public Result<None> UpdateWith(string text)=>
            Result.Of(() => new Hunspell("en_us.aff", "en_us.dic"))
                .RefineError("Hunspell library occured: ")
                .Then(hsl =>
                    Split(text).Foreach(word => IncOrAddToCounter(hsl.Stem(hsl.Suggest(word).First()).First())));
            
//            using (var hsl = new Hunspell("en_us.aff", "en_us.dic"))
//                foreach (var word in words)
//                    IncOrAddToCounter(hsl.Stem(hsl.Suggest(word).First()).First());
        

        private string[] Split(string text)=>
            text.ToLower()
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


        private void IncOrAddToCounter(string word) =>
            Result.Of(() => countedWords.TryGetValue(word, out var count) ? count : 0)
                .Then(i => countedWords[word] = i + 1);
    }
}