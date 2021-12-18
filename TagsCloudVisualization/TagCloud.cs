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
        // private string? sourceText;
        // private IEnumerable<string>? parsedText;
        // private IEnumerable<TagWordInfo>? tagWordInfo;
        // private IList<Text>? textToPrint;
        // private CircularCloudLayouter? layouter;

        public TagCloud()
        {
            Container = Configurator.InjectWith();
        }

        // private Result<string> LoadTextFile(string filePath)
        // {
        //     return LoadFile(filePath);
        // }
        
        private Result<string> LoadFile(string filePath)
        {
            return Result.Ok(Path.GetExtension(filePath))
                .Then(TextFormat.GetFormatByExtension)
                .Then(x => Container.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(y => y.Format == x))
                .ValidateNull("no proper reader exist")
                .Then(x => x!.ReadFile(filePath));
            // var format = TextFormat.GetFormatByExtension(Path.GetExtension(filePath));
            // var reader = Container.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) 
            //              ?? throw new ArgumentException("no proper reader exist");
            // return reader.ReadFile(filePath);
        }

        private Result<IEnumerable<string>> ParseText(Config config)
        {
            return LoadFile(config.TextFilePath)
                .Then(x => config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                    ? Container.Resolve<LiteraryTextParser>().ParseText(x)
                    : Container.Resolve<LinesParser>().ParseText(x));
            // LoadFile(config.TextFilePath);
            // return config.SourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText 
            //     ? Container.Resolve<LiteraryTextParser>().ParseText(sourceText!)
            //     : Container.Resolve<LinesParser>().ParseText(sourceText!);
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
            // var statistic = GetStatistics(config);

            // return ParseText(config)
            //     .Then(x => (x, GetStatistics(config)))
            //     .Then(x =>
            //     {
            //         if (config.CustomIgnoreFilePath is not null)
            //         {
            //             Result.Ok(LoadFile(config.CustomIgnoreFilePath))
            //                 .Then(y => Container.Resolve<LinesParser>().ParseText(y.GetValueOrThrow()!))
            //                 .Then(y => Container.Resolve<IgnoredWordsValidator>().SetIgnoreWords(y))
            //                 .OnFail(y => {  });
            //         }
            //         x.Item2.AddWords(x.x);
            //         return Container.Resolve<IWordStatisticsToSizeConverter>().Convert(x.Item2, config.WordCountToStatistic);
            //     });
                
            return ParseText(config)
                .Then(x => (x, GetStatistics(config)))
                .ThenAction(x =>
                {
                    if (config.CustomIgnoreFilePath is not null)
                    {
                        Result.Ok(LoadFile(config.CustomIgnoreFilePath))
                            .Then(y => Container.Resolve<LinesParser>().ParseText(y.GetValueOrThrow()!))
                            .Then(y => Container.Resolve<IgnoredWordsValidator>().SetIgnoreWords(y))
                            .OnFail(y => {  });
                    }
                })
                .ThenAction(x => { x.Item2.AddWords(x.x); })
                .Then(x => (x.x, x.Item2, Container.Resolve<IWordStatisticsToSizeConverter>()))
                .Then(x => x.Item3.Convert(x.Item2, config.WordCountToStatistic));
            
            
            // if (config.CustomIgnoreFilePath is not null) 
            //     Container.Resolve<IgnoredWordsValidator>().SetIgnoreWords(Container.Resolve<LinesParser>().ParseText(LoadFile(config.CustomIgnoreFilePath)));
            //
            // statistic.AddWords(parsedText!);
            // tagWordInfo = Container.Resolve<IWordStatisticsToSizeConverter>().Convert(statistic, config.WordCountToStatistic);
        }

        private Result<ILayouter<Rectangle>> LoadLayouter(Config config)
        {
            return Result.Ok<IInfinityPointsEnumerable>(new PointSpiral(Point.Empty, config.Density, config.Density))
                .Then(x => new CircularCloudLayouter(x) as ILayouter<Rectangle>);
        }

        private Result<IEnumerable<Text>> LoadTextToPrint(Config config)
        {
            return LoadStatistic(config)
                .ThenForEach(x =>
                {
                    return LoadLayouter(config)
                        .Then(y => y.PutNextRectangle(x.GetCollisionSize()))
                        .Then(y => new Text(x.Word, config.Font, y))
                        .OnFail(y => { })
                        .GetValueOrThrow()!;
                });
            
            // var tagWordInfo = LoadStatistic(config);
            // var layouter = LoadLayouter(config);
            //
            // foreach (var info in tagWordInfo.GetValueOrThrow()!)
            //     yield return new Text(info.Word, config.Font, layouter.GetValueOrThrow().PutNextRectangle(info.GetCollisionSize()));
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
                
                
            
            // IColorScheme colorScheme = config.Color is null 
            //     ? Container.Resolve<RandomColorScheme>() 
            //     : Container.Resolve<SingleColorScheme>();
            // if (config.Color is not null) ((SingleColorScheme) colorScheme).SetColor(config.Color.Value);
            //
            // return 
            //     ;.GetBitmap(colorScheme, LoadTextToPrint(config).GetValueOrThrow(), config.Size)
        }
    }
}