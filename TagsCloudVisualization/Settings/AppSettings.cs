using System.Drawing;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Painters;

namespace TagsCloudVisualization.Settings
{
    public class AppSettings
    {
        public ImageSettings ImageSettings;
        public PictureBoxImageHolder ImageHolder;
        public Palette Palette;
        public Font Font = new Font(FontFamily.GenericSansSerif, 1);
        public ImageFormat ImageFormat;
        public string LastOpenedFile;

        public AppSettings(ImageSettings imageSettings, PictureBoxImageHolder imageHolder, Palette palette)
        {
            ImageSettings = imageSettings;
            ImageHolder = imageHolder;
            Palette = palette;
        }

        public AppSettings(ImageSettings imageSettings, PictureBoxImageHolder imageHolder, Palette palette, Font font)
        {
            ImageSettings = imageSettings;
            ImageHolder = imageHolder;
            Palette = palette;
            Font = font;
        }
    }
}