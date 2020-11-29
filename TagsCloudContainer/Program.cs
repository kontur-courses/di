using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MatthiWare.CommandLine;
using RectanglesCloudLayouter.Core;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Reader;
using TagsCloudContainer.SettingsForTagsCloud;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TextProcessing;
using TagsCloudContainer.UserOptions;
using TagsCloudContainer.WordTagsCloud;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudSettings = new CloudSettings(new List<ICloudParameter>
            {
                new PathToCustomText(), new PathToSaveCloud(), new BackgroundColor(), new TextColor(), new ImageSize(), new TextFont()
            });
            var visualization = new Visualization(cloudSettings);
            var layouter = new CloudLayouter(new Point(0, 0));
            var analyzer = new TextAnalyzer();
            TagsCloudProcess(args, cloudSettings, visualization, layouter, analyzer);
        }

        private static void TagsCloudProcess(string[] args, ICloudSettings cloudSettings, Visualization visualization,
            CloudLayouter layouter, TextAnalyzer analyzer)
        {
            var parser = new CommandLineParser<AllCommands>();
            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                return;
            }

            var programOptions = result.Result;
            foreach (var option in programOptions.GetType().GetProperties())
            {
                var val = option.GetValue(programOptions);
                if (val == null)
                    continue;
                cloudSettings.AddOrUpdateParameter(option.Name, val.ToString());
            }

            var wordFrequency = analyzer.GetInterestingWords(
                new ReadingFile().GetTextFromFile(
                    cloudSettings.GetParameterValue<string>(ParameterType.PathToCustomText))).GetWordsFrequency();
            var wordTagsLayouter = new WordTagsLayouter(layouter).GetWordTags(wordFrequency,
                cloudSettings.GetParameterValue<Font>(ParameterType.Font));
            visualization.GetBitmapImageCloud(layouter.CloudRadius, wordTagsLayouter.ToList());
        }
    }
}