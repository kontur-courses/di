using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloud
    {
        private readonly FileReader fileReader;
        private readonly TokenGenerator tokenGenerator;
        private readonly TagCloudMaker tagCloudMaker;
        private readonly TagCloudVisualiser tagCloudVisualiser;

        public TagCloud(FileReader fileReader, TokenGenerator tokenGenerator,
            TagCloudMaker tagCloudMaker, TagCloudVisualiser tagCloudVisualiser)
        {
            this.fileReader = fileReader;
            this.tagCloudMaker = tagCloudMaker;
            this.tagCloudVisualiser = tagCloudVisualiser;
            this.tokenGenerator = tokenGenerator;
        }

        public void CreateTagCloudFromFile(string sourcePath, string resultPath, Font font,
            Color background, int maxTagCount, Size resolution)
        {
            var source = ParseSource(sourcePath);
            if (!fileReader.CanReadFile(source))
                throw new ArgumentException("Unknown source file format.");
            var text = fileReader.ReadFile(source);
            var tokens = tokenGenerator.GetTokens(text, maxTagCount).ToArray();
            var tags = tagCloudMaker.CreateTagCloud(tokens, font);
            if (tags.Length == 0)
                throw new ArgumentException("Zero tags found.");
            if (resolution.Height < 1 || resolution.Width < 1)
                throw new ArgumentException("Resolution must be positive");
            var image = tagCloudVisualiser.Render(tags, resolution, background);
            var format = ParseImageFormat(resultPath); 
            image.Save(resultPath, format);
        }

        public void CreateTagCloudFromFile(string sourcePath, string resultPath, string fontName,
            string background, int maxTagCount, int width, int height)
        {
            var resolution = ParseResolution(width, height);
            var font = ParseFont(fontName);
            var back = ParseColor(background);
            CreateTagCloudFromFile(sourcePath, resultPath, font, back, maxTagCount, resolution);
        }
        
        public static Font ParseFont(string fontName)
        {
            var font = new Font(fontName, 10);
            if (font.Name != fontName)
                throw new ArgumentException("Unknown Font " + fontName);
            return font;
        }

        public static Color ParseColor(string colorName)
        {
            if (!Enum.TryParse(colorName, out KnownColor color))
                throw new ArgumentException("Unknown color " + colorName);
            return Color.FromKnownColor(color);
        }

        public static FileInfo ParseSource(string sourcePath)
        {
            var source = new FileInfo(sourcePath);
            if (!source.Exists)
                throw new ArgumentException("Source file not found");
            return source;
        }

        public static Size ParseResolution(int width, int height)
        {
            if (height < 1 || width < 1)
                throw new ArgumentException("Resolution must be positive");
            return new Size(width, height);
        }
        
        public static ImageFormat ParseImageFormat(string path)
        {
            var extension = Path.GetExtension(path).TrimStart('.');
            extension = extension.ToLower();
            if (extension == "jpg")
                return ImageFormat.Jpeg;
            extension = extension[0].ToString().ToUpper() + extension[1..];
            var type = typeof(ImageFormat);
            var prop = type.GetProperty(extension);
            if (prop is null || prop.PropertyType != type)
                throw new ArgumentException("Unknown image format");
            return (ImageFormat)prop.GetValue(new object());
        }
    }
}