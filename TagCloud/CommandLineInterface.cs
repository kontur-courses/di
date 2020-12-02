using System;
using System.Drawing;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace TagCloud
{
    public class CommandLineInterface
    {
        private IPathCreater creater;
        public Color StringColor { get; private set; }
        public FontFamily StringFont { get; private set; }
        public string FileName { get; private set; }
        public Size CanvasSize { get; private set; }
        public Background BackgroundType { get; private set; }

        public CommandLineInterface(IPathCreater creater)
        {
            this.creater = creater;
        }

        public void ConfigureCLI(CommandLineApplication app)
        {
            app.HelpOption();
            var optionInput = app.Option("-i|--input <INPUT>", "input filename", CommandOptionType.SingleValue);
            var optionFont = app.Option("-f|--font <FONT>", "font family", CommandOptionType.SingleValue);
            var optionSize = app.Option("-s|--size <SIZE>", "size of image width,height", CommandOptionType.SingleValue);
            var optionBackground = app.Option("-b|--backgound <BACKGROUND_STYLE>", "background style rectangles|empty|circle", CommandOptionType.SingleValue);
            var optionStringColor = app.Option("-c|--color <COLOR>", "string color r,g,b", CommandOptionType.SingleValue);


            app.OnExecute(() =>
            {
                CanvasSize = optionSize.HasValue() ? GetSize(optionSize.Value()) : new Size(1000, 800);
                BackgroundType = optionBackground.HasValue() ? GetBackground(optionBackground.Value()) : Background.Empty;
                FileName = optionInput.HasValue() ? CheckFileName(optionInput.Value()) : "input.txt";
                StringFont = optionFont.HasValue() ? GetFont(optionFont.Value()) : new FontFamily("Arial");
                StringColor = optionStringColor.HasValue() ? ParseColor(optionStringColor.Value()) : Color.Black;

                return 0;
            });
        }

        private static Background GetBackground(string background)
        {
            switch (background)
            {
                case "empty":
                    return Background.Empty;
                case "circle":
                    return Background.Circle;
                case "rectangles":
                    return Background.Rectangles;
                default:
                    throw new ArgumentException("Unknown background type");
            }
        }
        
        private static Size GetSize(string size)
        {
            var arr = size.Split(',');
            if (arr.Length == 2 && int.TryParse(arr[0], out var width) && int.TryParse(arr[1], out var height))
            {
                return new Size(width, height);
            }
            
            throw new ArgumentException("Incorrect size argument");
        }

        private string CheckFileName(string fileName)
        {
            if (!File.Exists(creater.GetPathToFile(fileName)))
            {
                throw new ArgumentException("Input file not found");
            }
            return fileName;
        }

        private static FontFamily GetFont(string font)
        {
            try
            {
                return new FontFamily(font);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unknown FontFamily");
            }
        }
        
        private static Color ParseColor(string colorInRGB)
        {
            var arr = colorInRGB.Split(',');
            if (arr.Length == 3
                && TryGetColorComponent(arr[0], out var red)
                && TryGetColorComponent(arr[1], out var green)
                && TryGetColorComponent(arr[1], out var blue))
            {
                return Color.FromArgb(red, green, blue);
            }
            throw new ArgumentException($"Uncorrect color format. Given: {colorInRGB}. Expected: 0-255,0-255,0-255");
        }
        
        private static bool TryGetColorComponent(string colorComponent, out int value)
        {
            if (int.TryParse(colorComponent, out var intColorComponent))
            {
                value = intColorComponent;
                return intColorComponent >= 0 && intColorComponent <= 255;
            }

            value = 0;
            return false;
        }
    }
}