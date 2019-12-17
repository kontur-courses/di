using System;
using System.IO;
using edu.stanford.nlp.tagger.maxent;

namespace TagsCloudContainer
{
    public abstract class DullWordEliminator : IDullWordsEliminator
    {
        protected readonly string model = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory,
            "english-bidirectional-distsim.tagger"});
        protected readonly MaxentTagger tagger;

        public DullWordEliminator()
        {
            tagger = new MaxentTagger(model);
        }

        public abstract bool IsDull(string s);
    }
}