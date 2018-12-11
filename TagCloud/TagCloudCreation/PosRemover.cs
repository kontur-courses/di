using System;
using System.IO;
using edu.stanford.nlp.tagger.maxent;

namespace TagCloudCreation
{
    public abstract class PosRemover : IWordPreparer
    {
        private protected readonly MaxentTagger Tagger;

        protected PosRemover()
        {
            var s = Path.DirectorySeparatorChar;
            Tagger = new MaxentTagger($"..{s}..{s}..{s}TagCloudCreation{s}english-bidirectional-distsim.tagger");
        }

        public abstract string PrepareWord(string word, TagCloudCreationOptions options);

        private protected string PrepareWord(string word, Func<string, bool> tagPredicate)
        {
            var tag = Tagger.tagString(word);
            return tagPredicate(tag) ? null : word;
        }
    }
}
