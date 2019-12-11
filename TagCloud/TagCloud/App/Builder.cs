using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TagCloud
{
    public static class Builder
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            RegisterBoringWords(builder);
            RegisterSpeechParts(builder);
            RegisterReaders(builder);
            RegisterInterfacesCollections(builder);
            RegisterSettings(builder);

            builder.RegisterType<DefaultExtractor>().SingleInstance().As<IExtractor>();
            builder.RegisterType<ArchimedeanSpiralLayouter>().SingleInstance().As<ILayouter>();
            builder.RegisterType<AppVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<ImageHolder>().SingleInstance().As<IImageHolder>().As<PictureBox>().As<ImageHolder>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();

            return builder.Build();
        }

        private static void RegisterBoringWords(ContainerBuilder builder)
        {
            var boringWords = File.ReadAllLines($"{HelperMethods.GetProjectDirectory()}\\BoringWords.txt");
            foreach (var word in boringWords)
                builder.RegisterInstance(new BoringWord(word)).As<BoringWord>();
        }

        private static void RegisterSpeechParts(ContainerBuilder builder)
        {
            var speechParts = Enum.GetValues(typeof(SpeechPartEnum)).Cast<SpeechPartEnum>();
            foreach (var speechPart in speechParts)
            {
                var speechPartValue = Enum.GetName(typeof(SpeechPartEnum), speechPart);
                builder.RegisterInstance(new SpeechPart(speechPartValue, speechPart)).As<SpeechPart>();
            }
        }

        private static void RegisterReaders(ContainerBuilder builder)
        {
            builder.RegisterType<Reader>().SingleInstance().As<Reader>();
            builder.RegisterType<TxtReader>().SingleInstance().As<TxtReader>();
            builder.RegisterType<DocReader>().SingleInstance().As<DocReader>();
        }

        private static void RegisterInterfacesCollections(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IUiAction).IsAssignableFrom(t)).SingleInstance().As<IUiAction>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IFilter).IsAssignableFrom(t)).SingleInstance().As<IFilter>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IParser).IsAssignableFrom(t)).SingleInstance().As<IParser>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(ITheme).IsAssignableFrom(t)).SingleInstance().As<ITheme>();
        }

        private static void RegisterSettings(ContainerBuilder builder)
        {
            builder.Register(context => ImageSettings.GetDefaultSettings()).SingleInstance().As<ImageSettings>();
            builder.Register(context => FontSettings.GetDefaultSettings()).SingleInstance().As<FontSettings>();
            builder.Register(context => LayouterSettings.GetDefaultSettings()).SingleInstance().As<LayouterSettings>();
        }
    }
}
