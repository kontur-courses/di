using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public class Palette
    {
        public bool IsGradient { get; set; } = false;
        public Color PrimaryColor { get; set; } = Color.Red;
        public Color SecondaryColor { get; set; } = Color.Plum;
        public Color BackgroundColor { get; set; } = Color.DarkBlue;
    }
}