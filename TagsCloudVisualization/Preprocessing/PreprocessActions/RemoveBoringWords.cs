using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Text;

namespace TagsCloudVisualization.Preprocessing
{
    public class RemoveBoringWords : IPreprocessor
    {
        private readonly IPosTagger posTagger;

        public RemoveBoringWords(IPosTagger partOfSpeechTagger)
        {
            posTagger = partOfSpeechTagger;
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                yield return word;
            }
        }
    }
}