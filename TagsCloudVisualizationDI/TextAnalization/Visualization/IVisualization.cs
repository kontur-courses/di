using System;
using System.Drawing;

namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public interface IVisualization: IDisposable
    {
        void DrawAndSaveImage();
        Size GetStringSize(RectangleWithWord word);
    }
}
