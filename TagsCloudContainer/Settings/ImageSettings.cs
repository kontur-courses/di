using System.Drawing;
using TagsCloudContainer.Themes;

namespace TagsCloudContainer.Settings
{
    public class ImageSettings
    {
        public ImageSettings(int height, int width, string outputFile, string theme)
        {
            OutputFile = outputFile;
            Theme = GetThemeByName(theme);
            Height = height;
            Width = width;
            Center = new Point(height / 2, width / 2);
        }

        public int Height { get; }
        public int Width { get; }
        public Point Center { get; }
        public string OutputFile { get; }
        public ITheme Theme { get; }


        private ITheme GetThemeByName(string theme)
        {
            return Themes.Themes.ThemesDictionary[theme];
        }
    }
}