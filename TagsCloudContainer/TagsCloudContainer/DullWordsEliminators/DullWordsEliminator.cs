using edu.stanford.nlp.tagger.maxent;
using System;

namespace TagsCloudContainer
{
    public abstract class DullWordEliminator : IDullWordsEliminator
    {
        protected const string model = "english-bidirectional-distsim.tagger";
        protected readonly MaxentTagger tagger;

        public DullWordEliminator()
        {
            tagger = new MaxentTagger(model);
        }

        public abstract bool IsDull(string s);
    }
}