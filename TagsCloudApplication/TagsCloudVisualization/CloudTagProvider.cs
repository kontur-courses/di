using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TextConfiguration;


namespace TagsCloudVisualization
{
    public class CloudTagProvider : ITagProvider
    {
        private readonly CloudTagProperties properties;
        private readonly IWordsProvider provider;

        public CloudTagProvider(CloudTagProperties properties, IWordsProvider provider)
        {
            this.properties = properties;
            this.provider = provider;
        }

        public List<CloudTag> ReadCloudTags(string filePath)
        {
            var processedWords = provider.ReadWordsFromFile(filePath);
            if (processedWords.Count == 0)
                return new List<CloudTag>();

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
