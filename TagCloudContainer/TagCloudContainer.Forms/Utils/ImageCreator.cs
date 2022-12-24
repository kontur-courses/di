using System.Drawing.Imaging;
using TagCloudContainer.Core.Interfaces;

namespace TagCloudContainer;

public class ImageCreator : IImageCreator
{
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    
    public ImageCreator(ITagCloudFormConfig tagCloudFormConfig)
    {
        if (tagCloudFormConfig == null)
            throw new ArgumentException("Tag cloud form config can't be null");
        
        _tagCloudFormConfig = tagCloudFormConfig;
    }
    
    public void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(_tagCloudFormConfig.ImageSize.Width, _tagCloudFormConfig.ImageSize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(form.DesktopLocation.X, form.DesktopLocation.Y), new Point(0, 0), _tagCloudFormConfig.ImageSize);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}