using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.ImageSavers;

public abstract class AbstractImageSaver
{
    public void Save(string fullpath, Bitmap image)
    {
        image.Save(Path.ChangeExtension(fullpath,Extension), Format);
    }

    protected abstract string Extension { get; }
    protected abstract ImageFormat Format { get; }
}