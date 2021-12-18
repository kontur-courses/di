using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagCloud;
using TagCloud.Drawing;

namespace TagCloud_ConsoleUI
{
    [Verb("draw", HelpText = "Построить облако тэгов по обработанному тексту")]
    public class DrawerOptions : IDrawerOptions
    {
        private readonly Color _defaultBackgroundColor = Color.White;
        private Color _backgroundColor;

        [Option("format", Required = false, HelpText = "Задать формат результирующего изображения")]
        public ImageExtension ImageExtension { get; set; } = ImageExtension.Png;

        [Option('p', "path", Required = false, HelpText = "Задать директорию для сохранения изображений")]
        public string Path { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        [Option('n', "file-name", Required = false, HelpText = "Задать имя для сохраняемых изображений")]
        public string FileName { get; set; }

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

        [Option('s', "font-size", Required = false,
            HelpText = "Задать базовый размер шрифта (итоговый зависит от частоты встречаемости слова)", Default = 14)]
        public float BaseFontSize { get; set; } = 14;

        [Option("image-size", Required = false, HelpText = "Задать размер итогового изображения\n" +
                                                           "(пример: draw --image-size 1920,1080)")]
        public Size Size { get; set; }

        public ImageFormat? Format => GetImageFormat(ImageExtension);


        private static ImageFormat? GetImageFormat(ImageExtension extension)
        {
            var propertyInfo = typeof(ImageFormat)
                .GetProperties()
                .FirstOrDefault(p => p.Name
                    .Equals(extension.ToString(), StringComparison.InvariantCultureIgnoreCase));

            return propertyInfo?.GetValue(propertyInfo) as ImageFormat;
        }
    }
}