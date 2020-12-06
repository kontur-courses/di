using System.Drawing;
using TagsCloudLayouters.Contracts;
using TagsCloudLayouters.Core.ColorizeAlgorithms;

namespace TagsCloudLayouters.Configuration
{
    public class TagsSettings
    {
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public IColorizer Colorizer { get; set; } = new RandomColorizer();
    }
}