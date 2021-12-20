using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using ResultProject;
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

        public TagCloud()
        {
            Container = AutofacConfigurator.InjectWith();
        }
        
        private Result<string> LoadFile(string filePath)
        {
            return Path.GetExtension(filePath).AsResult()
                .Then(TextFormat.GetFormatByExtension)
                .Then(x => Container.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(y => y.Format == x))
                .ValidateNull("no proper reader exist")
                .Then(x => x!.ReadFile(filePath));
        }

        private Result<IEnumerable<string>> ParseText(Config config)
        {
            return LoadFile(config.TextFilePath)
                .Then(x => config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                    ? Container.Resolve<LiteraryTextParser>().ParseText(x)
                    : Container.Resolve<LinesParser>().ParseText(x));
        }

        private IWordsStatistics GetStatistics(Config config)
        {
            return config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                ? Container.Resolve<LiteraryWordsStatistics>()
                : Container.Resolve<OneWordByLineStatistics>();
        }

        private Result<IEnumerable<TagWordInfo>> LoadStatistic(Config config)
        {
            Container.Resolve<TooShortWordValidator>().SetLimit(config.MinWordToStatisticLength);
            
            return ParseText(config)
                .Then(x => (x, GetStatistics(config)))
                .ThenAction(x =>
                {
                    if (config.CustomIgnoreFilePath is not null)
                    {
                        LoadFile(config.CustomIgnoreFilePath).AsResult()
                            .Then(y => Container.Resolve<LinesParser>().ParseText(y.GetValueOrThrow()!))
                            .Then(y => Container.Resolve<IgnoredWordsValidator>().SetIgnoreWords(y))
                            .OnFail(y => {  });
                    }
                })
                .ThenAction(x => x.Item2.AddWords(x.x))
                .Then(x => (x.x, x.Item2, Container.Resolve<IWordStatisticsToSizeConverter>()))
                .Then(x => x.Item3.Convert(x.Item2, config.WordCountToStatistic));
        }

        private Result<ILayouter<Rectangle>> LoadLayouter(Config config)
        {
            return new PointSpiral(Point.Empty, config.Density, config.Density).AsResult()
                .Then(x => new CircularCloudLayouter(x) as ILayouter<Rectangle>);
        }

        private Result<IEnumerable<Text>> LoadTextToPrint(Config config)
        {
            var layouter = LoadLayouter(config);
            return LoadStatistic(config)
                .ThenForEach(x =>
                {
                    return layouter
                        .Then(y => y.PutNextRectangle(x.GetCollisionSize()))
                        .Then(y => new Text(x.Word, config.Font, y))
                        .OnFail(y => { })
                        .GetValueOrThrow()!;
                });
        }

        public Result<Bitmap> GetBitmap(Config config)
        {
            return Result.Ok<IColorScheme>(config.Color is null ? Container.Resolve<RandomColorScheme>() : Container.Resolve<SingleColorScheme>())
                .ThenAction(x =>
                {
                    if (config.Color is not null) ((SingleColorScheme)x).SetColor(config.Color.Value);
                })
                .Then(x => (x, LoadTextToPrint(config)))
                .Then(x => (x.x, x.Item2, Container.Resolve<IPrinter<Text>>()))
                .Then(x => x.Item3.GetBitmap(x.x, x.Item2, config.Size));
        }
    }
}