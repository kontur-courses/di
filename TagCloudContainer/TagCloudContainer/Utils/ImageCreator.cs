using System.Drawing.Imaging;

namespace TagCloudContainer;

public class ImageCreator : IImageCreator
{
    private readonly IMainFormConfig _mainFormConfig;

    public ImageCreator(IMainFormConfig mainFormConfig)
    {
        _mainFormConfig = mainFormConfig;
    }
    
    public void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(_mainFormConfig.FormSize.Width, _mainFormConfig.FormSize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(form.DesktopLocation.X, form.DesktopLocation.Y), new Point(0, 0), _mainFormConfig.FormSize);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}