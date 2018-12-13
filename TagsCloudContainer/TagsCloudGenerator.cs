using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloudGenerator
    {
        private readonly Size minLetterSize;
        private readonly ITagsCloudLayouter layouter;
        private readonly IWordsSizer wordsSizer;

        public TagsCloudGenerator(TagsCloudGeneratorSettings settings)
        {
            minLetterSize = settings.LetterSize;
            layouter = settings.TagsCloudLayouter;
            wordsSizer = settings.WordsSizer;
        }

        public ITagsCloud CreateCloud(List<string> words)
        {
            var wordsSizes = wordsSizer.GetWordsSizes(words, minLetterSize);

            foreach (var pair in wordsSizes)
            {
                var rectangle = layouter.PutNextRectangle(pair.Value);
                layouter.TagsCloud.AddWord(new TagsCloudWord(pair.Key, rectangle));
            }

            return layouter.TagsCloud;
        }
    }
}