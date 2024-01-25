using System.Drawing;
using TagsCloudContainer.SettingsClasses;

namespace TagsCloudContainer.CLI
{
    public static class CommandLineArgs
    {
        public static void PrintUsage()
        {
            Console.WriteLine(@"Usage: TagCloudContainer.exe <fileName> [-font <fontName>] [-fontsize <fontSize>] [-color <colorName>] [-size <Width> <Height>]

Options:
    -font <FontName>: Set the font family name. Default is Arial.
    -fontsize <FontSize>: Set the font size. Must be a positive integer.
    -color <ColorName>: Add a color to the list of allowed colors.
    -size <Width> <Height> : Set the image width. Must be two positive integer separated by whitespace.
    -layout <Layout> : Set cloud layouter - Spiral or Random. Default is Spiral.
    -out <outputImage> : Set path for output image.
");
        }

        public static (CloudDrawingSettings, AppSettings) CreateSettingsObject(IReadOnlyCollection<string> args)
        {
            if (args.Count < 1) throw new ArgumentException("At least one argument must be provided.", "args");

            var settings = new CloudDrawingSettings();

            var appSettings = new AppSettings();

            return (settings, appSettings);
        }

        public static void ParseCommandLineArguments(CloudDrawingSettings settings, AppSettings appSettings, string[] args)
        {
            HandleTextFileOption(args, ref appSettings);

            for (int i = 1; i < args.Length; i += 2)
            {
                HandleFontOption(args, ref settings, i);
                HandleFontSizeOption(args, ref settings, i);
                HandleColorOption(args, ref settings, i);
                HandleSizeOption(args, ref settings, i);
                HandleOutputFileOption(args, ref appSettings, i);
            }
        }

        private static void HandleOutputFileOption(string[] args, ref AppSettings appSettings, int index)
        {
            const string OptionName = "-out";

            if (args[index] != OptionName) return;

            if (index + 1 >= args.Length || string.IsNullOrEmpty(args[index + 1]))
            {
                Console.WriteLine($"The '-out' option requires a valid file name.");
                return;
            }
            appSettings.outImagePath = args[++index];


        }

        private static void HandleTextFileOption(string[] args, ref AppSettings appSettings)
        {
            appSettings.textFile = args[0];
        }

        private static void HandleFontOption(string[] args, ref CloudDrawingSettings settings, int index)
        {
            const string OptionName = "-font";

            if (args[index] != OptionName) return;

            if (index + 1 >= args.Length || string.IsNullOrEmpty(args[index + 1]))
            {
                Console.WriteLine($"The '-font' option requires a valid font family name.");
                return;
            }

            settings.FontFamily = new FontFamily(args[++index]);
        }

        private static void HandleFontSizeOption(string[] args, ref CloudDrawingSettings settings, int index)
        {
            const string OptionName = "-fontsize";

            if (args[index] != OptionName) return;

            if (index + 1 >= args.Length || !int.TryParse(args[index + 1], out _))
            {
                Console.WriteLine($"The '-fontsize' option requires a valid integer value.");
                return;
            }

            if (int.Parse(args[++index]) <= 0)
            {
                Console.WriteLine($"The '-fontsize' option requires a positive integer value.");
                return;
            }

            settings.FontSize = int.Parse(args[index]);
        }

        private static void HandleColorOption(string[] args, ref CloudDrawingSettings settings, int index)
        {
            const string OptionName = "-color";

            if (args[index] != OptionName) return;

            if (index + 1 >= args.Length) // || !Color.TryParse(args[index + 1], out _))
            {
                Console.WriteLine("The '-color' option requires a valid color value.");
                return;
            }

            settings.Colors.Add(Color.FromName(args[++index]));
        }

        private static void HandleSizeOption(string[] args, ref CloudDrawingSettings settings, int index)
        {
            const string OptionName = "-size";

            if (args[index] != OptionName) return;
            if (index + 2 >= args.Length || !int.TryParse(args[index + 1], out _) || !int.TryParse(args[index + 2], out _))
            {
                Console.WriteLine($"The '-size' option requires two valid integer value.");
                return;
            }

            if (int.Parse(args[index + 1]) <= 0 || int.Parse(args[index + 2]) <= 0)
            {
                Console.WriteLine($"The '-size' option requires two positive integer value.");
                return;
            }

            settings.Size = new Size(int.Parse(args[++index]), int.Parse(args[++index]));
        }
    }
}
