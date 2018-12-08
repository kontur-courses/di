using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageHolder : IImageHolder
    {
        public ImageHolder(IImageSettingsProvider imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height);
        }

        public Bitmap Image { get; set; }
    }
}