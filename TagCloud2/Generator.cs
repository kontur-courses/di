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
using TagCloud2.Cloud;

namespace TagCloud2
{
    public static class GeneratorHelper
    {
        public static Dictionary<string, Dictionary<object, Action>> dictionary;
        
        public static void Initialize(ContainerBuilder builder)
        {
            dictionary = new();

            var inputDict = new Dictionary<object, Action>();
            inputDict.Add("txt", () => builder.RegisterType<TxtFileReader>().As<IFileReader>());
            inputDict.Add("docx", () => builder.RegisterType<DocxFileReader>().As<IFileReader>());
            dictionary.Add("InputFormat", inputDict);

            var outputDict = new Dictionary<object, Action>();
            outputDict.Add("png", () => builder.RegisterType<PngImageFormatter>().As<IImageFormatter>());
            outputDict.Add("jpeg", () => builder.RegisterType<JpegImageFormatter>().As<IImageFormatter>());
            dictionary.Add("OutputFormat", outputDict);

            var colorDict = new Dictionary<object, Action>();
            colorDict.Add("random", () => builder.RegisterType<RandomColoringAlgorithm>().As<IColoringAlgorithm>());
            colorDict.Add("bw", () => builder.RegisterType<WhiteColoringAlgorithm>().As<IColoringAlgorithm>());
            dictionary.Add("ColoringMode", colorDict);

            var boringDict = new Dictionary<object, Action>();
            boringDict.Add("exclude", () => builder.RegisterType<ExcludedWordsSelector>().As<ISillyWordSelector>());
            boringDict.Add("short", () => builder.RegisterType<ShortWordsSelector>().As<ISillyWordSelector>());
            dictionary.Add("BoringMode", boringDict);
        }

        public static void RegisterTypes(ContainerBuilder builder, IOptions options)
        {
            var type = options.GetType();
            var props = type.GetProperties();
            foreach (var property in props)
            {
                if (dictionary.ContainsKey(property.Name))
                {
                    try
                    {
                        dictionary[property.Name][property.GetValue(options)].Invoke();
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Some of parameters are incorrect!\n" + e.Message);
                    }
                }
            }
        }
    }

    public class Generator
    {
        public void Generate(IOptions options)
        {
            var builder = new ContainerBuilder();
            GeneratorHelper.Initialize(builder);
            GeneratorHelper.RegisterTypes(builder, options);
            builder.RegisterType<Core>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            var Spiral = new ArchimedeanSpiral(new Point(options.X/2, options.Y/2), options.AngleSpeed, options.LinearSpeed);
            builder.RegisterInstance(Spiral).As<ISpiral>();
            builder.RegisterType<LinesWordReader>().As<IWordReader>();
            builder.RegisterType<StringPreprocessor>().As<IStringPreprocessor>();
            builder.RegisterType<StringToRectangleConverter>().As<IStringToSizeConverter>();
            builder.RegisterType<ColoredCloud>().As<IColoredCloud>();
            builder.RegisterType<FileGenerator>().As<IFileGenerator>();
            builder.RegisterType<ColoredCloudToBitmap>().As<IColoredCloudToImageConverter>();
            builder.RegisterType<SillyWordsRemover>().As<ISillyWordRemover>();
            builder.RegisterInstance(new ExcludedWordsPath() { Path = options.ExcludePath }).As<ExcludedWordsPath>();
            var core2 = builder.Build().Resolve<Core>();
            core2.Run(options);
        }
    }
}
