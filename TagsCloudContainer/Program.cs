using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TxtWordsReader>().As<IWordsReader>();
            var parser = containerBuilder.Build().Resolve<IWordsReader>();
            var path = "hello.txt";
            var words = parser.ReadWords(path);

            containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<BlacklistWordsFilter>().As<IWordsFilter>().WithParameter
                ("blacklist", new HashSet<string> {"в", "на", "к", "а", "не", "и", "с", "о"});
            containerBuilder.RegisterType<ToLowerCaseFormatter>().As<IWordsFormatter>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>()
                .WithParameter("center", new Point(400, 400));
            containerBuilder.RegisterType<TagsCloudGenerator>().AsSelf()
                .WithParameter("minLetterSize", new Size(16, 20));
            var generator = containerBuilder.Build().Resolve<TagsCloudGenerator>();
            var cloud = generator.CreateCloud(words);

            containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<IMGTagsCloudRenderer>().As<ITagsCloudRenderer>().WithParameters(
                new List<Parameter>()
                {
                    new NamedParameter("fontFamily", FontFamily.GenericMonospace),
                    new NamedParameter("textColor", Color.DarkBlue),
                    new NamedParameter("pictureSize", new Size(1280, 1024))
                });


            var renderer = containerBuilder.Build().Resolve<ITagsCloudRenderer>();
            renderer.RenderIntoFile("out.png", cloud);
        }
    }
}