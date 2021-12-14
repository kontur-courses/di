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
            IRepository<string> repository,
            IWordHelper helper,
            ITagRepositoryConfiguration configuration,
            ICloudLayouter layouter
        )
        {
            tags = new List<Tag>();
            foreach (var wordStatistic in helper.GetWordStatistics(ProcessWords(repository.Get(), helper)))
            {
                var font = new Font(
                    configuration.GetFamilyFont(),
                    wordStatistic.Count * configuration.GetSize()
                );
                var layoutRectangle = layouter.PutNextRectangle(
                    new Size((int)font.Size * wordStatistic.Word.Length, font.Height)
                );
                tags.Add(new Tag(wordStatistic.Word, configuration.GetColor(), font, layoutRectangle));
            }
        }

        public IEnumerable<Tag> Get() => tags;

        private static IEnumerable<string> ProcessWords(IEnumerable<string> words, IWordHelper helper)
            => helper.FilterWords(helper.ConvertWords(words));
    }
}