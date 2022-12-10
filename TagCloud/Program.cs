using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.FileReader;
using TagCloud.WordConverter;
using TagCloud.WordFilter;
using TagCloud.FrequencyAnalyzer;
using TagCloud.WordColoring;
using TagCloud.ImageProcessing;
using TagCloud.CloudLayouter;
using TagCloud.TextParsing;

namespace TagCloud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var text = new TxtFileReader().ReadAllText("TestText.txt");

            var words = new TextParser().GetWords(text);

            var converterExecutor = new ConvertersExecutor();
            converterExecutor.RegisterConverter(new ToLowerConverter());

            var filtersExecutor = new FiltersExecutor();
            filtersExecutor.RegisterFilter(new BoringWordFilter());

            var convertedWords = converterExecutor.Convert(words);

            var filteredWords = filtersExecutor.Filter(convertedWords);

            var frequencies = new WordsFrequencyAnalyzer().GetFrequencies(filteredWords);

            var gradientColoring = new GradientColoring()
            {
                MinValue = frequencies.Min(pair => pair.Value),
                MaxValue = frequencies.Max(pair => pair.Value)
            };

            var imageSettings = new ImageSettings()
            {
                WordColoring = gradientColoring
            };

            var layouter = new CircularCloudLayouter(new Point(0, 0));

            var imageGenerator = new CloudImageGenerator(layouter, imageSettings);

            var bitmap = imageGenerator.GenerateBitmap(frequencies);

            ImageSaver.SaveBitmapInSolutionSubDirectory(bitmap, "TagCloudImages", "GradientWordCloud.png");
        }

        private static void CreateTestText()
        {
            var lines = File.ReadAllLines("TestWords.txt");

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                var chars = tokens[0].ToCharArray();

                chars[0] = char.ToUpper(chars[0]);

                var word = new string(chars);

                var frequency = Convert.ToInt32(Math.Round(double.Parse(tokens[1]) / 100));

                dict.Add(word, frequency);
            }

            var sb = new StringBuilder();

            foreach (var pair in dict)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    sb.AppendLine(pair.Key);
                }
            }

            File.WriteAllText("TestText.txt", sb.ToString());
        }
    }
}

