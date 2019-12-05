using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Preprocessing
{
    public class ToLowercase : IPreprocessAction
    {
        public IEnumerable<Word> ProcessWords(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                word.Value = word.Value.ToLower();
                yield return word;
            }
        }
    }
}