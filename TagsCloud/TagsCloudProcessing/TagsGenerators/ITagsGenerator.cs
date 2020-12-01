using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing.TegsGenerators
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words, ILayouter layouter, Font font);
    }
}
