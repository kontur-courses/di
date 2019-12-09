using System;
using System.Drawing;
using Autofac;
using TagsCloudContainer.FileManager;
using TagsCloudContainer.Filters;
using TagsCloudContainer.RectangleGenerator;
using TagsCloudContainer.RectangleGenerator.PointGenerator;
using TagsCloudContainer.TokensGenerator;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = ArgumentParser.ParseArguments(args);
            var setting = new TagsCloudSetting(options);
//          var setting = TagsCloudSetting.GetDefault();

            var container = BuildContainer(setting);
            var tagCloudVisualizator = container.Resolve<TagCloudVisualizator>();
            tagCloudVisualizator.DrawTagCloud(options.InputFile, options.OutputFile, setting);
            Console.WriteLine($"Image save in {options.OutputFile}");
        }


        private static IContainer BuildContainer(TagsCloudSetting setting)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileManager.FileManager>().As<IFileManager>();
            builder.RegisterType<MyStemParser>().As<ITokensParser>().SingleInstance();
            builder.RegisterType<MyStemFilter>().As<IFilter>().SingleInstance()
                .WithParameter("allowedWorldType",
                    new[] {WordType.Noun, WordType.Verb, WordType.Adjective, WordType.Adverb});
            builder.Register(c => setting).As<ICloudSetting>().SingleInstance();
            builder.Register(c => setting.GetCenterImage()).As<Point>().SingleInstance();
            builder.RegisterType<SpiralGenerator>().As<IPointGenerator>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleGenerator>();
            builder.RegisterType<TagCloudVisualizator>().AsSelf();

            return builder.Build();
        }
    }
}