using Autofac;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Printing;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Statistics;
using TagsCloudVisualization.WordProcessors;
using TagsCloudVisualization.WordValidators;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization
{
    public static class Configurator
    {
        private static void InjectParsers(ContainerBuilder builder)
        {
            builder.RegisterType<LinesParser>().SingleInstance().AsSelf();
            builder.RegisterType<LiteraryTextParser>().SingleInstance().AsSelf();
            builder.RegisterType<InitialFormWordProcessor>().As<ILiteraryWordProcessor>().SingleInstance();
            builder.Register(_ => WordList.CreateFromFiles(@"Russian.dic")).SingleInstance().AsSelf();;
        }
        
        private static void InjectValidators(ContainerBuilder builder)
        {
            builder.RegisterType<IgnoredWordsValidator>().As<IWordValidator>().AsSelf().SingleInstance();
            builder.RegisterType<TooShortWordValidator>().As<IWordValidator>().AsSelf().SingleInstance();
        }
        
        public static IContainer InjectWith()
        {
            var builder = new ContainerBuilder();

            InjectProcessors(builder);
            InjectReaders(builder);
            InjectParsers(builder);
            InjectValidators(builder);
            
            builder.RegisterType<LiteraryWordsStatistics>().AsSelf();
            builder.RegisterType<OneWordByLineStatistics>().AsSelf();
            builder.RegisterType<DefaultWordStatisticsToSizeConverter>().As<IWordStatisticsToSizeConverter>().SingleInstance();

            builder.RegisterType<RandomColorScheme>().AsSelf().SingleInstance();
            builder.RegisterType<SingleColorScheme>().AsSelf().SingleInstance();
            
            builder.RegisterType<TextPrinter>().As<IPrinter<Text>>().SingleInstance();
            builder.RegisterType<RectanglesReCalculator>().As<IRectanglesReCalculator>().SingleInstance();

            return builder.Build();
        }

        private static void InjectProcessors(ContainerBuilder builder)
        {
            builder.RegisterType<LiteraryTextProcessor>().As<LiteraryTextProcessor>().SingleInstance();
            builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<OneWordByLineProcessor>().As<OneWordByLineProcessor>().SingleInstance();
        }

        private static void InjectReaders(ContainerBuilder builder)
        {
            builder.RegisterType<DocxReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<TxtReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<MyPdfReader>().As<IFileReader>().SingleInstance();
        }
    }
}