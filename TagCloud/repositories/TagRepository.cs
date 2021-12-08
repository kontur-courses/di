using System.Collections.Generic;
using System.Drawing;
using TagCloud.configurations;
using TagCloud.layouter;

namespace TagCloud.repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly List<Tag> tags;

        public TagRepository(
            IRepository<string> wordRepository,
            ITagRepositoryConfiguration configuration,
            ICloudLayouter cloudLayouter
        )
        {
            tags = new List<Tag>();
            foreach (var (word, freq) in wordRepository.CalculateWordStatistics())
            {
                var font = new Font(configuration.GetFamilyFont(), freq * configuration.GetSize());
                var layoutRectangle = cloudLayouter.PutNextRectangle(new Size((int) font.Size * word.Length, font.Height));
                tags.Add(new Tag(word, configuration.GetColor(), font, layoutRectangle));
            }
        }

        public IEnumerable<Tag> Get() => tags;
    }
}