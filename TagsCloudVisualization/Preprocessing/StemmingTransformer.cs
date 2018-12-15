using System;
using NHunspell;

namespace TagsCloudVisualization.Preprocessing
{
    public class StemmingTransformer : IWordTransformer, IDisposable
    {
        private readonly Hunspell hunspell;

        public StemmingTransformer(string dicFile, string affFile)
        {
            hunspell = new Hunspell(affFile, dicFile);
        }

        public string TransformWord(string word)
        {
            var stemResults = hunspell.Stem(word);

            if (stemResults == null || stemResults.Count == 0)
                return word;
            return stemResults[0];
        }

        public void Dispose()
        { 
            hunspell.Dispose();
        }
    }
}
