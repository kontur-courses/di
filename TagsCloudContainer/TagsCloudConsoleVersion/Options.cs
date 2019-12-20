using CommandLine;

namespace TagsCloudConsoleVersion
{
    public class Options
    {
        
        [Option('p', "path", Required = true, HelpText = "Путь до файла с текстом")]
        public string PathToFile { get; set; }
        
        [Option('n', "name", Required = true, HelpText = "Имя сохраняемого файла(расширение указывать не надо)")]
        public string PathSaveFile { get; set; }
        
        [Option('s', "squeezed", Default = false, HelpText = "Использовать сжатый алгоритм, дольше по времени")]
        public bool UseSqueezedAlgorithm { get; set; }
        
        [Option('b', "background", Default = "Aquamarine", HelpText = "Цвет фона, цвета только на английском")]
        public string ColorBackground { get; set; }
        
        [Option( "brush", Default = "Black", HelpText = "Цвет кистей, цвета только на английском")]
        public string ColorBrush { get; set; }
        
        [Option( "famyilyNameFont", Default = "Arial", HelpText = "Цвет кистей, цвета только на английском")]
        public string FamyilyNameFont { get; set; }
        
        [Option('w', "width", Default = 500, HelpText = "Ширина изображения")]
        public int Width { get; set; }

        [Option('h', "height", Default = 500, HelpText = "Высота изображения")]
        public int Height { get; set; }
        
        [Option('d', "delineation", Default = false, HelpText = "Обводка слов прямоугольниками черного цвета")]
        public bool HaveDelineation { get; set;}
        
    }
}