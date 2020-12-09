using CommandLine;

namespace TagCloud.Visualizer
{
    public class ImageOptions
    {
        [Option('s', "fontSize")] public float FontSize { get; set; }

        [Option('n', "fontName")] public string FontName { get; set; }

        [Option('h', "height")] public int ImageHeight { get; set; }

        [Option('w', "width")] public int ImageWidth { get; set; }

        [Option('c', "color")] public string ColorName { get; set; }

        [Option('e', "imageExt")] public string ImageExtension { get; set; }

        public ImageOptions()
        {
            FontSize = 12f;
            FontName = "Times New Roman";
            ImageHeight = 1000;
            ImageWidth = 1000;
            ColorName = "Black";
            ImageExtension = "png";
        }

        public int ChangeImageOptionsAndReturnExitCode(ImageOptions opts)
        {
            FontSize = opts.FontSize;
            FontName = opts.FontName;
            ImageHeight = opts.ImageHeight;
            ImageWidth = opts.ImageWidth;
            ColorName = opts.ColorName;
            ImageExtension = opts.ImageExtension;
            return 0;
        }
    }
}