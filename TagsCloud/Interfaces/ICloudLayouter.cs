using System.Collections.Generic;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
        IReadOnlyCollection<Rectangle> GetCloud();
    }
}