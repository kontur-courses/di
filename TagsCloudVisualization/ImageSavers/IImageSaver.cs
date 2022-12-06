using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Abstractions;

public abstract class IImageSaver
{
    public void Save(string fullpath, Bitmap image)
    {
        
    }
    
    public abstract string Extension { get; }
    public abstract ImageFormat Format { get; }
}