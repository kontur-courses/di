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

            [Option("img-ext", Default = "png", HelpText = "Extension of image to save.")]
            public string ImageExtension{ get; set; }
            
            [Option("input-ext", Default = "txt", HelpText = "Extension of input file with text (txt or docx).")]
            public string InputExtension{ get; set; }

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

        private Config GetConfig(SaveOptions opts) => new Config(
            new Point(500, 500),
            opts.PathToSave,
            opts.Count,
            new Font(opts.FontName, opts.FontSize),
            opts.FileName,
            opts.OutPath ?? Environment.CurrentDirectory,
            Color.FromName(opts.Color),
            Color.FromName(opts.BackgroundColor),
            opts.ImageExtension,
            opts.InputExtension);

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