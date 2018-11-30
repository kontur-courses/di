using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        private const int CountOfRectangles = 100;
        private const int MaxSizeOfRectangle = 100;
        private const int MinSizeOfRectangle = 10;

        public static void Main()
        {
            var center = new Point(400, 400);
            var spiral = new ArchimedesSpiral(center);

            var directory = Environment.CurrentDirectory;

            var cloud = new CloudLayouter(spiral);

            var visualization = new TagCloudVisualization(cloud);

            var font = new Font("Times New Roman", 40.0f);

            var words = new List<string>
            {
                "Россия",
                "Канада",
                "Китай",
                "США ",
                "Бразилия",
                "Австралия",
                "Индия",
                "Аргентина",
                "Казахстан",
                "Судан",
                "Алжир",
            };

            visualization.SaveCloudLayouter("cloud", directory, font, words);
        }
    }
}
