using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using com.sun.imageio.plugins.common;
using com.sun.xml.@internal.messaging.saaj.util;
using ikvm.extensions;
using java.io;
using javax.annotation;
using opennlp;
using opennlp.tools.postag;
using opennlp.tools.tokenize;

namespace TagCloud
{
    public class GoodWordsCounter : IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();
        private readonly Func<string, bool> posChecker;

        private readonly string[] allowedPos = {
            "noun",
            "pronoun",
            "adjective",
            "verb",
            "adverb"
        };

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public GoodWordsCounter()
        {
            var tokenizer = WhitespaceTokenizer.INSTANCE; 
            var tagger = new POSTaggerME(new POSModel(new FileInputStream(".\\en-pos-maxent.bin")));
            posChecker = s => tagger.tag(tokenizer.tokenize(s))
                .Select(x => x.ToLower())
                .Intersect(allowedPos).Any();
        }

        public void UpdateWith(string text)
        {
            var words = text
                .ToLower() 
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var word in words)
                IncOrAddToCounter(word);
        }
        
        private void IncOrAddToCounter(string word)
        {
            if (countedWords.TryGetValue(word, out var count))
            {
                if (count < 0)
                    return;
                countedWords[word] = count + 1;
            }
            else
                countedWords[word] = posChecker(word) ? 1 : -1;
        }
    }
}