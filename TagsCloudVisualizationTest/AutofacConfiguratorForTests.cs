// using System.Collections.Generic;
// using Autofac;
// using FakeItEasy;
// using ResultProject;
// using TagsCloudVisualization;
// using TagsCloudVisualization.Parsers;
// using TagsCloudVisualization.Printing;
// using TagsCloudVisualization.Readers;
// using TagsCloudVisualization.Statistics;
// using TagsCloudVisualization.WordProcessors;
// using TagsCloudVisualization.WordValidators;
//
// namespace TagsCloudVisualizationTest
// {
//     public static class AutofacConfiguratorForTests
//     {
//         private static void InjectParsers(ContainerBuilder builder)
//         {
//             builder.RegisterType<LinesParser>().SingleInstance().AsSelf();
//             builder.RegisterType<LiteraryTextParser>().SingleInstance().AsSelf();
//             builder.Register(_ => new InitialFormWordProcessor("Russian.dic")).As<ILiteraryWordProcessor>().SingleInstance();
//         }
//         
//         private static void InjectValidators(ContainerBuilder builder)
//         {
//             builder.RegisterType<IgnoredWordsValidator>().As<IWordValidator>().AsSelf().SingleInstance();
//             builder.RegisterType<TooShortWordValidator>().As<IWordValidator>().AsSelf().SingleInstance();
//         }
//         
//         public static T InjectWith<T>()
//         {
//             var builder = new ContainerBuilder();
//
//             InjectProcessors(builder);
//             InjectReaders(builder);
//             InjectParsers(builder);
//             InjectValidators(builder);
//             
//             builder.RegisterType<LiteraryWordsStatistics>().AsSelf();
//             builder.RegisterType<OneWordByLineStatistics>().AsSelf();
//             builder.RegisterType<DefaultWordStatisticsToSizeConverter>().As<IWordStatisticsToSizeConverter>().SingleInstance();
//
//             builder.RegisterType<RandomColorScheme>().AsSelf().SingleInstance();
//             builder.RegisterType<SingleColorScheme>().AsSelf().SingleInstance();
//             
//             builder.RegisterType<TextPrinter>().As<IPrinter<Text>>().SingleInstance();
//             builder.RegisterType<RectanglesReCalculator>().As<IRectanglesReCalculator>().SingleInstance();
//
//             builder.RegisterType<TagCloud>().AsSelf().SingleInstance();
//             builder.RegisterType<T>().AsSelf().SingleInstance();
//             builder.RegisterType<InfoTagsCloud>().AsSelf().SingleInstance();
//             
//             return builder.Build().Resolve<T>();
//         }
//
//         private static void InjectProcessors(ContainerBuilder builder)
//         {
//             builder.RegisterType<LiteraryTextProcessor>().As<LiteraryTextProcessor>().SingleInstance();
//             builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>().SingleInstance();
//             builder.RegisterType<OneWordByLineProcessor>().As<OneWordByLineProcessor>().SingleInstance();
//         }
//
//         private static void InjectReaders(ContainerBuilder builder)
//         {
//             builder.Register(_ =>
//             {
//                 var reader = A.Fake<IFileReader>();
//                 A.CallTo(() => reader.Format).Returns(TextFormat.Txt);
//                 A.CallTo(() => reader.ReadFile("correct file")).Returns("correct data".AsResult());
//                 // A.CallTo(() => reader.ReadFile(A<string>.Ignored)).ReturnsLazily((string filePath) =>
//                 // {
//                 //     return Result.Fail<string>($"Can't read {filePath} for some reason");
//                 // });
//                 return reader;
//             }).As<IFileReader>();
//         }
//     }
// }