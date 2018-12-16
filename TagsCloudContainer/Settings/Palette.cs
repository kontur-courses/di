using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ImageCreators
{
    public class Palette : IPalette
    {
        public Color FontColor { get; set; }
        public Color BackGroundColor { get; set; }

        public Palette()
        {
            FontColor = Color.Aqua;
            BackGroundColor = Color.Black;
        }
    }
}