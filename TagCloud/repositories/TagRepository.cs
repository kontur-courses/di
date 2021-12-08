using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.configurations;
using TagCloud.layouter;

namespace TagCloud.repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private const int Ext = 100;
        private readonly IRepository<string> wordRepository;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ITagConfiguration tagConfiguration;

        public TagRepository(
            IRepository<string> wordRepository,
            ICloudLayouter cloudLayouter,
            ITagConfiguration tagConfiguration
        )
        {
            this.wordRepository = wordRepository;
            this.cloudLayouter = cloudLayouter;
            this.tagConfiguration = tagConfiguration;
        }

        public IEnumerable<Tag> Get() 
            => wordRepository.CalculateWordStatistics()
                .Select(x => new Tag(
                    x.Key,
                    tagConfiguration,
                    cloudLayouter.PutNextRectangle(CalculateTagSize(x.Value))
                ));

        private static Size CalculateTagSize(int freq) => new Size(Ext * freq, Ext * freq);
    }
}