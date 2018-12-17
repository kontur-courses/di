using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.TagsCloudGenerating
{
    public class TagsCloudGenerator
    {
        private readonly ITagsCloudLayouter layouter;
        private readonly IWordsSizer wordsSizer;

        public TagsCloudGenerator(IWordsSizer wordsSizer, ITagsCloudLayouter layouter)
        {
            this.layouter = layouter;
            this.wordsSizer = wordsSizer;
        }

        public ITagsCloud CreateCloud(List<string> words, Size minLetterSize)
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