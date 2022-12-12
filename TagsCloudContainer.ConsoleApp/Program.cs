using Autofac;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.Register(ctx => new ConsoleSettingsProvider(args)).As<ISettingsProvider>().SingleInstance();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetWordColorSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetWordFontSizeSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetSaveTagsCloudSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetTextReaderSettings()).AsSelf();

            builder.RegisterType<TextFileReader>().As<IWordReader>().SingleInstance();
            builder.RegisterType<WordPreparer>().As<IWordPreparer>().SingleInstance();
            
            builder.RegisterType<WordConstColorProviderFactory>().As<IWordColorProviderFactory>().SingleInstance();
            builder.RegisterType<WordDynamicFontSizeProviderFactory>().As<IWordFontSizeProviderFactory>().SingleInstance();

            builder.RegisterType<CircularWordLayoutBuilder>().As<IWordLayoutBuilder>();
            builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();

            builder.RegisterType<WordPlateVisualizer>().AsSelf().SingleInstance();

            builder.RegisterType<Application>().AsSelf().SingleInstance();

            var container = builder.Build();

            try
            {
                container.Resolve<Application>().Run();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}