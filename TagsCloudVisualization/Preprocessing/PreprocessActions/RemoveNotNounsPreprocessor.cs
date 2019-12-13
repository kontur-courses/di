using System.Collections.Generic;
using System.Linq;
using edu.stanford.nlp.tagger.maxent;

namespace TagsCloudVisualization.Preprocessing
{
    public class RemoveNotNounsPreprocessor : IPreprocessor
    {
        private readonly MaxentTagger tagger;

        public RemoveNotNounsPreprocessor()
        {
            var model = "english-bidirectional-distsim.tagger";
            tagger = new MaxentTagger(model);
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            return words.Where(word => IsNoun(tagger.tagString(word)));
        }

        private static bool
            IsNoun(string taggedString) //https://www.ling.upenn.edu/courses/Fall_2003/ling001/penn_treebank_pos.html
        {
            var tag = taggedString.Split('_').Last();
            return tag.Contains("NN");
        }
    }
}