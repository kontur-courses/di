using System.Drawing;
using Autofac;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var container = Register(args);
            var imageCreator = container.Resolve<ImageCreator>();
            imageCreator.Save();
        }

        private static IContainer Register(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicWordsSelector>().As<IWordsSelector>();
            builder.RegisterType<WordReaderFromFile>().As<IWordReader>();
            builder.RegisterType<ConsoleArgumentParser>().As<IArgumentParser>();
            builder.Register(c => c.Resolve<IArgumentParser>().GetWordSetting(args)).As<WordSetting>();
            builder.Register(c => c.Resolve<IArgumentParser>().GetImageSetting(args)).As<ImageSetting>();
            builder.Register(c => c.Resolve<IArgumentParser>().GetPath(args)).As<string>();
            builder.Register(c =>
            {
                var setting = c.Resolve<ImageSetting>();
                return new CircularCloudLayouter(new Point(setting.Width / 2, setting.Height / 2));
            }).As<ICloudLayouter>();
            builder.RegisterType<Compositor>().As<Compositor>();
            builder.RegisterType<ImageCreator>().As<ImageCreator>();
            return builder.Build();
        }
    }
}