using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagCloud.Drawing
{
    [Verb("draw", HelpText = "Draw processed text")]
    public class DrawerOptions : IDrawerOptions
    {
        private readonly Color _defaultBackgroundColor = Color.White;
        private Color _backgroundColor;

        [Option("center", Required = false, HelpText = "Set center of cloud")]
        public Point Center { get; set; }
        
        [Option('c', "colors", Required = false, HelpText = "Set colors of cloud", Separator = ':')]
        public IEnumerable<Color> WordColors { get; set; }
        
        [Option('b', "backgroud", Required = false, HelpText = "Set background color of cloud")]
        public Color BackgroundColor
        {
            get => _backgroundColor.IsEmpty ? _defaultBackgroundColor : _backgroundColor; 
            set => _backgroundColor = value; 
        }

        [Option('f', "font-family", Required = false, HelpText = "Set font family of cloud")]
        public FontFamily FontFamily { get; set; } = FontFamily.GenericSansSerif;

        [Option('s', "font-size", Required = false, HelpText = "Set base font size of cloud", Default = 20)]
        public float BaseFontSize { get; set; } = 20;
        
        [Option( "picture-size", Required = false, HelpText = "Set picture size of cloud")]
        public Size Size { get; set; }
        
        [Option("format", Required = false, HelpText = "Set format of result picture")]
        public ImageFormat Format { get; set; } = ImageFormat.Png;
    }
}