using System;
using Autofac;
using DocoptNet;
using TagsCloudVisualization.ImageSaver;

namespace TagsCloudVisualization
{
    internal class Program
    {
        private const string Usage = @"Tags Cloud Visualizer

    Usage:
        TagsCloudVisualization.exe 
            [--input FILE] [--max_words MAX_WORD_COUNT] [--exclude BADWORDS_FILE] 
            [--sizer SIZE_SELECTOR]
            [--brush TEXT_BRUSH] [--back BACKGOUND_COLOR] 
            [--font FONT] [--font_size FONT_SIZE]
            [--width WIDTH] [--height HEIGHT]
            [--x X] [--y Y] 
            [--coef SPIRAL_COEF] [--type SPIRAL_TYPE] 
            [--output_path PATH] [--output_ext EXT]   

        TagsCloudVisualization.exe (-h | --help)
        TagsCloudVisualization.exe --version

    Options:
              -h --help                Show help screen
              --version                Show application version
              --input FILE             Path to input file (with extension)  [default: ] if path is emty getting from resources
              --max_words MAX_WORD_COUNT   Max words count in cloud [default: 30]
              --exclude EXCLUDE_FILE   File witch contains excluded words [default: ] if path is emty getting from resources
              --sizer SIZE_SELECTOR    Type of size selector ( Log or Sqrt or SqrtL) [default: Log]
              --brush TEXT_BRUSH       Text brush for cloud[default: DarkOrange]
              --back BACKGOUND_COLOR   Color for background[default: Gray]
              --font FONT              Name of the font used for text in cloud[default: Arial]
              --font_size FONT_SIZE    Starting size of tag font[default: 1]
              --width WIDTH            Width of the result image[default: 10000]
              --height HEIGHT          Height of the result image[default: 10000]
              --x X                    Layouter center x[default: 0]
              --y Y                    Layouter center y[default: 0]
              --coef SPIRAL_COEF       SpiralLayouter coefficient [default: 10]
              --type SPIRAL_TYPE       SpiralLayouter type InLine Rectangle or Ferma[default: Ferma]
              --output_path PATH       Path to output directory [default: ] if path is emty saving to resources
              --output_ext EXT         Image extension[default: Jpg]
    ";

        private const string Version = "Tags Cloud Visualizer 1.0";

        internal static void Main(string[] args)
        {
            var container = ContainerProvider.Build();

            var parameters = new Docopt().Apply(Usage, args, version: Version, exit: true);
            var appSettingsResult = SettingsParser.GetSettings(parameters);
            if (!appSettingsResult.IsSuccess)
            {
                Console.WriteLine(appSettingsResult.Error);
                return;
            }

            var appSettings = appSettingsResult.Value;

            var bitmap = container.Resolve<TagCreator>().DrawTag(appSettings.ReaderSettings, appSettings.DrawerSettings,
                appSettings.LayouterSettings);
            if (!bitmap.IsSuccess)
            {
                Console.WriteLine(bitmap.Error);
                return;
            }

            var saveResult = container.Resolve<IImageSaverFactory>().GetSaver(appSettings.ImageExt)
                .Save(bitmap.Value, appSettings.SavePath);
            if (!saveResult.IsSuccess)
            {
                Console.WriteLine(saveResult.Error);
            }

            Console.WriteLine("Ended successful");
        }
    }
}