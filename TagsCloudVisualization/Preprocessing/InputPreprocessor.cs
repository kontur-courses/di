using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Preprocessing
{
    public class InputPreprocessor
    {
        private readonly AppSettings appSettings;
        private readonly IPreprocessor[] preprocessors;

        public InputPreprocessor(IPreprocessor[] preprocessors, AppSettings appSettings)
        {
            this.preprocessors = preprocessors;
            this.appSettings = appSettings;
        }

        public Word[] PreprocessWords(IEnumerable<string> words)
        {
            var preprocessedWords = preprocessors
                .Aggregate(words, (current, action) => action.ProcessWords(current))
                .Select(x => new Word(x));

            return appSettings.Restrictions.AmountOfWordsOnTagCloud >= 0
                ? preprocessedWords.Take(appSettings.Restrictions.AmountOfWordsOnTagCloud).ToArray()
                : preprocessedWords.ToArray();
        }
    }
}