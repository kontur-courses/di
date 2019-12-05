using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Text;

namespace TagsCloudVisualization.Preprocessing
{
    public class RemoveBoringWords : IPreprocessAction
    {
        private readonly IPosTagger posTagger;

        public RemoveBoringWords(IPosTagger partOfSpeechTagger)
        {
            posTagger = partOfSpeechTagger;
        }

        public IEnumerable<Word> ProcessWords(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                yield return word;
            }
        }
    }
}