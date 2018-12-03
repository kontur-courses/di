using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Curves;

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
            var cloudParametersParser = container.Resolve<ICloudParametersParser>();
            var parameters = cloudParametersParser.Parse(args);

            if (!parameters.IsCorrect())
                return;

            var cloud = container.Resolve<ICloudLayouter>(new TypedParameter(typeof(ICurve), parameters.Curve));
            var wordDataHandler = container.Resolve<IWordDataHandler>();
            var data = wordDataHandler.GetDatas(cloud);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }

        protected static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            builder.RegisterType<WordDataHandler>().As<IWordDataHandler>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter(new TypedParameter(typeof(ICurve), "curve"));
            builder.RegisterType<Spiral>().As<ICurve>();
            builder.RegisterType<Astroid>().As<ICurve>();
            builder.RegisterType<Heart>().As<ICurve>();
        }
    }
}