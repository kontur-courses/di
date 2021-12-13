using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public interface IVisualization : IDisposable
    {
        void DrawAndSaveImage(List<RectangleWithWord> elements, string savePath, ImageFormat format);
        List<RectangleWithWord> FindSizeForElements(Dictionary<string, RectangleWithWord> formedElements);
    }
}
