using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Preprocessing.PreprocessActions
{
    public class RemoveIgnoredWords : IPreprocessor
    {
        private readonly AppSettings appSettings;

        public RemoveIgnoredWords(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            var wordsToIgnore = appSettings.Restrictions.WordsToIgnore
                .Select(x => x.ToLower()).ToHashSet();
            return words.Where(word => !wordsToIgnore.Contains(word));
        }
    }
}