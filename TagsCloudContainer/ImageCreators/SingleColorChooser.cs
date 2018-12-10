using System.Drawing;

namespace TagsCloudContainer.ImageCreators
{
    public class SingleColorChooser : IColorChooser
    {
        private readonly Palette palette;

        public SingleColorChooser(Palette palette)
        {
            this.palette = palette;
        }

        public Color GetNextColor()
        {
            return palette.FontColor;
        }
    }
}