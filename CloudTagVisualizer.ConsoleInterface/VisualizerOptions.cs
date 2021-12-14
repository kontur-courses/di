using System;
using System.Drawing;
using CommandLine;
using Visualization.VisualizerProcessorFactory;

namespace CloudTagVisualizer.ConsoleInterface
{
    [Verb("visualize")]
    public class VisualizerOptions
    {
        [Option("textToVisualize", Required = true, HelpText = "Set path to input txt file")]
        public string PathToFileWithWords { get; set; }
        
        [Option("textFormat", Default = "txt", HelpText = "Set format for input file")]
        public string InputTextFormat { get; set; }

        [Option("pathToSaveImage", Required = true, HelpText = "Set path where image will be saved")]
        public string PathToSaveImage { get; set; }
        
        [Option("imageFormat", Default = "png", HelpText = "Set format for image saving")]
        public string ImageFormat { get; set; }

        [Option("backgroundColor", Default = "2F2F2F", HelpText = "Set background color")]
        public string BackgroundColorArgb { get; set; }

        [Option("textColor", Default = "D2F898", HelpText = "Set text color")]
        public string TextColorArgb { get; set; }

        [Option("strokeColor", Default = "0000FF", HelpText = "Set text stroke color")]
        public string StrokeColorArgb { get; set; }

        [Option("width", Default = 1920, HelpText = "Set image width")]

        public int ImageWidth { get; set; }
        
        [Option("height", Default = 1080, HelpText = "Set image height")]
        public int ImageHeight { get; set; }
        
        [Option("font", Default = "Arial")]
        public string FontName { get; set; }
        
        [Option("fontSize", Default = 240)]
        public int FontSize { get; set; }

        public Font Font => new(FontName, FontSize);
        public Size ImageSize => new(ImageWidth, ImageHeight);
        public Color BackgroundColor => ColorFromHex(BackgroundColorArgb);
        public Color TextColor => ColorFromHex(TextColorArgb);
        public Color StrokeColor => ColorFromHex(StrokeColorArgb);

        public SavingFormat SavingFormat
        {
            get
            {
                Enum.TryParse(ImageFormat, true, out SavingFormat res);
                return res;
            }
        }
        
        public InputFileFormat InputFileFormat
        {
            get
            {
                Enum.TryParse(InputTextFormat, true, out InputFileFormat res);
                return res;
            }
        }
        
        private static Color ColorFromHex(string hexed)
        {
            var red = Convert.ToInt32(hexed[0..2], 16);
            var green = Convert.ToInt32(hexed[2..4], 16);
            var blue = Convert.ToInt32(hexed[4..6], 16);
            return Color.FromArgb(red, green, blue);
        }
    }
}