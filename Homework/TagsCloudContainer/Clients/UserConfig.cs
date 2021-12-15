using System;
using System.Drawing;
using System.IO;

namespace TagsCloudContainer.Clients
{
    public class UserConfig
    {
        public string InputFile { get; }
        public string OutputFile { get; }
        public Size ImageSize { get; }
        public Point ImageCenter { get; }
        public string TagsFontName { get; }
        public int TagsFontSize { get; }
        public Brush TagsColor { get; }

        public UserConfig() { }

        public UserConfig(Options options)
        {
            InputFile = options.Input;
            OutputFile = options.Output;
            ImageSize = new Size(options.Width, options.Height);
            ImageCenter = new Point(ImageSize.Width / 2, ImageSize.Height / 2);
            TagsFontName = options.FontName;
            TagsFontSize = options.FontSize;
            ThrowIfAnyArgIsIncorrect();
            try
            {
                TagsColor = (Brush) typeof(Brushes).GetProperty(options.Color).GetValue(null, null);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException($"There is no such color \"{options.Color}\"");
            }
        }

        private void ThrowIfAnyArgIsIncorrect()
        {
            if (!File.Exists(InputFile))
                throw new ArgumentException("There is no such file!");
            if (ImageSize.Width <= 0 || ImageSize.Height <= 0)
                throw new ArgumentException("Image size is incorrect!");
            if (TagsFontSize <= 0)
                throw new ArgumentException("Font size is incorrect!");
            var font = new Font(TagsFontName, TagsFontSize);
            if (font.Name != TagsFontName)
                throw new ArgumentException("Font name is incorrect!");
        }
    }
}
