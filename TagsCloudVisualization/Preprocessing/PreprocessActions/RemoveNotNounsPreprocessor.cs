using System;
using System.Collections.Generic;
using System.Linq;
using edu.stanford.nlp.tagger.maxent;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Text;

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
            foreach (var word in words)
            {
                if(IsNoun(tagger.tagString(word)))
                    yield return word;
            }
        }

        private bool IsNoun(string taggedString) //https://www.ling.upenn.edu/courses/Fall_2003/ling001/penn_treebank_pos.html
        {
            var tag = taggedString.Split('_').Last();
            return tag.Contains("NN");
        }
    }
}