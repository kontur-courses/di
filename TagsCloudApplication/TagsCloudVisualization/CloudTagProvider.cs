using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TextConfiguration;


namespace TagsCloudVisualization
{
    public class CloudTagProvider
    {
        private readonly CloudTagProperties properties;
        private readonly WordsProvider provider;

        public CloudTagProvider(CloudTagProperties properties, WordsProvider provider)
        {
            this.properties = properties;
            this.provider = provider;
        }

        public List<CloudTag> ReadCloudTags(string filePath)
        {
            var processedWords = provider.ReadWordsFromFile(filePath);

            return processedWords
                .CountWords()
                .NormalizeByMin()
                .Select(pair => 
                    new CloudTag(pair.Key, new Font(
                                            properties.TextFontFamily, 
                                            (float)(properties.MinSize + 2 * pair.Value))))
                .ToList();
        }
    }
}
