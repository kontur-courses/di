using System;
using System.Windows.Forms;
using Autofac;
using FluentAssertions;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CloudWordsForm
    {
        private static void Main(string[] args)
        {
            var kernel = new ContainerBuilder();
            kernel.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            kernel.RegisterType<WordDataHandler>().As<IWordDataHandler>();
            kernel.RegisterType<CircularCloudLayouter>().UsingConstructor(typeof(ICurve)).As<ICloudLayouter>();
            kernel.RegisterType<Spiral>().As<ICurve>();
            kernel.RegisterType<Astroid>().As<ICurve>();
            kernel.RegisterType<Heart>().As<ICurve>();
            var container = kernel.Build();

            var cloudParametersParser = container.Resolve<ICloudParametersParser>();
            var parameters = cloudParametersParser.Parse(args);

            if (!parameters.IsCorrect())
                return;

            var cloud = container.Resolve<ICloudLayouter>();
            var wordDataHandler = container.Resolve<IWordDataHandler>();
            var data = wordDataHandler.GetDatas(cloud);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }
    }
}