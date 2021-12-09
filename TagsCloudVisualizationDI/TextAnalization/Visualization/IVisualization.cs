using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudVisualizationDI.Visualization
{
    public interface IVisualization: IDisposable
    {
        void DrawAndSaveImage();
        Size GetStringSize(RectangleWithWord word);
    }
}
