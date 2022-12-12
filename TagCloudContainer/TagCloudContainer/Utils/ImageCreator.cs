using System.Drawing.Imaging;

namespace TagCloudContainer;

public class ImageCreator
{
    public static void Save(Form form, string path)
    {
        using (Bitmap bitmap = new Bitmap(form.Size.Width, form.Size.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(form.DesktopLocation.X, form.DesktopLocation.Y), new Point(0, 0), form.Size);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}