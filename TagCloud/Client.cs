using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TagCloud.IServices;
using TagCloud.Models;
using System.Windows.Forms;

namespace TagCloud
{
    public class Client : IClient
    {
        private readonly IAction[] actions;
        private readonly ClientConfig config;
        private readonly HashSet<string> availableFontNames;
        private HashSet<string> availablePaletteNames;
        private readonly IPaletteNamesFactory paletteNamesFactory;
        public Client(IAction[] actions, IPaletteNamesFactory paletteNamesFactory)
        {
            availableFontNames = new HashSet<string>()
            {
                "Arial",
                "Comic Sans MS"
            };
            config = new ClientConfig();
            this.actions = actions;
            this.paletteNamesFactory=paletteNamesFactory;
            availablePaletteNames = new HashSet<string>();
        }

        public void Start(ICloudVisualization visualization)
        {
            availablePaletteNames = paletteNamesFactory.GetPaletteNames(visualization);
            var actionsDictionary = actions.ToDictionary(a => a.CommandName, a => a);
            while (!config.ToExit)
            {
                config.ToCreateNewImage = false;
                var userSettings = ReadUserSettings();
                if(userSettings is null)
                    continue;
                config.ImageToSave = visualization.GetAndDrawRectangles(userSettings.ImageSettings, userSettings.PathToRead);
                while (!config.ToCreateNewImage)
                {
                    Console.WriteLine("Введите команду");
                    Console.WriteLine("Список доступных комманд :");
                    foreach (var action in actions)
                        Console.WriteLine($"{action.CommandName}     \"{action.Description}\"");
                    var command = Console.ReadLine().ToLower();
                    if (!actionsDictionary.ContainsKey(command))
                    {
                        Console.WriteLine("Unknown command");
                        continue;
                    }
                    actionsDictionary[command].Perform(config,userSettings.ImageSettings);
                }
            }
        }

        private UserSettings ReadUserSettings()
        {
            var defaultFontName = "Arial";
            var defaultPath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\test.txt";
            var defaultPaletteName = "ShadesOfBlue";
            Console.WriteLine("Введите ширину изображения");
            if (!int.TryParse(Console.ReadLine() ?? throw new ArgumentException(), out var width))
            {
                Console.WriteLine("Введенная ширина не является числом");
                return null;
            }
            Console.WriteLine("Введите высоту изображения");
            if (!int.TryParse(Console.ReadLine() ?? throw new ArgumentException(), out var height))
            {
                Console.WriteLine("Введенная высота не является числом");
                return null;
            }
            Console.WriteLine("Укажите путь к файлу с тегами");
            Console.WriteLine("Оставьте строку пустой, чтоб использовать путь: " + defaultPath);
            var pathToRead = Console.ReadLine();
            pathToRead = pathToRead == string.Empty
                ? defaultPath
                : pathToRead;
            Console.WriteLine("Введите шрифт");
            Console.WriteLine("Список доступных шрифтов :");
            foreach (var availableFontName in availableFontNames)
                Console.WriteLine(availableFontName);
            var fontName = Console.ReadLine();
            fontName = fontName == string.Empty
                ? defaultFontName
                : fontName;
            if (!availableFontNames.Contains(fontName))
            {
                Console.WriteLine("Введенный шрифт не поддерживается");
                return null;
            }
            Console.WriteLine("Введите название палитры");
            Console.WriteLine("Список доступных палитр :");
            foreach (var name in availablePaletteNames)
                Console.WriteLine(name);
            var palleteName = Console.ReadLine();
            palleteName = palleteName == string.Empty
                ? defaultPaletteName
                : palleteName;
            if (!availablePaletteNames.Contains(palleteName))
            {
                Console.WriteLine("Введенная палитра не поддерживается");
                return null;
            }
            return new UserSettings(new ImageSettings(width,height,fontName,palleteName),pathToRead);
        }
    }
}