using System;
using System.IO;
using Autofac;
using TagsCloudContainer.Filters;
using TagsCloudContainer.RectangleGenerator;
using TagsCloudContainer.RectangleGenerator.PointGenerator;
using TagsCloudContainer.TokensGenerator;
using TagsCloudContainer.Visualization;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = ArgumentParser.ParseArguments(args);
            var setting = new TagsCloudSetting(options);
            //var setting = TagsCloudSetting.GetDefault();

            var container = BuildContainer(setting);
            var tagCloudVisualizator = container.Resolve<TagCloudVisualizator>();
            var text = File.ReadAllText(options.InputFile);
            tagCloudVisualizator.DrawTagCloud(text, setting)
                .Save(options.OutputFile);
            Console.WriteLine($"Image save in {options.OutputFile}");
        }


        private static IContainer BuildContainer(TagsCloudSetting setting)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Visualizer>().As<IVisualizer>();
            builder.RegisterType<MyStemParser>().As<ITokensParser>().SingleInstance();
            builder.RegisterType<MyStemFilter>().As<IFilter>().SingleInstance()
                .WithParameter("allowedWorldType",
                    new[] {GramPartsEnum.Noun, GramPartsEnum.Verb, GramPartsEnum.Adjective, GramPartsEnum.Adverb});
            builder.Register(c => setting).As<ICloudSetting>().SingleInstance();
            builder.RegisterType<SpiralGenerator>().As<IPointGenerator>()
                .WithParameter("center", setting.GetCenterImage());
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleGenerator>();
            builder.RegisterType<TagCloudVisualizator>().AsSelf();

            return builder.Build();
        }
    }
}