using System.Collections.Generic;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.TagToDrawableTransformer
{
    public interface ITagToDrawableTransformer
    {
        public IEnumerable<IDrawable> Transform(List<Tag> tags);
    }
}