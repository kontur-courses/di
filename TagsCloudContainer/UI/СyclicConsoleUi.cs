using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine.Text;
using TagsCloudContainer.ImageCreator;
using TagsCloudContainer.UI.SettingsCommands;

namespace TagsCloudContainer.UI
{
    public class CyclicConsoleUI : IUserInterface
    {
        private readonly ISettingsCommand[] settingsCommands;
        private readonly IImageCreator imageCreator;

        private readonly string helpText = @"поддерживаемые команды:

input <input_path> - Настраивает файл с текстом

output <output_path> - Имя файла для итогового изображения

size <width> <height> - Настраивает размер изображения

draw - создает картинку

exit - закрывает программу
";

        public CyclicConsoleUI(ISettingsCommand[] settingsCommands, IImageCreator imageCreator)
        {
            this.imageCreator = imageCreator;
            this.settingsCommands = settingsCommands;
        }

        private Result<IInitialSettings> TryChangeSettings(string input, IInitialSettings settings)
        {
            foreach (var command in settingsCommands)
            {
                if (!command.IsThisCommandInput(input, out var arguments))
                    continue;
                var result = command.TryChangeSettings(arguments, settings);
                return result;
            }

            return Result.Fail<IInitialSettings>("Unknown command");
        }

        public void Run(IEnumerable<string> args)
        {
            Console.WriteLine(helpText);
            IInitialSettings settings = new InitialSettings();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                    continue;
                if (input.Length == 0)
                {
                    Console.WriteLine("Empty input, please input command");
                    continue;
                }
                if (input == "help")
                {
                    Console.WriteLine(helpText);
                    continue;
                }
                if (input == "exit")
                    break;
                if (input == "draw")
                {
                    if (settings.InputFilePath == null || settings.OutputFilePath == null)
                        Console.WriteLine("Please enter paths for input and output files");
                    else
                    {
                        imageCreator.CreateImage(settings);
                        Console.WriteLine("Image saved");
                    }
                    continue;
                }
                var result = TryChangeSettings(input, settings);
                if (result.IsSuccess)
                    settings = result.Value;
                else
                    Console.WriteLine(result.Error);
            }
        }
    }
}
