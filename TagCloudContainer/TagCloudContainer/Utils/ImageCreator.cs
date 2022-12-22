using System.Drawing.Imaging;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class ImageCreator : IImageCreator
{
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    
    public ImageCreator(ITagCloudFormConfig tagCloudFormConfig)
    {
        _tagCloudFormConfig = tagCloudFormConfig;
    }
    
    public void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(_tagCloudFormConfig.FormSize.Width, _tagCloudFormConfig.FormSize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(form.DesktopLocation.X, form.DesktopLocation.Y), new Point(0, 0), _tagCloudFormConfig.FormSize);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}