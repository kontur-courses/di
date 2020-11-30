using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.ColoringAlgorithms;

namespace TagCloud.Core.ImageCreators
{
    public interface IImageCreator
    {
        public Bitmap Create(IColoringAlgorithm algorithm, IEnumerable<Tag> tags,
            string fontName, Size size);
    }
}