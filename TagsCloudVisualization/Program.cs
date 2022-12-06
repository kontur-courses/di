using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static readonly string ProjectDirectory 
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public static void Main()
        {
            var serviceCollection = new ServiceCollection();

            var fileGenerator = new FileGenerator();
            var cloudLayouter = new CircularCloudLayouter();
            var prepocessor = new Preprocessor();
            var cloudConfiguration = new CloudConfiguration(
                new Size(1500, 1500),
                Color.FromArgb(255, 0, 34, 43),
                Color.FromArgb(255, 217,92,6),
                new FontFamily("Arial")
            );

            var wordsFilePath = string.Concat(ProjectDirectory, @"\words.txt");
            fileGenerator.Generate(wordsFilePath, 1000);
            
            var center = new Point(750, 750);

            var words = prepocessor.Process(wordsFilePath);
            var tags = TagCloudHelper.CreateTagsFromWords(words);
            var sizes = TagCloudHelper.GenerateRectangleSizes(tags);
            var rectangles = cloudLayouter.GenerateCloud(center, sizes);
            
            var tagsByRectangles = tags.Zip(rectangles, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);

            var bitmap = TagCloudHelper.DrawTagCloud(tagsByRectangles, cloudConfiguration);
            
            bitmap.Save(string.Concat(ProjectDirectory, @"\Images\cloud.png"));
        }
    }
}