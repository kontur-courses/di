using System.Collections.Generic;
using TagCloud.Layouter;

namespace TagCloud.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
        IReadOnlyCollection<Rectangle> GetCloud();
    }
}