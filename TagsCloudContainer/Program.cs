using System.Drawing;
using Autofac;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var consoleParser = new ConsoleArgumentParser();
            var path = consoleParser.GetPath(args);
            var imageSetting = consoleParser.GetImageSetting(args);
            var wordSetting = consoleParser.GetWordSetting(args);
            var container = Register(wordSetting, imageSetting, path);
            var imageCreator = container.Resolve<ImageCreator>();
            imageCreator.Save();
        }

        private static IContainer Register(WordSetting wordSetting, ImageSetting imageSetting, string path)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicWordsSelector>().As<IWordsSelector>();
            builder.RegisterType<WordReaderFromFile>().As<IWordReader>();
            builder.RegisterInstance(wordSetting).As<WordSetting>();
            builder.RegisterInstance(imageSetting).As<ImageSetting>();
            builder.RegisterInstance(path).As<string>();
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