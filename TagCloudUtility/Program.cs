using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.CloudLayouter;
using TagCloud.CloudVisualizer;
using TagCloud.Models;
using TagCloud.PointsSequence;
using TagCloud.Utility.Models;

namespace TagCloud.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!IsInputCorrect(args))
                return;

            var pathToWords = args[0];
            var pathToPicture = args[1];

            var tagGroups = new TagGroups();

            if (args.Length > 2)
                tagGroups = ReadTagGroups(args[2]);
            else
            {
                tagGroups.AddSizeGroup("Big", new FrequencyGroup(0.9, 1), new Size(80, 150));
                tagGroups.AddSizeGroup("Average", new FrequencyGroup(0.6, 0.9), new Size(60, 100));
                tagGroups.AddSizeGroup("Small", new FrequencyGroup(0, 0.6), new Size(30, 50));
            }


            var settings = DrawSettings.WordsInRectangles;
            if (args.Length > 3)
                settings = (DrawSettings)(int.Parse(args[3]) % 4);

            var spiral = new Spiral();
            var cloud = new CircularCloudLayouter(spiral);
            var visualizer = new CloudVisualizer.CloudVisualizer
            {
                Settings = settings
            };

            var cloudItems = new TagReader(tagGroups)
                .GetTags(pathToWords)
                .Select(tag => new CloudItem(tag.Word, cloud.PutNextRectangle(tag.Size)))
                .ToArray();

            var picture = visualizer.CreatePictureWithItems(cloudItems);
            picture.Save(Path.Combine(Directory.GetCurrentDirectory(), $"{pathToPicture}.png"));
        }

        private static TagGroups ReadTagGroups(string pathToTagGroups)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathToTagGroups);
            var tagGroups = new TagGroups();

            var groups = new StreamReader(path)
                .ReadToEnd()
                .Split(';')
                .Where(line => !string.IsNullOrEmpty(line));

            foreach (var group in groups)
            {
                var items = group.Split(' ');
                var name = items[0];
                var freq = items[1]
                    .Replace('.',',')
                    .Split('-')
                    .Select(double.Parse)
                    .ToArray();
                var size = items[2]
                    .Split('x')
                    .Select(int.Parse)
                    .ToArray();
                tagGroups.AddSizeGroup(name, new FrequencyGroup(freq[0], freq[1]), new Size(size[0], size[1]));
            }

            return tagGroups;
        }

        private static bool IsInputCorrect(string[] input)
        {
            if (input.Length == 0)
            {
                Console.WriteLine("Input was null. For help write \"help\" in input");
                Console.ReadLine();
                return false;
            }

            if (input[0].ToLower() == "help")
            {
                WriteHelp();
                Console.ReadLine();
                return false;
            }

            return true;
        }

        static void WriteHelp()
        {
            Console.WriteLine("Input should be in format: \n" +
                              "[pathToWords] [pathToPicture] [pathToGroups] [drawSettings = RectanglesWithNumeration] \n" +
                              "Paths starting from exe directory.\n " +
                              "For example:\n" +
                              "Exe in C:.../Test/TagCloud.Utility.exe\n" +
                              "Input: words.txt result tagGroups.txt\n" +
                              "In result:\n" +
                              "Words should be in ../Test/words.txt\n" +
                              "Groups should be in ../Test/tagGroups.txt\n" +
                              "Picture will be saved in ../Test/result.png \n" +
                              "\n" +
                              "Draw Settings:\n" +
                              "OnlyWords == 0\n" +
                              "WordsInRectangles == 1\n" +
                              "OnlyRectangles == 2\n" +
                              "RectanglesWithNumeration == 3 \n" +
                              "\n" +
                              "Tag groups should be in format :\n" +
                              "[Name] [minVal]-[maxVal] [width per letter]x[height];\n" +
                              "For example:\n" +
                              "Big 0.9-1 80x150;Others 0-0.9 50x100;\n" +
                              "In result will be 2 groups: Big and Others;\n" +
                              " \"Big\" group will include words whose number is greater than MaxAppearanceCount * 0.9\n" +
                              " \"Others\" group will include words whose number is less than MaxAppearanceCount * 0.9\n");
        }
    }
}