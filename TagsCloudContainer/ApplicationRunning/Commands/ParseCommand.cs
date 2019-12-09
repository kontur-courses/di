using System;
using System.IO;
using TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands;
using TagsCloudContainer.TextParsing;
using TagsCloudContainer.TextParsing.CloudParsing.ParsingRules;
using TagsCloudContainer.TextParsing.FileWordsParsers;

namespace TagsCloudContainer.ApplicationRunning.Commands
{
    public class ParseCommand : IConsoleCommand
    {
        private TagsCloud cloud;
        private SettingsManager manager;
        public ParseCommand(TagsCloud cloud, SettingsManager manager)
        {
            this.cloud = cloud;
            this.manager = manager;
        }
        public void Act(string[] args)
        {
            var path = string.Join(" ", args).Trim('\'');
            if(!File.Exists(path)) throw new ArgumentException($"No file '{path}' found!");
            var extension = Path.GetExtension(path);
            var parser = WordsParser.GetParser(extension);
            Parse(parser, path);
            Console.WriteLine($"Successfully parsed words from: '{path}'");
        }

        private void Parse(IFileWordsParser parser, string path)
        {
            manager.ConfigureWordsParserSettings(parser, path, new DefaultParsingRule());
            cloud.ParseWords();
        }

        public string Name => "Parse";
        public string Description => "Parse words from path";
        public string Arguments => "path";
    }
}