using System.Collections.Generic;
using CommandLine;

namespace CUITagCloud
{
    public class Option
    {
        [Option('t', "text", Required = true, HelpText = "Текст, по которому строить облако тегов.")]
        public string InputFileName { get; set; }

        [Option('h', "height", Required = false, Default = 1000, HelpText = "Высота изображения в пискелях")]
        public int Height { get; set; }

        [Option('w', "width", Required = false, Default = 1000, HelpText = "Ширина изображения в пискелях")]
        public int Width { get; set; }

        [Option('c', "count", Default = 20, HelpText = "Количество слов из текста в облаке")]
        public int CountWords { get; set; }

        [Option('o', "outputfile", Default = "out.png", HelpText = "Файл, куда сохранится картинка")]
        public string OutputFile { get; set; }

        [Option('s', "styletheme", Default = "classic",
            HelpText = "Тема, по которой будет отрисовываться. [classic, black]")]
        public string Theme { get; set; }

        [Option('f', "filters", Separator = ',', Default = new[] {"simple"},
            HelpText = "Фильтры, которые можно применить к тексту. [simple, length]")]
        public IEnumerable<string> Filters { get; set; }

        [Option('v', "converters", Separator = ',', Default = new[] {"simple"},
            HelpText = "Преобразователи слов, которые можно применить к тексту. [simple]")]
        public IEnumerable<string> Converters { get; set; }
    }
}