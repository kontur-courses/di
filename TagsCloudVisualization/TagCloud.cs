using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Printing;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Statistics;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization
{
    public class TagCloud
    {
        public ILifetimeScope Container { get; }
        private string? sourceText;
        private IEnumerable<string>? parsedText;
        private IEnumerable<TagWordInfo>? tagWordInfo;
        private IList<Text>? textToPrint;
        private CircularCloudLayouter? layouter;

        public TagCloud()
        {
            Container = Configurator.InjectWith();
        }

        private void LoadTextFile(string filePath)
        {
            sourceText = LoadFile(filePath);
        }
        
        private string LoadFile(string filePath)
        {
            var format = TextFormat.GetFormatByExtension(Path.GetExtension(filePath));
            var reader = Container.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) 
                         ?? throw new ArgumentException("no proper reader exist");
            return reader.ReadFile(filePath);
        }

        private void ParseText(Config config)
        {
            LoadTextFile(config.TextFilePath);
            parsedText = config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText 
                ? Container.Resolve<LiteraryTextParser>().ParseText(sourceText!)
                : Container.Resolve<LinesParser>().ParseText(sourceText!);
        }

        private void LoadStatistic(Config config)
        {
            ParseText(config);
            
            Container.Resolve<TooShortWordValidator>().SetLimit(config.MinWordToStatisticLength);
            IWordsStatistics statistic = config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                ? Container.Resolve<LiteraryWordsStatistics>()
                : Container.Resolve<OneWordByLineStatistics>();
            if (config.CustomIgnoreFilePath is not null) 
                Container.Resolve<IgnoredWordsValidator>().SetIgnoreWords(Container.Resolve<LinesParser>().ParseText(LoadFile(config.CustomIgnoreFilePath)));
            statistic.AddWords(parsedText!);
            tagWordInfo = Container.Resolve<IWordStatisticsToSizeConverter>().Convert(statistic, config.WordCountToStatistic);
        }

        private void LoadLayouter(Config config)
        {
            layouter = new CircularCloudLayouter(new PointSpiral(Point.Empty, config.Density, (int) config.Density));
        }

        private void LoadTextToPrint(Config config)
        {
            LoadStatistic(config);
            LoadLayouter(config);
            
            textToPrint = new List<Text>();
            foreach (var info in tagWordInfo!)
                textToPrint.Add(new Text(info.Word, config.Font, layouter!.PutNextRectangle(info.GetCollisionSize())));
        }

        public Bitmap GetBitmap(Config config)
        {
            LoadTextToPrint(config);
            
            var painter = Container.Resolve<IPrinter<Text>>();
            IColorScheme colorScheme = config.Color is null 
                ? Container.Resolve<RandomColorScheme>() 
                : Container.Resolve<SingleColorScheme>();
            if (config.Color is not null) ((SingleColorScheme) colorScheme).SetColor(config.Color.Value);
            
            return painter.GetBitmap(colorScheme, textToPrint!, config.Size);
        }
    }
}