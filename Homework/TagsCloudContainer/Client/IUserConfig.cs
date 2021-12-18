using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.Client
{
    public interface IUserConfig
    {
        string InputFile { get; set; }
        string[] Tags { get; set; }
        string OutputFile { get; set; }
        Size ImageSize { get; set; }
        Point ImageCenter { get; set; }
        string TagsFontName { get; set; }
        int TagsFontSize { get; set; }
        IColorScheme TagsColors { get;  set; }
        ISpiral Spiral { get; set; }
        ImageFormat ImageFormat { get;  set; }
        ITextParser TextParser { get; set; }
    }
}
