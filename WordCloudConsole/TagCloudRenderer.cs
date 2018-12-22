using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCloudImageGenerator;
using WordCloudImageGenerator.Parsing.BlackList;
using WordCloudImageGenerator.Parsing.Extractors;
using WordCloudImageGenerator.Parsing.Word;

namespace WordCloudConsole
{
    public class TagCloudRenderer
    {
        private TagCloud tagCloud;
        private readonly IBlackList blackList;
        private readonly IWordExtractor wordExtractor;
        private readonly ITagCloudVizualizer vizualizer;
        private readonly WordCloudConfig wordCloudConfig;

        public TagCloudRenderer(IWordExtractor wordExtractor, IBlackList blackList, ITagCloudVizualizer vizualizer,
            WordCloudConfig wordCloudConfig)
        {
            this.wordExtractor = wordExtractor;
            this.blackList = blackList;
            this.vizualizer = vizualizer;
            this.wordCloudConfig = wordCloudConfig;
        }

        public string GetLayout(string words)
        {
            var weightedWords = wordExtractor.GetWords(words)
                .Filter(blackList)
                .CountEntries()
                .SortByEntries();

            tagCloud = new TagCloud(wordCloudConfig, vizualizer);
            var imagePath = tagCloud.ArrangeLayout(weightedWords);
            return imagePath;
        }
    }
}
