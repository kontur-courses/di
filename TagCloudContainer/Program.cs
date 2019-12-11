using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using Fclp;
using TagCloudContainer.Api;
using TagCloudContainer.fluent;
using TagCloudContainer.Implementations;

namespace TagCloudContainer
{
    internal class Program
    {
        public static Dictionary<string, Func<TagCloudConfig, IWordProvider>> wordProviders =
            new Dictionary<string, Func<TagCloudConfig, IWordProvider>>
                {{"txt", cfg => new TxtFileReader(cfg.InputFile)}};

        public static Dictionary<string, ImageFormat> imageFormats = new Dictionary<string, ImageFormat>
        {
            {"bmp", ImageFormat.Bmp}, {"emf", ImageFormat.Emf}, {"exif", ImageFormat.Exif},
            {"gif", ImageFormat.Gif}, {"icon", ImageFormat.Icon}, {"jpeg", ImageFormat.Jpeg},
            {"png", ImageFormat.Png}, {"tiff", ImageFormat.Tiff}, {"wmf", ImageFormat.Wmf},
            {"membmp", ImageFormat.MemoryBmp}
        };

        public static Dictionary<string, Type> cliElements = new Dictionary<string, Type>();

        private static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();
            TagCloudConfig config = null;
            p.Setup<string>('f', "file").Callback(file => config = CreateTagCloud.FromFile(file)).Required();
            p.Setup<string>('p').Callback(useParameters =>
            {
                if (useParameters.ToLower().Equals("true"))
                    CollectCliElements();
            });
            p.Setup<string>("source-type").Callback(type => config.WordProvider = wordProviders[type](config));
            p.Setup<string>("processor").Callback(proc => config.WordProcessor = cliElements[proc]);
            p.Setup<string>("layout").Callback(layout => config.CloudLayouter = cliElements[layout]);
            p.Setup<string>("wordlayouter")
                .Callback(layouter => config.WordCloudLayouter = cliElements[layouter]);
            p.Setup<string>("sizefunc").Callback(size => config.SizeProvider = cliElements[size]);
            p.Setup<string>("brush").Callback(brush => config.BrushProvider = cliElements[brush]);
            p.Setup<string>("pen").Callback(pen => config.PenProvider = cliElements[pen]);
            p.Setup<string>("visualizer")
                .Callback(visualizer => config.WordVisualizer = cliElements[visualizer]);
            p.Setup<string>("format").Callback(format => config.ImageFormat = imageFormats[format]);
            p.Setup<string>("size").Callback(size => config.SetSize(size));
            p.Setup<string>('o', "output").Callback(file => config.SaveToFile(file)).Required();

            p.Parse(args);
        }

        private static void CollectCliElements()
        {
            var cliAttributeType = typeof(CliElementAttribute);

            var assembly = Assembly.GetAssembly(cliAttributeType);
            foreach (var type in assembly.DefinedTypes.Where(t =>
                t.GetCustomAttributes(cliAttributeType).Any()))
            {
                var attributes = type.GetCustomAttributes(cliAttributeType).Select(t => (CliElementAttribute) t);
                var attribute = attributes.First();
                cliElements.Add(attribute.CliName, attribute.TargetType);
            }
        }
    }
}