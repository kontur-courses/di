using System;
using System.IO;
using CommandLine;

namespace TagsCloudVisualization
{
    public class Options
    {
        public static readonly string ProjectDirectory 
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        
        public static string WordsFilePath = string.Concat(ProjectDirectory, @"\words.txt");
        public static string SaveTagCloudImagePath = string.Concat(ProjectDirectory, @"\Images\");
        public static int AmountImages = 1;
        
        [Option('p', "path", Required = false, HelpText = "Путь к файлу со словами")]
        public string WordsFilePathArgument { get; set; }
        [Option('s', "save", Required = false, HelpText = "Путь сохранения результата")]
        public string SaveTagCloudImagePathArgument { get; set; }
        [Option('a', "amount", Required = false, HelpText = "Количество изображений")]
        public int AmountImagesArgument { get; set; }
        
        public static void RunOptions(Options opts)
        {
            if (!string.IsNullOrEmpty(opts.WordsFilePathArgument))
                WordsFilePath = opts.WordsFilePathArgument;

            if (!string.IsNullOrEmpty(opts.SaveTagCloudImagePathArgument))
                SaveTagCloudImagePath = opts.SaveTagCloudImagePathArgument;
            
            if (opts.AmountImagesArgument != 0)
                AmountImages = opts.AmountImagesArgument;
        }
    }
}