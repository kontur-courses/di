using System;
using System.IO;
using System.Linq;
using TagCloud.IServices;

namespace TagCloud
{
    public class Client : IClient
    {
        private readonly IAction[] actions;
        private readonly ClientConfig config;

        public Client(IAction[] actions)
        {
            config = new ClientConfig();
            this.actions = actions;
        }

        public void Start(ICloudVisualization visualization)
        {
            var actionsDictionary = actions.ToDictionary(a => a.CommandName, a => a);
            while (!config.ToExit)
            {
                config.ToCreateNewImage = false;
                Console.WriteLine("Введите ширину изображения");
                var width = int.Parse(Console.ReadLine() ?? throw new ArgumentException());
                Console.WriteLine("Введите высоту изображения");
                var height = int.Parse(Console.ReadLine() ?? throw new ArgumentException());
                Console.WriteLine("Укажите путь к файлу с тегами");
                var pathToRead = Console.ReadLine();
                pathToRead = pathToRead == string.Empty
                    ? $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\test.txt"
                    : pathToRead;
                config.ImageToSave = visualization.GetAndDrawRectangles(width, height, pathToRead);
                while (!config.ToCreateNewImage)
                {
                    Console.WriteLine("Введите команду");
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
    }
}