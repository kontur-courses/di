using System.Collections.Generic;
using TagsCloudVisualization.TagsCloudDrawer;

namespace TagsCloudVisualization.ImageCreator
{
    public interface IImageCreator
    {
        void Create(string filename, IEnumerable<IDrawable> tags);
    }
}