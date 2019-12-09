using System;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.Actions
{
    public class SaveImageAction : IAction
    {
        public string CommandName => "- saveimage";

        public void Perform(ClientConfig config)
        {
            Console.WriteLine("Введите путь для сохранения картинки");
            var path = Console.ReadLine();
            path = path == string.Empty ? Path.GetTempPath() + "\\image.png" : path;
            config.ImageToSave.Save(path, ImageFormat.Png);
            Console.WriteLine($"Файл сохранен в {path}");
        }
    }
}