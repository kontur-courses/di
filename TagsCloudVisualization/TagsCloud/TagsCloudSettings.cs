using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization.TagsCloud
{
    public class TagsCloudSettings
    {
        public WordsSettings WordsSettings { get; set; }
        public Dictionary<string, int> WordFrequencyDictionary { get; set; }
        public Size ImageSize { get; set; }
        public Point Center { get; set; }

        public TagsCloudSettings(WordsSettings wordsSettings)
        {
            WordFrequencyDictionary = new Dictionary<string, int>() { { "Открой", 3 }, { "Файл", 2 } };
            Center = new Point(1000, 1000);
            ImageSize = new Size(2000, 2000);
            WordsSettings = wordsSettings;
        }
    }
}