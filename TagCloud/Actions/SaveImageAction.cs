using System;
using System.Drawing.Imaging;
using System.IO;
using TagCloud.Models;

namespace TagCloud.Actions
{
    public class SaveImageAction : IAction
    {
        public string CommandName { get; } = "-saveimage";

        public string Description { get; } = "save image";

        public void Perform(ClientConfig config, ImageSettings imageSettings)
        {
            Console.WriteLine("Введите путь для сохранения картинки");
            var path = Console.ReadLine();
            path = path == string.Empty ? Path.GetTempPath() + "\\image.png" : path;
            config.ImageToSave.Save(path, ImageFormat.Png);
            Console.WriteLine($"Файл сохранен в {path}");
        }
    }
}