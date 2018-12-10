using TagsCloudContainer.Layouter;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public class Settings
    {
        public AppSettings AppSettings { get; }
        public FontSettings FontSettings { get; }
        public ImageSettings ImageSettings { get; }
        public Palette Palette { get; }
        public WordsPreprocessorSettings WordsPreprocessorSettings { get; }
        public SpiralSettings SpiralSettings { get; }
    }
}