using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using TagCloudContainer.Api;
using TagCloudContainer.Implementations;

namespace TagCloudContainer.fluent
{
    public class TagCloudConfig
    {
        public readonly string InputFile;
        public Type BrushProvider;
        public Type CloudLayouter;
        public ImageFormat ImageFormat;
        public DrawingOptions Options;
        public Type PenProvider;
        public Type SizeProvider;
        public Type WordCloudLayouter;
        public Type WordProcessor;
        public IWordProvider WordProvider;
        public Type WordVisualizer;

        private TagCloudConfig(TagCloudConfig parent)
        {
            InputFile = parent.InputFile;
            WordProvider = parent.WordProvider;
            WordProcessor = parent.WordProcessor;
            WordVisualizer = parent.WordVisualizer;
            CloudLayouter = parent.CloudLayouter;
            WordCloudLayouter = parent.WordCloudLayouter;
            SizeProvider = parent.SizeProvider;
            BrushProvider = parent.BrushProvider;
            ImageFormat = parent.ImageFormat;
            Options = parent.Options;
        }

        public TagCloudConfig(string inputFile)
        {
            InputFile = inputFile;
            ImageFormat = ImageFormat.Png;
            Options = new DrawingOptions();
            WordProvider = new TxtFileReader(inputFile);
            WordProcessor = typeof(LowercaseWordProcessor);
            CloudLayouter = typeof(CircularCloudLayouter);
            SizeProvider = typeof(StringSizeProvider);
            BrushProvider = typeof(OneColorBrushProvider);
            PenProvider = typeof(OneColorPenProvider);
            WordCloudLayouter = typeof(WordCloudLayouter);
            WordVisualizer = typeof(TagCloudVisualizer);
        }

        public void SetSize(string size)
        {
            var sizes = size.Split('x').Select(int.Parse).ToList();
            Options.ImageSize = new Size(sizes[0], sizes[1]);
        }

        public void SaveToFile(string file)
        {
            var container = PrepareContainer();
            var bitmap = container.Resolve<Image>();
            bitmap.Save(file, ImageFormat);
        }

        private IContainer PrepareContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => WordProvider).As<IWordProvider>().SingleInstance();
            builder.RegisterType(WordProcessor).As<IWordProcessor>().SingleInstance();

            builder.Register(c =>
            {
                var words = c.Resolve<IWordProvider>().GetWords();
                return c.Resolve<IWordProcessor>().Process(words);
            }).As<IEnumerable<string>>();

            builder.RegisterType(CloudLayouter).As<ICloudLayouter>().SingleInstance();
            builder.RegisterType(WordCloudLayouter).As<IWordCloudLayouter>().SingleInstance();
            builder.RegisterType(SizeProvider).As<IStringSizeProvider>().SingleInstance();
            builder.RegisterType(BrushProvider).As<IWordBrushProvider>().SingleInstance();
            builder.RegisterType(PenProvider).As<IRectanglePenProvider>().SingleInstance();

            builder.Register(c => Options).As<DrawingOptions>().SingleInstance();
            builder.Register(c => c.Resolve<DrawingOptions>().Font).As<Font>().SingleInstance();

            builder.RegisterType(WordVisualizer).As<IWordVisualizer>();

            builder.Register(c => c.Resolve<IWordVisualizer>().CreateImageWithWords(
                c.Resolve<IEnumerable<string>>())).As<Image>();
            return builder.Build();
        }
    }
}