using System.Drawing;

namespace TagsCloudContainer.ImageCreators
{
    public class SingleColorChooser : IColorChooser
    {
        private readonly IPalette palette;

        public SingleColorChooser(IPalette palette)
        {
            this.palette = palette;
        }

        public Color GetNextColor()
        {
            return palette.FontColor;
        }
    }
}