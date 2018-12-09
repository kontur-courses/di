using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization.TagsCloud
{
    public class TagsCloudSettings
    {
        public WordsSettings WordsSettings { get; set; }
        public Dictionary<string, int> FrequenciesByWords { get; set; }
        public Palette Palette { get; set; }
        public ImageSettings ImageSettings { get; set; }

        public TagsCloudSettings(WordsSettings wordsSettings, Palette palette, ImageSettings imageSettings)
        {
            FrequenciesByWords = wordsSettings.WordAnalyzer.MakeWordFrequencyDictionary();
            WordsSettings = wordsSettings;
            Palette = palette;
            ImageSettings = imageSettings;
        }
    }
}