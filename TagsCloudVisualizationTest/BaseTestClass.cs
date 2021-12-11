// using System.Collections.Generic;
// using System.Drawing;
// using Autofac;
// using Autofac.Features.AttributeFilters;
// using TagsCloudVisualization;
// using TagsCloudVisualization.Layouters;
// using TagsCloudVisualization.WordReaders;
// using TagsCloudVisualization.WordReaders.FormatDecoders;
// using TagsCloudVisualization.WordReaders.WordProcessors;
// using TagsCloudVisualization.WordReaders.WordValidators;
// using TagsCloudVisualizationTest.Builders;
//
// namespace TagsCloudVisualizationTest
// {
//     public abstract class BaseTestClass
//     {
//         protected BaseTestClass(IContainer container)
//         {
//             Container = container;
//         }
//
//         private IContainer GetContainer()
//         {
//             var builder = new ContainerBuilder();
//             
//             builder.Register(_ => PointSpiralBuilder.APointSpiral().Build()).As<IInfinityPointsEnumerable>();
//             builder.RegisterType<CircularCloudLayouter>().As<ILayouter<Rectangle>>();
//             builder.RegisterType<ProcessedWordReader>().Keyed<IWordReader>("FullProcessed").WithAttributeFiltering();
//             builder.RegisterType<TxtFormatDecoder>().As<IFormatDecoder>();
//             builder.RegisterType<IgnoredWordsValidator>().As<IWordValidator>().WithAttributeFiltering();
//             builder.Register(_ => new TooShortWordValidator(1)).As<IWordValidator>();
//             builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>();
//
//             if (!IsLiteraryText)
//             {
//                 builder.Register(p => new FileWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("CurrentReadMode");
//             }
//             else
//             {
//                 builder.Register(p => new FileTextByWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("CurrentReadMode");
//                 builder.RegisterType<InitialFormWordProcessor>().As<IWordProcessor>();
//                 builder.Register(_ => WordList.CreateFromFiles(@"Russian.dic")).As<WordList>();
//             }
//             
//             builder.Register(p => new FileWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("Word");
//             builder.Register(p => new FileTextByWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("Text");
//             builder.Register(p => new FileWordReader(IgnoreWordsFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("IgnoreWords");
//             builder.RegisterType<WordsStatistics>().As<IWordsStatistics>().OnActivated(s => s.Instance.Load()).WithAttributeFiltering();
//             builder.Register(p => new DefaultWordStatisticsToSizeConverter(MaximumWordFontSize, p.Resolve<IWordsStatistics>())).As<IWordStatisticsToSizeConverter>();
//
//             return builder.Build();
//         }
//
//         public IContainer Container { get; }
//     }
// }