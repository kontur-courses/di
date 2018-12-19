using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageHolder : IProvider<Bitmap>
    {
        public ImageHolder(IProvider<ImageSettings> imageSettingsProvider)
        {
            var imageSettings = imageSettingsProvider.Get();
            bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
        }

        private Bitmap bitmap;
        public Bitmap Get()
        {
            return bitmap;
        }
    }
}