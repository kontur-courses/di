using System.Drawing.Imaging;
using Autofac;
using CommandLine;
using TagCloudConsoleApplication.Options;
using TagCloudPainter.Builders;
using TagCloudPainter.FileReader;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;
using TagCloudPainter.Savers;

namespace TagCloudConsoleApplication;

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<TagCloudOptions>(args)
            .WithParsed(o =>
            {
                var container = new Configurator().Confiugre(o);


                var words = container.Resolve<IFileReader>().ReadFile(o.InputPath);
                var dictionary = container.Resolve<IWordPreprocessor>().GetWordsCountDictionary(words);
                var tags = container.Resolve<ITagCloudElementsBuilder>().GetTags(dictionary);
                var btm = container.Resolve<ICloudPainter>().PaintTagCloud(tags);
                var format = GetImageFormat(o.OutputPath);
                container.Resolve<ITagCloudSaver>().SaveTagCloud(btm, o.OutputPath, format);
            });
    }

    private static ImageFormat GetImageFormat(string output)
    {
        if (output.Contains(".png"))
            return ImageFormat.Png;
        if (output.Contains(".jpg") || output.Contains(".jpeg"))
            return ImageFormat.Jpeg;
        throw new ArgumentException("output is in not supported format", output);
    }
}