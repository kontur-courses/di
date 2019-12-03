﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class ExamplesCircularCloud
    {
        public static void GenerateTagCloud(IEnumerable<Tuple<string, Font>> strings, Size size, string filename)
        {
            var circularCloudDrawing = new CircularCloudDrawing(size);
            foreach (var (str, font) in strings) 
                circularCloudDrawing.DrawString(str, font);
            circularCloudDrawing.SaveImage(filename);
        }

        private const string FONT_FAMILY_NAME = "Arial";
        
        public static void GenerateFirstTagCloud()
        {
            GenerateTagCloud(new List<Tuple<string, Font>>
            {
                new Tuple<string, Font>("Привет", new Font(FONT_FAMILY_NAME, 30)),
                new Tuple<string, Font>("В инете сижу", new Font(FONT_FAMILY_NAME, 14)),
                new Tuple<string, Font>("Чем занимаешься?", new Font(FONT_FAMILY_NAME, 17)),
                new Tuple<string, Font>("Как дела?", new Font(FONT_FAMILY_NAME, 33)),
                new Tuple<string, Font>("Фаня", new Font(FONT_FAMILY_NAME, 42)),
                new Tuple<string, Font>("Я", new Font(FONT_FAMILY_NAME, 20)),
                new Tuple<string, Font>("Круто", new Font(FONT_FAMILY_NAME, 22)),
                new Tuple<string, Font>("Вот так", new Font(FONT_FAMILY_NAME, 19)),
                new Tuple<string, Font>("Играю", new Font(FONT_FAMILY_NAME, 16)),
                new Tuple<string, Font>("Коготь", new Font(FONT_FAMILY_NAME, 33)),
                new Tuple<string, Font>("Шарпей", new Font(FONT_FAMILY_NAME, 42)),
                new Tuple<string, Font>("Трейсер", new Font(FONT_FAMILY_NAME, 31)),
                new Tuple<string, Font>("Доктор", new Font(FONT_FAMILY_NAME, 23)),
                new Tuple<string, Font>("Пшено", new Font(FONT_FAMILY_NAME, 35)),
                new Tuple<string, Font>("Мяч", new Font(FONT_FAMILY_NAME, 32)),
                new Tuple<string, Font>("Ирбит", new Font(FONT_FAMILY_NAME, 16)),
                new Tuple<string, Font>("Екб", new Font(FONT_FAMILY_NAME, 20)),
                new Tuple<string, Font>("Сон", new Font(FONT_FAMILY_NAME, 40))
            }, new Size(600, 600), "1.png");
        }
        
        public static void GenerateSecondTagCloud()
        {
            var enumerable = Enumerable.Range(1, 100)
                .Select(i => new Tuple<string, Font>(i.ToString(), new Font(FONT_FAMILY_NAME, i)));
            GenerateTagCloud(enumerable, new Size(1600, 1600), "2.png");
        }
        
        public static void GenerateThirdTagCloud()
        {
            var enumerable = Enumerable.Repeat(new Tuple<string, Font>((6).ToString(), new Font(FONT_FAMILY_NAME, 15)), 5000);
            GenerateTagCloud(enumerable, new Size(2400, 2400), "3.png");
        }
    }
}