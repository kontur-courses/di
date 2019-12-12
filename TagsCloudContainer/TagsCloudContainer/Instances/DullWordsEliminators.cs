using edu.stanford.nlp.tagger.maxent;
using System;

namespace TagsCloudContainer
{
    public class DefaultDullWordsEliminator : IDullWordsEliminator
    {
        private const string model = "english-bidirectional-distsim.tagger";
        private readonly MaxentTagger tagger;

        public DefaultDullWordsEliminator()
        {
            tagger = new MaxentTagger(model);
        }

        public bool IsDull(string s)
        {
            return tagger.tagString(s).Contains("IN") || tagger.tagString(s).Contains("DT") ||
                   tagger.tagString(s).Contains("CC") || tagger.tagString(s).Contains("PRP$") ||
                   tagger.tagString(s).Contains("PRP") || tagger.tagString(s).Contains("TO") ||
                   tagger.tagString(s).Contains("VBP") || tagger.tagString(s).Contains("VBZ") ||
                   tagger.tagString(s).Contains("LS") || s.Length == 1;
        }
    }

    public class OnlyNounDullWordsEliminator : IDullWordsEliminator
    {
        private const string model = "english-bidirectional-distsim.tagger";
        private readonly MaxentTagger tagger;

        public OnlyNounDullWordsEliminator()
        {
            tagger = new MaxentTagger(model);
        }

        public bool IsDull(string s)
        {
            return !(tagger.tagString(s).Contains("NN") || tagger.tagString(s).Contains("NNS") ||
                     tagger.tagString(s).Contains("NNP") || tagger.tagString(s).Contains("NNPS"));
        }
    }
}