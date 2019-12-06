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
    class Program
    {
        static void Main(string[] args)
        {
            var options = ArgumentParser.ParseArguments(args);
            var setting = new TagsCloudSetting(options);
            var container = BuildContainer(setting);
            var tagCloudVisualizator = container.Resolve<TagCloudVisualizator>();
            tagCloudVisualizator.DrawTagCloud(options.InputFile, options.OutputFile, setting);
        }
        

        private static IContainer BuildContainer(TagsCloudSetting setting)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TokensParser>().As<ITokensParser>();
            builder.Register(c => setting).As<ICloudSetting>().SingleInstance();
            builder.Register(c => setting.GetCenterImage()).As<Point>().SingleInstance();
            builder.RegisterType<BoringFilter>().As<IFilter>().WithParameter("boringWords",new string[0]);
            builder.RegisterType<SpiralGenerator>().As<IPointGenerator>();
            builder.RegisterType<FileManager.FileManager>().As<IFileManager>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleGenerator>();
            builder.RegisterType<TagCloudVisualizator>().AsSelf();
            
            return builder.Build();
        }
    }
}