using System.Drawing;
using TagsCloud.Core.Layouters;
using TagsCloud.Core.TagContainersCreators.TagsPreproessors;

namespace TagsCloud.Core.TagContainersCreators;

public class TempCreator : ITagContainersCreator
{
    private ICloudLayouter layouter;
    private ITagsPreprocessor preprocessor;

    public TempCreator(ICloudLayouter layouter, ITagsPreprocessor preprocessor)
    {
        this.layouter = layouter;
        this.preprocessor = preprocessor;
    }

    public IEnumerable<TagContainer> Create(int? count)
    {
        var minLetterSize = new Size(10, 10);
        var tags = preprocessor.GetTags(count).ToList();
        var containers = new List<TagContainer>();

        var tagsSum = tags.Sum(t => t.Count);

        foreach (var tag in tags)
        {
            var multiplier = 1 + (double)tagsSum / tag.Count;
            var tagHeight = (int)(minLetterSize.Height * multiplier);
            var tagWidth = (int)(tag.Word.Length * minLetterSize.Width * multiplier);

            containers.Add(new TagContainer(tag, layouter.PutNextRectangle(new Size(tagWidth, tagHeight))));
        }

        return containers;
    }
}