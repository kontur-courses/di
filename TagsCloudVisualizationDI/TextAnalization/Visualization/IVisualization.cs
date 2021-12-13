using System;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public interface IVisualization: IDisposable
    { 
        void DrawAndSaveImage(string savePath, ImageFormat format);
    }
}
