using System;
using System.Windows.Forms;
using Autofac;
using CommandLine;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class CloudWordsForm
    {
        private static IContainer container;

        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            Load(builder);
            container = builder.Build();
            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            var parameters = container.Resolve<CloudParameters>();
            var cloudParametersParser =
                container.Resolve<ICloudParametersParser>(new TypedParameter(typeof(CloudParameters), parameters));
            parameters = cloudParametersParser.Parse(options);
            parameters.PointGenerator = container.ResolveNamed<IPointGenerator>(options.PointGenerator,
                new TypedParameter(typeof(double), parameters.FactorStep),
                new TypedParameter(typeof(double), parameters.DegreeStep));
            var cloud = container.Resolve<ICloudLayouter>(new TypedParameter(typeof(IPointGenerator), parameters.PointGenerator));
            var wordDataHandler = container.Resolve<IWordDataProvider>();
            var data = wordDataHandler.GetData(cloud, options.FilePath);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }

        protected static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            builder.RegisterType<WordDataProvider>().As<IWordDataProvider>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter(new TypedParameter(typeof(IPointGenerator), "pointGenerator"));
            builder.RegisterType<Spiral>().Named<IPointGenerator>("spiral");
            builder.RegisterType<Heart>().Named<IPointGenerator>("heart");
            builder.RegisterType<Astroid>().Named<IPointGenerator>("astroid");
            builder.RegisterType<CloudParameters>();
        }
    }
}