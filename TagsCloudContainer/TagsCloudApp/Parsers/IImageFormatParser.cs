using System.Drawing.Imaging;

namespace TagsCloudApp.Parsers
{
    public interface IImageFormatParser
    {
        ImageFormat Parse(string value);
    }
}