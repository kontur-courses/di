using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using MatthiWare.CommandLine.Abstractions;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Reader;
using TagsCloudContainer.UserOptions;
using TagsCloudContainer.WordTagsCloud;

namespace TagsCloudContainer
{
    public class AppProcessor
    {
        private readonly ICommandLineParser<AllCommands> _parser;
        private readonly string[] _args;
        private readonly ICloudSettings _cloudSettings;
        private readonly IVisualization _visualization;
        private readonly ITextAnalyzer _textAnalyzer;
        private readonly ICloudLayouter _cloudLayouter;

        public AppProcessor(ICommandLineParser<AllCommands> commandsParser, string[] args, ICloudSettings cloudSettings,
            IVisualization visualization,
            ITextAnalyzer textAnalyzer, ICloudLayouter cloudLayouter)
        {
            _parser = commandsParser;
            _args = args;
            _cloudSettings = cloudSettings;
            _visualization = visualization;
            _textAnalyzer = textAnalyzer;
            _cloudLayouter = cloudLayouter;
        }

        public void Run()
        {
            var parsed = _parser.Parse(_args);
            if (parsed.HasErrors)
            {
                return;
            }

            AddValuesInCloudSettings(parsed.Result);
            var text = ReadingFile.GetTextFromFile(
                _cloudSettings.GetParameterValue<string>(ParameterType.PathToCustomText));
            var interestingWords = _textAnalyzer.GetInterestingWords(text,
                _cloudSettings.GetParameterValue<string[]>(ParameterType.BoringWords));
            var wordFrequency = interestingWords.GetWordsFrequency();
            var wordTagsLayouter = new WordTagsLayouter(_cloudLayouter).GetWordTags(wordFrequency,
                _cloudSettings.GetParameterValue<Font>(ParameterType.Font));
            using var bitmap =
                _visualization.GetBitmapImageCloud(_cloudLayouter.CloudRadius, wordTagsLayouter.ToList());
            bitmap.Save(_cloudSettings.GetParameterValue<string>(ParameterType.PathToSave), ImageFormat.Png);
        }

        private void AddValuesInCloudSettings(AllCommands commands)
        {
            foreach (var option in commands.GetType().GetProperties())
            {
                var val = option.GetValue(commands);
                if (val == null)
                    continue;
                _cloudSettings.AddOrUpdateParameter(option.Name, val.ToString());
            }
        }
    }
}