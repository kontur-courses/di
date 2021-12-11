using System.Collections.Generic;
using System.Drawing;
using App.Implementation.Words.Tags;

namespace App.Infrastructure.Visualization
{
    public interface IVisualizer
    {
        Bitmap VisualizeCloud(Bitmap bitmap, Point cloudCenter, IEnumerable<Tag> tags);

        void VisualizeDebuggingMarkupOnImage(Image image, Point cloudCenter, int cloudCircleRadius);
    }
}