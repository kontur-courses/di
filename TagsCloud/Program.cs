using System;
using System.Drawing;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud.BitmapCreator;
using TagsCloud.ColoringAlgorithms;
using TagsCloud.FileReaders;
using TagsCloud.ImageConfig;
using TagsCloud.LayoutAlgorithms;
using TagsCloud.PointGenerator;
using TagsCloud.WordFilters;

namespace TagsCloud
{
    public static class Program
    {
        private static string _pathToInputFile;
        private static Color _backgroundColor;
        private static Color _textColor;
        private static FontFamily _fontFamily;
        private static string _format;
        private static Size _size;
        private static IServiceProvider serviceProvider;

        public static int Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption();

            var input = app.Option("-i|--input <INPUT>", "Input file path", CommandOptionType.SingleValue);
            var backgroundColor = app.Option<Color>("-b|--bgcolor <BGCOLOR>", "Background color",
                CommandOptionType.SingleValue);
            var textColor = app.Option<Color>("-t|--tagcolor <TAGCOLOR>", "Tags color", CommandOptionType.SingleValue);
            var font = app.Option("-f|--font <FONT>", "Font family", CommandOptionType.SingleValue);
            var format = app.Option("-r|--format <FORMAT>", "Result image format", CommandOptionType.SingleValue);
            var size = app.Option("-s|--size <SIZE>", "Result image size", CommandOptionType.SingleValue);

            app.OnExecute(() =>
                {
                    _pathToInputFile = ArgumentParser.CheckFilePath(input.Value());

                    _backgroundColor = backgroundColor.HasValue()
                        ? ArgumentParser.GetColor(backgroundColor.Value())
                        : Color.Bisque;

                    _textColor = textColor.HasValue()
                        ? ArgumentParser.GetColor(textColor.Value())
                        : Color.Aquamarine;

                    _fontFamily = font.HasValue()
                        ? ArgumentParser.GetFontFamily(font.Value())
                        : new FontFamily("Arial");

                    _format = format.HasValue() && ArgumentParser.IsCorrectFormat(format.Value())
                        ? format.Value()
                        : "png";

                    _size = size.HasValue()
                        ? ArgumentParser.GetSize(size.Value())
                        : new Size(1200, 1200);

                    return 0;
                }
            );

            app.Execute(args);

            ConfigureServices();

            var text = serviceProvider.GetService<IFileReader>()?.GetWordsFromFile(_pathToInputFile);
            var words = serviceProvider.GetService<IWordFilter>()?.FilterWords(text).Select(x => x.ToLower());
            var image = serviceProvider.GetService<IBitmapCreator>()?.Create(words);
            BitmapSaver.BitmapSaver.Save(image, _format);

            return 0;
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IWordFilter>(new PartsOfSpeechFilter(PartsOfSpeech.ADVPRO, PartsOfSpeech.APRO,
                PartsOfSpeech.CONJ));
            services.AddSingleton<ILayoutAlgorithm>(
                new CircularCloudLayouter(new Point(_size.Width / 2, _size.Height / 2)));
            services.AddSingleton<IColoringAlgorithm>(new DefaultColoringAlgorithm(_textColor));
            services.AddSingleton<IImageConfig>(new ImageConfig.ImageConfig(_size, _fontFamily, _backgroundColor,
                new DefaultColoringAlgorithm(_textColor)));
            services.AddSingleton<IPointGenerator, ArchimedeanSpiral>();
            services.AddSingleton(ChooseReader(services));
            services.AddSingleton<IBitmapCreator, BitmapCreator.BitmapCreator>();
            serviceProvider = services.BuildServiceProvider();
        }

        private static IFileReader ChooseReader(ServiceCollection serviceCollection)
        {
            if (_pathToInputFile.EndsWith(".txt"))
                return new TxtFileReader();

            if (_pathToInputFile.EndsWith(".docx"))
                return new DocxFileReader();

            throw new ArgumentException("Unsupported file format");
        }
    }
}

