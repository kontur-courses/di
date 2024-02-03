using System.Drawing;
using TagsCloud.Entities;

namespace TagsCloud.Layouters;

public interface ILayouter
{
    public IEnumerable<Tag> GetTagsCollection();
    public void CreateTagCloud(Dictionary<string, int> tagsDictionary);

    public Size GetImageSize();
}