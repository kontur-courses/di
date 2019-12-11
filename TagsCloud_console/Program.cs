using Autofac;
using CommandLine;
using System;
using System.Reflection;
using TagsCloud;
using TagsCloud.Renderers;
using TagsCloud.FileParsers;
using TagsCloud.ImageSavers;
using TagsCloud.Layouters;
using TagsCloud.WordsFiltering;

namespace TagsCloud_console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var tagsCloudAssembly = Assembly.GetAssembly(typeof(TagsCloudGenerator));
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFileParser).Namespace).As<IFileParser>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IFilter).Namespace).As<IFilter>().SingleInstance().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(ITagsCloudLayouter).Namespace).As<ITagsCloudLayouter>().SingleInstance();
            builder.RegisterType<SimpleRenderer>().As<ITagsCloudRenderer>().SingleInstance();
            builder.RegisterAssemblyTypes(tagsCloudAssembly).InNamespace(typeof(IImageSaver).Namespace).As<IImageSaver>().SingleInstance();
            builder.RegisterType<TagsCloudGenerator>().AsSelf().SingleInstance();

            var container = builder.Build();

            Parser.Default.ParseArguments<InputOptions>(args)
                .WithParsed(opts =>
                {
                    var tagCloud = container.Resolve<TagsCloudGenerator>();
                    tagCloud.GenerateCloud(opts.InputFile).SaveTo(opts.OutputFile);
                });

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
