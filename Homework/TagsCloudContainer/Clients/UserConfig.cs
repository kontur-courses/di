using System.Drawing;

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
            TagsColor = (Brush)typeof(Brushes).GetProperty(options.Color).GetValue(null, null);
        }
    }
}
