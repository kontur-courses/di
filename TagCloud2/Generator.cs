using System.Drawing;
using TagCloud2.Image;
using TagCloud2.Text;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;
using Autofac;

namespace TagCloud2
{
    public class Generator
    {
        public void Generate(IOptions options)
        {
            var builder = new ContainerBuilder();
            var generatorHelper = new GeneratorHelper(builder);
            generatorHelper.RegisterTypes(options);
            builder.RegisterType<InnerCoreLogic>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            var spiral = new ArchimedeanSpiral(new Point(options.X/2, options.Y/2), options.AngleSpeed, options.LinearSpeed);
            builder.RegisterInstance(spiral).As<ISpiral>();
            builder.RegisterType<LinesWordReader>().As<IWordReader>();
            builder.RegisterType<StringPreprocessor>().As<IStringPreprocessor>();
            builder.RegisterType<StringToRectangleConverter>().As<IStringToSizeConverter>();
            builder.RegisterType<ColoredCloud>().As<IColoredCloud>();
            builder.RegisterType<FileGenerator>().As<IFileGenerator>();
            builder.RegisterType<ColoredCloudToBitmap>().As<IColoredCloudToImageConverter>();
            builder.RegisterType<SillyWordsFilter>().As<ISillyWordsFilter>();
            builder.RegisterInstance(new ExcludedWordsPath() { Path = options.ExcludePath }).As<ExcludedWordsPath>();
            var core2 = builder.Build().Resolve<InnerCoreLogic>();
            core2.Run(options);
        }
    }
}
