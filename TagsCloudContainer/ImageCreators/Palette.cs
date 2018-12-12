using System.Drawing;

namespace TagsCloudContainer.ImageCreators
{
    public class Palette
    {
        public Color FontColor;
        public Color BackGroundColor;

        public Palette(Color fontColor, Color backGroundColor)
        {
            FontColor = fontColor;
            BackGroundColor = backGroundColor;
        }
    }
}