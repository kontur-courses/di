using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.Clients
{
    public class UserConfig
    {
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }
        public Size ImageSize { get; private set; }
        public Point ImageCenter { get; private set; }
        public string TagsFontName { get; private set; }
        public int TagsFontSize { get; private set; }
        public ITextFormatReader FormatReader { get; private set; }
        public IColorScheme TagsColors { get; private set; }
        public ISpiral Spiral { get; private set; }
        public ImageFormat ImageFormat { get; private set; }

        public BoringWords ExcludedWords { get; private set; }

        public UserConfig()
        {
            ExcludedWords = new BoringWords();
        }

        public void GetConfig(Options options)
        {
            InputFile = options.Input;
            OutputFile = options.Output;
            ImageSize = new Size(options.Width, options.Height);
            ImageCenter = new Point(ImageSize.Width / 2, ImageSize.Height / 2);
            TagsFontName = options.FontName;
            TagsFontSize = options.FontSize;
            ImageFormat = GetImageFormat(options.OutputFileFormat);
            FormatReader = GetTextFormatReader(options.InputFileFormat);
            AddExcludedWords(options.ExcludedWords);
            TagsColors = TryGetTagsColors(options);
            ThrowIfAnyArgIsIncorrect();
            Spiral = GetSpiral(options.Spiral);
        }

        private ISpiral GetSpiral(string spiralName)
        {
            spiralName = spiralName.ToLower();
            switch (spiralName)
            {
                case "log": return new LogarithmSpiral(ImageCenter);
                case "sqr": return new SquareSpiral(ImageCenter);
                default: throw new ArgumentException("Unknown spiral kind!");
            }
        }

        private ImageFormat GetImageFormat(string formatName)
        {
            formatName = formatName.ToLower();
            switch (formatName)
            {
                case "png": return ImageFormat.Png;
                case "bmp": return ImageFormat.Bmp;
                case "jpeg": return ImageFormat.Jpeg;
                default: throw new ArgumentException("Uknown output image format!");
            }
        }

        private ITextFormatReader GetTextFormatReader(string formatName)
        {
            if (formatName.ToLower() != "txt")
                throw new ArgumentException("Unknown input file format!");
            return new TxtReader();
        }

        private void AddExcludedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                ExcludedWords.AddWord(word.ToLower());
        }

        private IColorScheme TryGetTagsColors(Options options)
        {
            switch (options.Color)
            {
                case 0: return new BlackWhiteScheme();
                case 1: return new CamouflageScheme();
                case 2: return new CyberpunkScheme();
                default: throw new ArgumentException("Unknow color scheme number!");
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
