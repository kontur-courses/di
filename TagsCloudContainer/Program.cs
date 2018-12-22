using System;
using System.Drawing;
using System.IO;
using Autofac;
using TagsCloudContainer.Generation;
using TagsCloudContainer.GettingTokens;
using TagsCloudContainer.Infrastructure.FileManaging;
using TagsCloudContainer.Infrastructure.PointTracks;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = new ArgumentsParser().ParseArguments(args);

            if (options.Help)
            {
                const string helpMessage = "Usage: -r (--read) <input file> -f (--font) " +
                                           "<font size> -o (--output) <output file>";
                Console.WriteLine(helpMessage);
            }
            else
            {
                GenerateCloud(options, BuildContainer());
            }
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Tokenizer>().As<ITokenizer>();
            builder.RegisterType<TagsCloudVisualizer>().As<IVisualizer>();
            builder.RegisterType<SpiralTrack>().As<IPointsTrack>();
            builder.RegisterType<TagsCloudGenerator>().AsSelf();
 
            return builder.Build();
        }

        private static void GenerateCloud(Options options, IContainer container)
        {
            if (TryGetText(out var text, options)
            && TryGenerateTagCloud(out var tagsCloud, text, options, container))
            {
                TrySaveImage(tagsCloud, options);
            }
        }

        private static void TrySaveImage(Bitmap tagsCloud, Options options)
        {
            try
            {
                new ImageSaver().Save(tagsCloud, options.OutputFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static bool TryGenerateTagCloud(out Bitmap tagsCloud, string text, 
            Options options, IContainer container)
        {
            try
            {
                tagsCloud = container.Resolve<TagsCloudGenerator>()
                    .GenerateTagsCloud(text, OptionsToSettings(options));

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tagsCloud = null;
                return false;
            }
        }

        private static bool TryGetText(out string text, Options options)
        {
            try
            {
                text = new TemporaryFileManager()
                    .ReadTextFromFile(options.InputFile);
                
                return true;
            }
            catch (FileNotFoundException exp)
            {
                Console.WriteLine($"Error file {exp.FileName} not found!");
                text = null;
                return false;
            }
        }

        private static TagsCloudSettings OptionsToSettings(Options options)
        {
            return new TagsCloudSettings
            (
                new Font(FontFamily.GenericSansSerif, options.FontSize),
                new Size(options.Size, options.Size)
            );
        }
    }
}