using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class BitmapViewerToForm:IBitmapViewer
    {
        public void View(Bitmap bitmap)
        {
            bitmap.ToForm();
        }
    }
    internal static class BitmapExtensions
    {
        public static void ToForm(this Bitmap bitmap)
        {
            Form aForm = new Form();
            aForm.Width = bitmap.Width;
            aForm.Height = bitmap.Height;
            aForm.BackgroundImage = bitmap;
            aForm.ShowDialog();
        } 
    }
}