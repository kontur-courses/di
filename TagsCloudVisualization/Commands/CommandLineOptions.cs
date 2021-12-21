using System.Collections.Generic;
using CommandLine;

namespace TagsCloudVisualization.Commands
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true,
            HelpText = "Путь до файла, по которому будет построено облако тегов.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true,
            HelpText = "Путь, куда будет сохранен результат.")]
        public string OutputFile { get; set; }


        [Option('w', "width", Default = 1920,
            HelpText = "Ширина генерируемого изображения.")]
        public int Width { get; set; }

        [Option('h', "height", Default = 1080,
            HelpText = "Высота генерируемого изображения.")]
        public int Height { get; set; }

        [Option("bgColor", Default = "DimGray",
            HelpText = "Используемый цвет для фона изображения. Допускается либо системное имя цвета, либо hex-значение.")]
        public string BackgroundColor { get; set; }

        [Option("fgColors", Separator = ';', Default = new[] {"Chocolate"},
            HelpText = "Используемые цвета для тегов. Допускается либо системное имя цвета, либо hex-значение, а также указание нескольких цветов через ';'.")]
        public IEnumerable<string> ForegroundColors { get; set; }

        [Option("fonts", Separator = ';', Default = new[] {"Arial"},
            HelpText = "Используемые шрифты для тегов. Допускается указание нескольких шрифтов через ';'.")]
        public IEnumerable<string> Fonts { get; set; }

        [Option("size", Default = 25,
            HelpText = "Используемый размер шрифта тегов.")]
        public float TagSize { get; set; }

        [Option("scatter", Default = 10,
            HelpText = "Используемый разброс в размере шрифтов тегов.")]
        public float TagSizeScatter { get; set; }
    }
}