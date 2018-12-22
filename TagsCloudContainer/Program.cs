using System;
using System.Drawing;
using System.IO;
using TagsCloudContainer.Infrastructure.FileManaging;

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
                GenerateCloud(options);
        }

        private static void GenerateCloud(Options options)
        {
            if (TryGetText(out var text, options)
            && TryGenerateTagCloud(out var tagsCloud, text, options))
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
            Options options)
        {
            try
            {
                tagsCloud = new TagsCloudGenerator()
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