using Autofac;
using TagsCloudConsoleUI.DIPresetModules;

namespace TagsCloudConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager.Run(CloudPresetContainer);
        }

        static IContainer CloudPresetContainer(BuildOptions options)
        {
            return CloudBuilder.BuildPreset(
                new CircularRandomCloudModule(options),
                new WordParserWithYandexToolModule(options.BoringPartsOfSpeech.Split(' '),
                    options.BoringWords.Split(' ')),
                new BitmapImageCreatorModule());
        }
    }
}
