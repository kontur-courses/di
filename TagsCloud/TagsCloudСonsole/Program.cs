using Autofac;
using DocoptNet;

namespace TagsCloudConsole
{
    internal static class Program
    {
        private const string Usage = @"Sc222's Tags Cloud Generator.

    Usage:
      TagsCloudConsole.exe generate --input FILE [--input_ext FILE_EXT] [--exclude EXCLUDE_FILE] [--split_pattern PATTERN]
                           [--shuffle SHUFFLE] [--seed SEED] [--font FONT] [--font_size FONT_SIZE] [--theme THEME] [--colorize METHOD]
                           --x X --y Y [--rad RADIUS] [--incr INCREMENT] [--angle ANGLE]  --w WIDTH --h HEIGHT --output IMAGE [--output_ext IMAGE_EXT] 
      TagsCloudConsole.exe (-h | --help)
      TagsCloudConsole.exe --version

    Options:
      -h --help                Show help screen
      --version                Show application version
      --input FILE             Path to input file (with extension)
      --input_ext FILE_EXT     Input file extension (docx, txt or pdf) [default: txt]
      --exclude EXCLUDE_FILE   File that contains words that need to be excluded in tag cloud [default: none]
      --split_pattern PATTERN  Regex pattern for splitting text into words[default: \W+]
      --shuffle SHUFFLE        Token shuffle type (a - ascending (smallest in centre), d - descending (a reversed), r - random)[default: r]
      --seed SEED              Random seed ONLY for random token shuffler[default: Environment.TickCount]
      --font FONT              Name of the font used for tag rendering[default: Arial]
      --font_size FONT_SIZE    Minimum size of tag font[default: 20]
      --theme THEME            Name of tag cloud theme (gr - graydark, go - godotengine, p - pixelart)[default: p]
      --colorize METHOD        Choose tag colorizing method (r - random, s - by tag size)[default: r]
      --x X                    Layouter center x[default: 0]
      --y Y                    Layouter center y[default: 0]
      --rad RADIUS             SpiralLayouter start radius[default: 0.5]
      --incr INCREMENT         SpiralLayouter increment[default: 0.5]
      --angle ANGLE            SpiralLayouter start angle increment[default: 0]
      --w WIDTH                Width of the result image
      --h HEIGHT               Height of the result image
      --output IMAGE           Path to output file (without extension)
      --output_ext IMAGE_EXT   Image extension[default: png]
    ";

        private const string Version = "Tag Cloud Generator 2.0";

        public static void Main(string[] args)
        {
            var parameters = new Docopt().Apply(Usage, args, version: Version, exit: true);
            var container = ContainerConfigurator.Configure(parameters);
            container.Resolve<Application>().Run();
        }
    }
}