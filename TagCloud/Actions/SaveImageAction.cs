using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.Actions
{
    public class SaveImageAction : IAction
    {
        private readonly Dictionary<string, ImageFormat> namesFormatsToSave;

        public SaveImageAction()
        {
            namesFormatsToSave = new Dictionary<string, ImageFormat>();
            namesFormatsToSave["jpg"] = ImageFormat.Jpeg;
            namesFormatsToSave["png"] = ImageFormat.Png;
            namesFormatsToSave["bmp"] = ImageFormat.Bmp;
        }

        public string CommandName { get; } = "-saveimage";

        public string Description { get; } = "save image";

        public void Perform(ClientConfig config)
        {
            Console.WriteLine("Введите путь для сохранения картинки");
            Console.WriteLine("Оставьте строку пустой, чтоб сохранить в: " + Path.GetTempPath());
            Console.Write(">>>");
            var path = Console.ReadLine();
            var imageFormat = tryReadFormat();
            if (imageFormat is null) return;
            path = path == string.Empty ? Path.GetTempPath() + "\\image." + imageFormat : path;
            config.ImageToSave.Save(path, imageFormat);
            Console.WriteLine($"Файл сохранен в {path}");
        }

        private ImageFormat tryReadFormat()
        {
            var defaultFormaName = "png";
            Console.WriteLine("Введите формат в котором хотите сохранить");
            Console.WriteLine("Список доступных форматов :");
            foreach (var formatName in namesFormatsToSave) Console.WriteLine(formatName);
            Console.WriteLine("Оставьте строку пустой, чтоб использовать формат : " + defaultFormaName);
            Console.Write(">>>");
            var name = Console.ReadLine();
            name = name == string.Empty
                ? defaultFormaName
                : name;
            if (namesFormatsToSave.ContainsKey(name)) return namesFormatsToSave[name];
            Console.WriteLine("Введенный формат не поддерживается");
            return null;
        }
    }
}