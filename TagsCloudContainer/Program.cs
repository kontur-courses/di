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
//            if (args.Length > 0)
//                RunCommand(args);
//            else
//                RunInteractiveMode();

            var setting = TagsCloudSetting.GetDefault();
            var container = BuildContainer(setting);

            var tagCloudVisualizator = container.Resolve<TagCloudVisualizator>();
            tagCloudVisualizator.DrawTagCloud("InputFile.txt", "OutputFile.png", setting);

            
        }
        
//        public static void RunInteractiveMode()
//        {
//            while (true)
//            {
//                var line = Console.ReadLine();
//                if (line == null || line == "exit") return;
//                RunCommand(line.Split(' '));
//            }
//        }

//        private static void RunCommand(string[] split)
//        {
//            var options = ArgumentParser.ParseArguments(split);
//            if (options.Help)
//            {
//                Console.WriteLine(options.Help);
//            }
//            else
//            {
//                var container = BuildContainer();
//                var tagCloudVisualizator = container.Resolve<TagCloudVisualizator>();
//                tagCloudVisualizator.DrawTagCloud(options.InputFile, options.OutputFile);
//            }
//        }

        private static IContainer BuildContainer(TagsCloudSetting setting)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TokensParser>().As<ITokensParser>();
            builder.Register(c => setting).As<ICloudSetting>().SingleInstance();
            builder.Register(c => setting.GetCenterImage()).As<Point>();
            builder.RegisterType<BoringFilter>().As<IFilter>().WithParameter("boringWords",new string[0]);
            builder.RegisterType<SpiralGenerator>().As<IPointGenerator>();
            builder.RegisterType<FileManager.FileManager>().As<IFileManager>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleGenerator>();
            builder.RegisterType<TagCloudVisualizator>().AsSelf();
            
            return builder.Build();
        }
    }
}