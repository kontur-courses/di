using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Mono.Options;
using TagCloud.Layouters;
using TagCloud.Settings;

namespace TagCloud.UserInterfaces
{
    public class ConsoleUserInterface : IUserInterface
    {
        public DrawerSettings DrawerSettings { get; private set; }
        public CircularLayouterSettings CircularLayouterSettings { get; private set; }
        public FileReaderSettings FileReaderSettings { get; private set; }
        public LayoutSettings LayoutSettings { get; private set; }
        public SaverSettings SaverSettings { get; private set; }

        public ConsoleUserInterface(string[] args)
        {
            ParseFileReaderSettingsArgs(args);
            ParseDrawerSettingsArgs(args);
            ParseSaverSettingsArgs(args);
            ParseLayoutSettingsArgs(args);
            ParseLayouterSettingsArgs(args);
        }

        private void ParseFileReaderSettingsArgs(string[] args)
        {
            string filePath = null;
            var options = new OptionSet 
            {
                "File reader settings args:",
                { "file=", "Source file with text for the tag cloud",
                    v => filePath = v },
            };
            options.Parse(args);
            if (filePath == null)
                throw new ArgumentException("Required argument missed: file");
            FileReaderSettings = new FileReaderSettings(filePath);
        }

        private void ParseLayouterSettingsArgs(string[] args)
        {
            var center = DrawerSettings != null 
                ? new Point(
                    DrawerSettings.ImageSize.Width / 2,
                    DrawerSettings.ImageSize.Height / 2)
                : new Point(1000, 1000);
            var spiralPitch = 4;
            var spiralStep = 0.005;
            
            var options = new OptionSet {
                "Drawer settings args:",
                { "pitch=", "Spiral pitch",
                    v => spiralPitch = Convert.ToInt32(v) },
                { "step=", "Spiral step",
                    v => spiralStep = Convert.ToDouble(v) },
                { "center=", $"Cloud center. Default: {center.X}x{center.Y}",
                    v =>
                    {
                        var argAsIntArray = GetIntArrayFromString(v, 2);
                        
                        center = new Point(argAsIntArray[0], argAsIntArray[1]);
                    }
                }
            };
            options.Parse(args);
            CircularLayouterSettings = new CircularLayouterSettings(center, spiralPitch, spiralStep);
        }

        private void ParseDrawerSettingsArgs(string[] args)
        {
            var imageSize = new Size(2000, 2000);
            var backgroundColor = Color.Black;
            var options = new OptionSet {
                "Drawer settings args:",
                { "bg|background=", "Background color in \"255 255 255\" format. Default: Black",
                    v => backgroundColor = ParseColorFromRGBString(v) },
                { "imsize=", $"Image size. Default: {imageSize.Width}x{imageSize.Height}",
                    v =>
                    {
                        var argAsIntArray = GetIntArrayFromString(v, 2);
                        
                        imageSize = new Size(argAsIntArray[0], argAsIntArray[1]);
                    }
                }
            };
            options.Parse(args);
            DrawerSettings = new DrawerSettings(imageSize, backgroundColor);
        }

        private void ParseLayoutSettingsArgs(string[] args)
        {
            var fontFamily = "Times New Roman";
            var minFontSize = 14;
            var maxFontSize = 48;
            
            var options = new OptionSet {
                "Layout settings args:",
                { "font=", $"Tag cloud font. Default: {fontFamily}",
                    v => fontFamily = v },
                { "fontsizes=", $"Font sizes. Default: {minFontSize}-{maxFontSize}",
                    v =>
                    {
                        var argAsIntArray = GetIntArrayFromString(v, 2);
                        
                        minFontSize = argAsIntArray[0];
                        maxFontSize = argAsIntArray[1];
                        if (minFontSize > maxFontSize)
                            throw new ArgumentException("Minsize must be less or equal to maxsize");
                    }
                }
            };
            options.Parse(args);
            LayoutSettings = new LayoutSettings(fontFamily, minFontSize, maxFontSize);
        }

        private void ParseSaverSettingsArgs(string[] args)
        {
            var outputFile = "output.png";
            string outputPath = null;
            var options = new OptionSet {
                "Image saver args:",
                { "out=", $"Output file, default: {outputFile}",
                    v => outputFile = v },
                { "outPath=", "Output file path, default: CWD",
                    v => outputPath = v }
            };
            options.Parse(args);
            SaverSettings = new SaverSettings(outputPath, outputFile);
        }

        private int[] GetIntArrayFromString(string arrayAsStr, int requiredLength = 0, char separator = ' ')
        {
            var array = arrayAsStr.Split(separator).Select(v => Convert.ToInt32(v)).ToArray();
            if (requiredLength > 0 && array.Length != requiredLength)
                throw new ArgumentException();
            return array;
        }

        private Color ParseColorFromRGBString(string colorAsString)
        {
            var rgbAsArray = colorAsString.Split()
                .Select(s => Convert.ToInt32(s)).ToArray();
            if (rgbAsArray.Length != 3 || rgbAsArray.All(n => n < 0 || n > 255))
                throw new ArgumentException("Incorrect Color format");
            return Color.FromArgb(rgbAsArray[0], rgbAsArray[1], rgbAsArray[2]);
        }
    }
}