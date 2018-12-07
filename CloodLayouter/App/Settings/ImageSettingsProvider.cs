using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSettingsProvider : IImageSettingsProvider
    {
        public Size ImageSize { get; set; } = new Size(500,500);
    }
}