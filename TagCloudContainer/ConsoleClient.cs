using System;
using System.Drawing;
using CommandLine;
using CommandLine.Text;

namespace TagCloudContainer
{
    public class ConsoleClient : IUserInterface
    {
        private class Options
        {
            [Option('c', "count", Default = 70, HelpText = "Count of tags in cloud.")]
            public int Count { get; set; }

            [Option("font-name", Default = "Times New Roman", HelpText = "Font name.")]
            public string FontName { get; set; }

            [Option("font-size", Default = 40.0f, HelpText = "Font size in pt.")]
            public float FontSize { get; set; }

            [Option('n', "name", Default = "Cloud", HelpText = "File name.")]
            public string FileName { get; set; }

            [Option("color", Default = "Black", HelpText = "Name of color.")]
            public string Color { get; set; }

            [Option("back-color", Default = "White", HelpText = "Name of background color.")]
            public string BackgroundColor { get; set; }

            [Option("out-path", HelpText = "Path to output directory.")]
            public string OutPath { get; set; }

            [Value(0, Required = true, HelpText = "Path to input file.")]
            public string PathToSave { get; set; }
            //ToDo Сделать вызов help с ключем -h
            //ToDo Сделать режимы работы (сохранение картинки, добавление слов в исключенные)
            //ToDo Сделать выбор языка
            //ToDo Выбор разрешения сохраняемого файла
            //ToDo Пофиксить баг, что после запуска программы с --help -- version начинается создание облака тегов 
        }

        public Config GetConfig(string[] args, out bool toExit)
        {
            toExit = false;
            var center = new Point(500, 500);
            string inputFile = null;
            var count = 20;
            var fileName = "SimpleCloud";
            var outPath = "";
            var font = new Font("Times New Roman", 40.0f);
            var color = Color.Black;
            var backgroundColor = Color.FromArgb(0, Color.White);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var fontName = o.FontName;
                    var fontSize = o.FontSize;
                    count = o.Count;
                    inputFile = o.PathToSave;
                    fileName = o.FileName;
                    outPath = o.OutPath ?? Environment.CurrentDirectory;
                    font = new Font(fontName, fontSize);
                    color = Color.FromName(o.Color);
                    backgroundColor = Color.FromName(o.BackgroundColor);
                });

            if (inputFile == null)
                toExit = true;
            
            return new Config(
                center,
                inputFile,
                count,
                font,
                fileName,
                outPath,
                color,
                backgroundColor);
        }
    }
}
