using System;
using System.Drawing;

namespace TagsCloudDrawer.ImageSettings
{
    public class ImageSettingsProvider : IImageSettingsProvider
    {
        private readonly Size _imageSize = new(800, 600);
        public Color BackgroundColor { get; init; } = Color.Gray;

        public Size ImageSize
        {
            get => _imageSize;
            init
            {
                if (value.Width <= 0) throw new ArgumentException("Expected width to be positive");
                if (value.Height <= 0) throw new ArgumentException("Expected height to be positive");
                _imageSize = value;
            }
        }
    }
}