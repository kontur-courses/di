using TagsCloudContainer.Vizualization;

namespace TagsCloudContainer.Settings_Providing
{
    public class Settings
    {
        public readonly string inputPath;
        public readonly string outputPath;
        public readonly ColoringOptions ColoringOptions;

        public Settings(string inputPath, string outputPath, ColoringOptions coloringOptions)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            ColoringOptions = coloringOptions;
        }
    }
}