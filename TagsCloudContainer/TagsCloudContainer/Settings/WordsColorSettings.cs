using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class WordsColorSettings : IWordsColorSettings
    {
        public IWordColorMapper ColorMapper { get; }

        public WordsColorSettings(IWordColorMapper colorMapper)
        {
            ColorMapper = colorMapper;
        }
    }
}