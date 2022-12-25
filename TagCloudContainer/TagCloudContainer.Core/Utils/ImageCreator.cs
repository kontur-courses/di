using System.Drawing.Imaging;
using TagCloudContainer.Core.Interfaces;

namespace TagCloudContainer.Core.Utils;

public class ImageCreator : IImageCreator
{
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;
    
    public ImageCreator(ITagCloudContainerConfig tagCloudContainerConfig)
    {
        _tagCloudContainerConfig = 
            tagCloudContainerConfig ?? throw new ArgumentNullException("Tag cloud config can't be null");
    }
    
    public void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(_tagCloudContainerConfig.ImageSize.Width, _tagCloudContainerConfig.ImageSize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(
                    new Point(form.DesktopLocation.X, form.DesktopLocation.Y), 
                    new Point(0, 0), 
                    _tagCloudContainerConfig.ImageSize);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}