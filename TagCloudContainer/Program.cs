using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Fclp;
using TagCloudContainer.Api;
using TagCloudContainer.fluent;
using TagCloudContainer.Implementations;

namespace TagCloudContainer
{
    class Program
    {
        public static Dictionary<string, Func<TagCloudConfig, IWordProvider>> wordProviders =
            new Dictionary<string, Func<TagCloudConfig, IWordProvider>>()
                {{"txt", cfg => new TxtFileReader(cfg.inputFile)}};

        public static Dictionary<string, Type> wordProcessors = new Dictionary<string, Type>()
            {{"lowercase", typeof(LowercaseWordProcessor)}};

        public static Dictionary<string, Type> cloudLayouters = new Dictionary<string, Type>()
            {{"circular", typeof(CircularCloudLayouter)}};

        public static Dictionary<string, Type> sizeProviders = new Dictionary<string, Type>()
            {{"default", typeof(StringSizeProvider)}};

        public static Dictionary<string, Type> brushProviders = new Dictionary<string, Type>()
            {{"onecolor", typeof(OneColorBrushProvider)}};

        public static Dictionary<string, Type> penProviders = new Dictionary<string, Type>()
            {{"onecolor", typeof(OneColorPenProvider)}};

        public static Dictionary<string, Type> wordCloudLayouters = new Dictionary<string, Type>()
            {{"default", typeof(WordCloudLayouter)}};

        public static Dictionary<string, Type> wordVisualizers = new Dictionary<string, Type>()
            {{"default", typeof(TagCloudVisualizer)}};

        public static Dictionary<string, ImageFormat> imageFormats = new Dictionary<string, ImageFormat>()
        {
            {"bmp", ImageFormat.Bmp}, {"emf", ImageFormat.Emf}, {"exif", ImageFormat.Exif},
            {"gif", ImageFormat.Gif}, {"icon", ImageFormat.Icon}, {"jpeg", ImageFormat.Jpeg},
            {"png", ImageFormat.Png}, {"tiff", ImageFormat.Tiff}, {"wmf", ImageFormat.Wmf},
            {"membmp", ImageFormat.MemoryBmp}
        };

        static void Main(string[] args)
        {
//            var inputFile = "words.txt";
//            var outputFile = "wordCloud.bmp";

            var p = new FluentCommandLineParser();
            TagCloudConfig config = null;
            p.Setup<string>('f', "file").Callback(file => config = CreateTagCloud.FromFile(file)).Required();
            p.Setup<string>("source-type").Callback(type => config.UsingWordProvider(wordProviders[type](config)));
            p.Setup<string>("processor").Callback(proc => config.UsingWordProcessor(wordProcessors[proc]));
            p.Setup<string>("layout").Callback(layout => config.UsingCloudLayouter(cloudLayouters[layout]));
            p.Setup<string>("wordlayouter")
                .Callback(layouter => config.UsingWordCloudLayouter(wordCloudLayouters[layouter]));
            p.Setup<string>("sizefunc").Callback(size => config.UsingStringSizeProvider(sizeProviders[size]));
            p.Setup<string>("brush").Callback(brush => config.UsingWordBrushProvider(brushProviders[brush]));
            p.Setup<string>("pen").Callback(pen => config.UsingRectanglePenProvider(penProviders[pen]));
            p.Setup<string>("visualizer")
                .Callback(visualizer => config.UsingWordVisualizer(wordVisualizers[visualizer]));
            p.Setup<string>("format").Callback(format => config.Using(imageFormats[format]));
            p.Setup<string>('o', "output").Callback(file => config.SaveToFile(file)).Required();

            p.Parse(args);
        }
    }
}