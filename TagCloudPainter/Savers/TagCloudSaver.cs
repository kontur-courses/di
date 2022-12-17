using System.Drawing;
using System.Drawing.Imaging;
using TagCloudPainter.Builders;
using TagCloudPainter.FileReader;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;

namespace TagCloudPainter.Savers;

public class TagCloudSaver : ITagCloudSaver
{

    public void SaveTagCloud(Bitmap bitmap,string outputPath, ImageFormat format)
    {
        bitmap.Save(outputPath, format);
    }
}