using System;
using System.IO;
using CommandLine;

namespace TagsCloudVisualization
{
    public class Options
    {
        public static readonly string ProjectDirectory 
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        
        public string WordsFilePath = string.Concat(ProjectDirectory, @"\words.txt");
        public string SaveTagCloudImagePath = string.Concat(ProjectDirectory, @"\Images\");
        public int AmountImages = 1;
        
        [Option('p', "path", Required = false, HelpText = "Путь к файлу со словами")]
        private string WordsFilePathArgument { get; set; }
        [Option('s', "save", Required = false, HelpText = "Путь сохранения результата")]
        private string SaveTagCloudImagePathArgument { get; set; }
        [Option('a', "amount", Required = false, HelpText = "Количество изображений")]
        private int AmountImagesArgument { get; set; }
        
        public void RunOptions()
        {
            if (!string.IsNullOrEmpty(WordsFilePathArgument))
                WordsFilePath = WordsFilePathArgument;

            if (!string.IsNullOrEmpty(SaveTagCloudImagePathArgument))
                SaveTagCloudImagePath = SaveTagCloudImagePathArgument;
            
            if (AmountImagesArgument != 0)
                AmountImages = AmountImagesArgument;
        }
    }
}