using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;

namespace TagsCloudContainer.Image;

public static class ImageEncodings
{
    public static (ImageEncoder encoding, string ext) Bmp => (new BmpEncoder(), "bmp");
    public static (ImageEncoder encoding, string ext) Gif => (new GifEncoder(), "gif");
    public static (ImageEncoder encoding, string ext) Jpg => (new JpegEncoder { Quality = 100 }, "jpg");
    public static (ImageEncoder encoding, string ext) Png => (new PngEncoder(), "png");
    public static (ImageEncoder encoding, string ext) Tiff => (new TiffEncoder(), "tiff");
}