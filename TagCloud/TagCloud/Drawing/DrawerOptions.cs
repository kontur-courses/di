using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagCloud.Drawing
{
    [Verb("draw", HelpText = "Построить облако тэгов по обработанному тексту")]
    public class DrawerOptions : IDrawerOptions
    {
        private readonly Color _defaultBackgroundColor = Color.White;
        private Color _backgroundColor;

        [Option("center", Required = false, HelpText = "Задать центр облака")]
        public Point Center { get; set; }
        
        [Option('c', "colors", Required = false, HelpText = "Задать палитру цветов для текста", Separator = ':')]
        public IEnumerable<Color> WordColors { get; set; }
        
        [Option('b', "backgroud", Required = false, HelpText = "Задать цвет фона")]
        public Color BackgroundColor
        {
            get => _backgroundColor.IsEmpty ? _defaultBackgroundColor : _backgroundColor; 
            set => _backgroundColor = value; 
        }

        [Option('f', "font-family", Required = false, HelpText = "Выбрать семейство шрифтов")]
        public FontFamily FontFamily { get; set; } = FontFamily.GenericSansSerif;

        [Option('s', "font-size", Required = false, HelpText = "Задать базовый размер шрифта (итоговый зависит от частоты встречаемости слова)", Default = 20)]
        public float BaseFontSize { get; set; } = 20;
        
        [Option( "picture-size", Required = false, HelpText = "Задать размер итогового изображения")]
        public Size Size { get; set; }
        
        [Option("format", Required = false, HelpText = "Задать формат результирующего изображения")]
        public ImageFormat Format { get; set; } = ImageFormat.Png;
    }
}