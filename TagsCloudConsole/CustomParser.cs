using System;
using System.Drawing;
using System.Linq;
using CommandLine;

namespace TagsCloudConsole
{
    class CustomArgs
    {
        public readonly string WordsFile;
        public readonly Size ImageSize;
        public readonly Color BackgroundColor;
        public readonly Color TextColor;
        public readonly string OutputFileName;
        public readonly string ImageExtension;
        public readonly string FontName;

        public CustomArgs(CmdOptions options)
        {
            WordsFile = options.WordsFile;
            ImageSize = ParseImageSize(options.RawImageSize);
            BackgroundColor = ParseKnownColor(options.BackgroundColorName);
            TextColor = ParseKnownColor(options.TextColorName);
            OutputFileName = options.OutputFileName;
            ImageExtension = ExtractExtension(OutputFileName);
            FontName = options.FontName;
        }

        private Color ParseKnownColor(string colorName)
        {
            var color = Color.FromName(colorName);
            if (color.IsKnownColor)
                return color;
            throw new ArgumentException("Color is invalid");
        }

        private string ExtractExtension(string fileName)
        {
            if (!fileName.Contains('.'))
                throw new ArgumentException("Image has no extension");
            return fileName.Split('.').LastOrDefault();
        }

        private Size ParseImageSize(string imageSizeString)
        {
            var displayParts = imageSizeString.Split('x');
            if (displayParts.Length != 2)
                throw new ArgumentException("Image size has invalid format");

            int height, widht;
            var leftParsed = int.TryParse(displayParts[0], out height);
            var rightParsed = int.TryParse(displayParts[1], out widht);

            if (!(leftParsed && rightParsed))
                throw new ArgumentException("Image size has invalid format");
            return new Size(height, widht);
        }
    }

    class CmdOptions
    {
        [Option("filename", Required = true)]
        public string WordsFile { get; set; }

        [Option("size", Required = false, Default = "600x600")]
        public string RawImageSize { get; set; }

        [Option("font", Required = false, Default = "Arial")]
        public string FontName { get; set; }

        [Option("backcolor", Required = false, Default = "Black")]
        public string BackgroundColorName { get; set; }

        [Option("textcolor", Required = false, Default = "White")]
        public string TextColorName { get; set; }

        [Option("output", Required = true)]
        public string OutputFileName { get; set; }
    }

    class CustomParser
    { 
        public CustomArgs Parse(string[] args)
        {
            var anyErrors = false;
            CustomArgs customArgs = null;

            Parser.Default.ParseArguments<CmdOptions>(args)
                .WithParsed(opts => customArgs = new CustomArgs(opts))
                .WithNotParsed(err => anyErrors = true);

            if (anyErrors)
                throw new ArgumentException("");

            return customArgs;
        }
    }
}
