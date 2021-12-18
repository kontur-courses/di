using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Client;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace CLI
{
    public class CommandLineConfig : IUserConfig
    {
        public string InputFile { get; set; }
        public string[] Tags { get; set; }
        public string OutputFile { get; set; }
        public Size ImageSize { get; set; }
        public Point ImageCenter { get; set; }
        public string TagsFontName { get; set; }
        public int TagsFontSize { get; set; }
        public IColorScheme TagsColors { get; set; }
        public ISpiral Spiral { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public ITextParser TextParser { get; set; }
    }
}
