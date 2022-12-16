using System.Drawing.Imaging;

namespace TagCloudContainer;

public class ImageCreator
{
    public static void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(MainFormConfig.FormSize.Width, MainFormConfig.FormSize.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(form.DesktopLocation.X, form.DesktopLocation.Y), new Point(0, 0), MainFormConfig.FormSize);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}