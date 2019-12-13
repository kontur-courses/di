using System.Drawing;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.UI;

namespace TagsCloudVisualization.Settings
{
    public class AppSettings
    {
        public string CurrentFile;
        public IVisualizer CurrentInterface;
        public Font Font = new Font(FontFamily.GenericSansSerif, 1);
        public PictureBoxImageHolder ImageHolder;
        public ImageSettings ImageSettings;
        public Palette Palette;

        public AppSettings(ImageSettings imageSettings, PictureBoxImageHolder imageHolder, Palette palette)
        {
            ImageSettings = imageSettings;
            ImageHolder = imageHolder;
            Palette = palette;
        }
    }
}