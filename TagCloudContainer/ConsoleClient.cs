using System;
using System.Drawing;
using CommandLine;
using TagsCloudPreprocessor;

namespace TagCloudContainer
{
    public class ConsoleClient : IUserInterface
    {
        private readonly string[] args;
        private readonly IWordExcluder wordExcluder;
        private bool toExit = true;
        public Config Config { get; private set; }
        
        public ConsoleClient(string[] args, IWordExcluder wordExcluder)
        {
            this.args = args;
            this.wordExcluder = wordExcluder;
            HandleArgs();
            if (toExit)
                Environment.Exit(0);
        }

        [Verb("save", HelpText = "Save tag cloud.")]
        private class SaveOptions
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

            //ToDo Выбор разрешения сохраняемого файла
        }
        
        [Verb("exclude", HelpText = "Exclude word from drawing.")]
        private class ExcludeOptions
        {
            [Value(0, Required = true, HelpText = "Word to exclude.")]
            public string Word{ get; set; }

            //ToDo Выбор разрешения сохраняемого файла
        }

        private Config GetConfig(SaveOptions opts)
        {
            var center = new Point(500, 500);
            
            var fontName = opts.FontName;
            var fontSize = opts.FontSize;
            var count = opts.Count;
            var inputFile = opts.PathToSave;
            var fileName = opts.FileName;
            var outPath = opts.OutPath ?? Environment.CurrentDirectory;
            var font = new Font(fontName, fontSize);
            var color = Color.FromName(opts.Color);
            var backgroundColor = Color.FromName(opts.BackgroundColor);
            
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
        
        private void HandleArgs()
        {
            var saveOptions = typeof(SaveOptions);
            var excludeOptions = typeof(ExcludeOptions);
            
            Parser.Default.ParseArguments(args, saveOptions, excludeOptions).WithParsed(
                (opts) =>
                {
                    if (opts.GetType() == typeof(SaveOptions))
                    {
                        Config = GetConfig((SaveOptions)opts);
                        toExit = false;
                    }
                    else if (opts.GetType() == typeof(ExcludeOptions))
                    {
                        AddExcludingWord(((ExcludeOptions)opts).Word);
                    }
                });
        }

        private void AddExcludingWord(string word)
        {
            wordExcluder.SetExcludedWord(word);
        }
    }
}