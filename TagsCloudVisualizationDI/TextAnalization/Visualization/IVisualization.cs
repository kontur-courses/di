using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public interface IVisualization: IDisposable
    {
        //void DrawAndSaveImage();

        void DrawAndSaveImage(string savePath, ImageFormat format);

        //Size GetStringSize(RectangleWithWord word);
    }
}
