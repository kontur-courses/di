using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.TextRenderers;

namespace TagsCloudVisualization.Settings
{
    public class ImageSettings
    {
        public Size ImageSize { get; private set; }
        public string ImageName { get; private set; }
        public string ImageExtention { get; private set; }
        public int MinimalTextSize { get; private set; }
        public string Font { get; private set; }
        public List<Color> Colors { get; private set; }
        public ITextRenderer TextRenderer { get; private set; }

        public ImageSettings(Size imageSize, string imageName, string imageExtention, int minimalTextSize, string font, 
            string colors, ITextRenderer textRenderer)
        {
            ImageSize = imageSize;
            ImageName = imageName;
            ImageExtention = imageExtention;
            MinimalTextSize = minimalTextSize;
            Font = font;
            TextRenderer = textRenderer;
            Colors = colors.Split().Select(Color.FromName).ToList();
        }
    }
}
