using System.Drawing;
using TagsCloud.Entities;

namespace TagsCloud.Layouters;

public interface ILayouter
{
    public List<Tag> Tags { get; set; }
    public void AddTag(Tag tag);
}