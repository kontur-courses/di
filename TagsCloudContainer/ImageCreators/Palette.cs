using System.Drawing;

namespace TagsCloudContainer.ImageCreators
{
    public class Palette
    {
        public Color FontColor;
        public Color BackGroundColor;
        public Color SecondColor;

        public Palette(Color fontColor, Color backGroundColor, Color secondColor)
        {
            FontColor = fontColor;
            BackGroundColor = backGroundColor;
            SecondColor = secondColor;
        }
    }
}