using System;
using TagCloud2.Image;
using TagCloud2.Text;
using System.Collections.Generic;
using Autofac;
using TagCloud2.Cloud;

namespace TagCloud2
{
    public class GeneratorHelper
    {
        private readonly Dictionary<string, Dictionary<object, Action>> dictionary;

        public GeneratorHelper(ContainerBuilder builder)
        {
            var inputDict = new Dictionary<object, Action>
            {
                { "txt", () => builder.RegisterType<TxtFileReader>().As<IFileReader>() },
                { "docx", () => builder.RegisterType<DocxFileReader>().As<IFileReader>() }
            };

            var outputDict = new Dictionary<object, Action>
            {
                { "png", () => builder.RegisterType<PngImageFormatter>().As<IImageFormatter>() },
                { "jpeg", () => builder.RegisterType<JpegImageFormatter>().As<IImageFormatter>() },
                { "bitmap", () => builder.RegisterType<BitmapImageFormatter>().As<IImageFormatter>() }
            };

            var colorDict = new Dictionary<object, Action>
            {
                { "random", () => builder.RegisterType<RandomColoringAlgorithm>().As<IColoringAlgorithm>() },
                { "bw", () => builder.RegisterType<WhiteColoringAlgorithm>().As<IColoringAlgorithm>() }
            };

            var boringDict = new Dictionary<object, Action>
            {
                { "exclude", () => builder.RegisterType<ExcludedWordsSelector>().As<ISillyWordSelector>() },
                { "short", () => builder.RegisterType<ShortWordsSelector>().As<ISillyWordSelector>() }
            };

            dictionary = new Dictionary<string, Dictionary<object, Action>>
            {
                { "InputFormat", inputDict },
                { "OutputFormat", outputDict },
                { "ColoringMode", colorDict },
                { "BoringMode", boringDict }
            };
        }

        public void RegisterTypes(IOptions options)
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
}
