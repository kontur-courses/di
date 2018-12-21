using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using com.sun.imageio.plugins.common;
using com.sun.xml.@internal.messaging.saaj.util;
using Extensions;
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
        private Func<string, bool> posChecker;

        private readonly string[] allowedPos = {
            "noun",
            "pronoun",
            "adjective",
            "verb",
            "adverb"
        };

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

//        public GoodWordsCounter()
//        {//It becomes too ugly, if functionalize it 
//         //But constructor should not errors throw anyway
        
//            var tokenizer = WhitespaceTokenizer.INSTANCE; 
//            var tagger = new POSTaggerME(new POSModel(new FileInputStream(".\\en-pos-maxent.bin")));
//            posChecker = s => tagger.tag(tokenizer.tokenize(s))
//                .Select(x => x.ToLower())
//                .Intersect(allowedPos).Any();
//        }

        private Result<None> BuildChecker()=>
            Result.Of(() => (
                    WhitespaceTokenizer.INSTANCE,
                    new POSTaggerME(new POSModel(new FileInputStream(".\\en-pos-maxent.bin")))))
                .ThenAct<(WhitespaceTokenizer tokenizer, POSTaggerME tagger)>(t =>
                    posChecker = s => t.tagger.tag(t.tokenizer.tokenize(s))
                        .Select(x => x.ToLower())//Lambda cannot return func
                        .Intersect(allowedPos).Any())
                .PureResult();

        private bool CheckWord(string word)
        {
            if (posChecker == null)
                BuildChecker();
            return posChecker(word);
        }

        public Result<None> UpdateWith(string text) =>
            Split(text).Select(IncOrAddToCounter).FirstOrDefault(x => x.IsSuccess);

        private string[] Split(string text)=>
            text.ToLower()
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        
        private Result<None> IncOrAddToCounter(string word)=>
            Result.Of(() => countedWords.TryGetValue(word, out var count) 
                                        ? (count >= 0 ? count + 1 : -1) 
                                        : (CheckWord(word) ? 1 : -1))
                .RefineError("Opennlp thrown error:")
                .ThenAct(i => countedWords[word] = i + 1)
                .PureResult();
    }
}