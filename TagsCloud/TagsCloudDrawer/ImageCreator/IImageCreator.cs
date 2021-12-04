using System.Collections.Generic;

namespace TagsCloudDrawer.ImageCreator
{
    public interface IImageCreator
    {
        void Create(string filename, IEnumerable<IDrawable> tags);
    }
}