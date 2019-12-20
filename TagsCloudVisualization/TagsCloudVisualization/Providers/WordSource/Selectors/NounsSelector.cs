using System.Linq;
using edu.stanford.nlp.tagger.maxent;
using TagsCloudVisualization.Providers.WordSource.Interfaces;

namespace TagsCloudVisualization.Providers.WordSource.Selectors
{
    internal class NounsSelector : IWordSelector
    {
        private readonly MaxentTagger tagger;

        public NounsSelector()
        {
            var model = "english-bidirectional-distsim.tagger";

            tagger = new MaxentTagger(model);
        }

        public bool Select(string word)
        {
            return IsNoun(tagger.tagString(word));
        }

        private static bool IsNoun(string taggedString)
        {
            var tag = taggedString.Split('_').Last();
            return tag.Contains("NN");
        }
    }
}