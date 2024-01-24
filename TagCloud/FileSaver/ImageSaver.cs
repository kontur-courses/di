using System.Drawing;

namespace TagCloud.FileSaver;

public class ImageSaver : ISaver
{
    private List<string> supportedFormats = new() { "png", "jpg", "jpeg", "bmp", "gif" };
    
    public void Save(Bitmap bitmap, string outputPath, string imageFormat)
    {
        if (!supportedFormats.Contains(imageFormat))
            throw new ArgumentException($"{imageFormat} format is not supported");
        using (bitmap)
        {
            bitmap.Save($"{outputPath}.{imageFormat}");
        }
    }
}