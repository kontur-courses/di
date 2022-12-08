using System.Drawing;

namespace TagsCloudContainer
{
    public class MyConfigValidator
    {
        public static void ValidateConfig(MyConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));
            if (!Directory.Exists(configuration.TextsPath))
                throw new ArgumentException("Texts directory does not exist");
            if (!File.Exists(Path.Combine(configuration.TextsPath, configuration.WordsFileName)))
                throw new ArgumentException("Tag file does not exist");
            if (!File.Exists(Path.Combine(configuration.TextsPath, configuration.BoringWordsName)))
                throw new ArgumentException("Exclude words file does not exist");
            var color = Color.FromName(configuration.BackgroundColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Invalid backgroud color");
            color = Color.FromName(configuration.FontColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Invalid font color");
            if (configuration.FontSize < 1)
                throw new ArgumentException("Font size should be above 0");
            if (configuration.PictureSize < 1)
                throw new ArgumentException("Picture size should be above 0");
            var font = new Font(configuration.Font, 1);
            if (font.Name != configuration.Font)
                throw new ArgumentException($"Font \"{configuration.Font}\" can't be found");
        }
    }
}