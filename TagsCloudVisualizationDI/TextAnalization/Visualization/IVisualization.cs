using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudVisualizationDI.Visualization
{
    public interface IVisualization: IDisposable
    {
        void DrawAndSaveImage();
    }
}
