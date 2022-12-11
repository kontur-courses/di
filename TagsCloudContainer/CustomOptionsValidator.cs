using System.Drawing;

namespace TagsCloudContainer
{
    public class CustomOptionsValidator
    {
        public static void ValidateOptions(CustomOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            if (!Directory.Exists(options.WorkingDir))
                throw new ArgumentException("Texts directory does not exist");
            if (!File.Exists(Path.Combine(options.WorkingDir, options.WordsFileName)))
                throw new ArgumentException("Tag file does not exist");
            if (!File.Exists(Path.Combine(options.WorkingDir, options.BoringWordsName)))
                throw new ArgumentException("Exclude words file does not exist");
            var color = Color.FromName(options.BackgroundColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Invalid backgroud color");
            color = Color.FromName(options.FontColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Invalid font color");
            if (options.MinTagSize < 1)
                throw new ArgumentException("Font size should be above 0");
            if (options.PictureSize < 1)
                throw new ArgumentException("Picture size should be above 0");
            if (options.MaxTagSize >= options.PictureSize)
                throw new ArgumentException("Font size should be less than picture size");
            var font = new Font(options.Font, 1);
            if (font.Name != options.Font)
                throw new ArgumentException($"Font \"{options.Font}\" can't be found");
        }
    }
}