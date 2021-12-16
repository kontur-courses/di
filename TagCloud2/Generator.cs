using System;
using System.Drawing;
using System.Reflection;
using Ninject;
using TagCloud2.Image;
using TagCloud2.Text;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;
using Ninject.Extensions.Conventions;
using System.Collections.Generic;
using Autofac;


namespace TagCloud2
{
    public static class GeneratorHelper
    {
        public static Dictionary<string, Action> dictionary;
        
        public static void Initialize(ContainerBuilder builder)
        {
            dictionary = new();
            dictionary.Add("txt", () => builder.RegisterType<TxtFileReader>().As<IFileReader>());
            dictionary.Add("docx", () => builder.RegisterType<DocxFileReader>().As<IFileReader>());
            dictionary.Add("png", () => builder.RegisterType<PngImageFormatter>().As<IImageFormatter>());
            dictionary.Add("jpeg", () => builder.RegisterType<JpegImageFormatter>().As<IImageFormatter>());
        }
    }

    public class Generator
    {
        public void Generate(IOptions options)
        {
            var builder = new ContainerBuilder();
            GeneratorHelper.Initialize(builder);
            builder.RegisterType<Core>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            var Spiral = new ArchimedeanSpiral(new Point(options.X/2, options.Y/2), options.AngleSpeed, options.LinearSpeed);
            builder.RegisterInstance(Spiral).As<ISpiral>();
            GeneratorHelper.dictionary[options.Format].Invoke();
            GeneratorHelper.dictionary[options.OutputFormat].Invoke();
            builder.RegisterType<LinesWordReader>().As<IWordReader>();
            builder.RegisterType<StringPreprocessor>().As<IStringPreprocessor>();
            builder.RegisterType<StringToRectangleConverter>().As<IStringToSizeConverter>();
            builder.RegisterType<ColoredCloud>().As<IColoredCloud>();
            builder.RegisterType<RandomColoringAlgorithm>().As<IColoringAlgorithm>();
            builder.RegisterType<FileGenerator>().As<IFileGenerator>();
            builder.RegisterType<ColoredCloudToBitmap>().As<IColoredCloudToImageConverter>();
            builder.RegisterType<SillyWordsRemover>().As<ISillyWordRemover>();
            builder.RegisterType<ShortWordsSelector>().As<ISillyWordSelector>();
            var core2 = builder.Build().Resolve<Core>();
            core2.Run(options);
        }
    }
}
