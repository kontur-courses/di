using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Selectors
{
    class WordSelectorFactory : ISelectorFactory<string>
    {
        public IEnumerable<ISelector<string>> GetSelectors(ReaderSettings readerSettings)
        {
            yield return new BadWordsSelector(readerSettings.BadWords);
        }
    }
}