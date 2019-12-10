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
            this.paletteNamesFactory = paletteNamesFactory;
            availablePaletteNames = new HashSet<string>();
        }

        public void Start(ICloudVisualization visualization)
        {
            availablePaletteNames = paletteNamesFactory.GetPaletteNames(visualization);
            var actionsDictionary = actions.ToDictionary(a => a.CommandName, a => a);
            while (!config.ToExit)
            {
                config.ToCreateNewImage = false;
                var userSettings = TryReadUserSettings();
                if (userSettings is null)
                    continue;
                config.ImageToSave = visualization.GetAndDrawRectangles(userSettings.ImageSettings, userSettings.PathToRead);
                Console.WriteLine("Список доступных комманд :");
                foreach (var action in actions)
                    Console.WriteLine($"{action.CommandName}     \"{action.Description}\"");
                while (!config.ToCreateNewImage)
                {
                    Console.WriteLine("Введите команду");
                    Console.Write(">>>");
                    var command = Console.ReadLine().ToLower();
                    if (!actionsDictionary.ContainsKey(command))
                    {
                        Console.WriteLine("Unknown command");
                        continue;
                    }
                    actionsDictionary[command].Perform(config);
                }
            }
        }

        private UserSettings TryReadUserSettings()
        {
            if (!TryReadWidth(out var width) || !TryReadHeight(out var height)
                || !TryReadFontName(out var fontName) || !TryReadPaletteName(out var paletteName)) return null;
            var pathToRead = ReadPathToRead();
            return new UserSettings(new ImageSettings(width, height, fontName, paletteName), pathToRead);
        }

        private static bool TryReadWidth(out int width)
        {
            Console.WriteLine("Введите ширину изображения");
            Console.Write(">>>");
            if (int.TryParse(Console.ReadLine() ?? throw new ArgumentException(), out width)) return true;
            Console.WriteLine("Введенная ширина не является числом");
            return false;
        }

        private static bool TryReadHeight(out int height)
        {
            Console.WriteLine("Введите высоту изображения");
            Console.Write(">>>");
            if (int.TryParse(Console.ReadLine() ?? throw new ArgumentException(), out height)) return true;
            Console.WriteLine("Введенная высота не является числом");
            return false;
        }

        private static string ReadPathToRead()
        {
            var defaultPath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\test.txt";
            Console.WriteLine("Укажите путь к файлу с тегами");
            Console.WriteLine("Оставьте строку пустой, чтоб использовать путь: " + defaultPath);
            Console.Write(">>>");
            var pathToRead = Console.ReadLine();
            return pathToRead == string.Empty
                ? defaultPath
                : pathToRead;
        }

        private bool TryReadFontName(out string fontName)
        {
            var defaultFontName = "Arial";
            Console.WriteLine("Введите шрифт");
            Console.WriteLine("Список доступных шрифтов :");
            foreach (var availableFontName in availableFontNames)
                Console.WriteLine(availableFontName);
            Console.WriteLine("Оставьте строку пустой, чтоб использовать шрифт: " + defaultFontName);
            Console.Write(">>>");
            fontName = Console.ReadLine();
            fontName = fontName == string.Empty
                ? defaultFontName
                : fontName;
            if (availableFontNames.Contains(fontName)) return true;
            Console.WriteLine("Введенный шрифт не поддерживается");
            return false;
        }

        private bool TryReadPaletteName(out string paletteName)
        {
            var defaultPaletteName = "ShadesOfBlue";
            Console.WriteLine("Введите название палитры");
            Console.WriteLine("Список доступных палитр :");
            foreach (var name in availablePaletteNames)
                Console.WriteLine(name);
            Console.WriteLine("Оставьте строку пустой, чтоб использовать палитру: " +defaultPaletteName);
            Console.Write(">>>");
            paletteName = Console.ReadLine();
            paletteName = paletteName == string.Empty
                ? defaultPaletteName
                : paletteName;
            if (availablePaletteNames.Contains(paletteName)) return true;
            Console.WriteLine("Введенная палитра не поддерживается");
            return false;

        }

    }
}