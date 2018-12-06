
using System.Collections.Generic;
using Autofac;
using System.Drawing;
using TagsCloudContainer.Util;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Arguments;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "-i", "./input/input.txt", "-o", "./output/o.png", "-c", "red", "--words-to-exclude", "./input/words to exclude.txt" };
            var container = AutofacConfig.ConfigureContainer(args);
            var wordsToExclude = container.ResolveNamed<HashSet<string>>("WordsToExclude");
            container
                .Resolve<WordPreprocessing>()
                .ToLower()
                .Exclude(wordsToExclude)
                .IgnoreInvalidWords();
            var tagCloud = container.Resolve<TagCloud>();            
            var brush = container.Resolve<Brush>();            
            var fontName = container.ResolveNamed<string>("FontName");            
            var outputFilePath = container.Resolve<ArgumentsParser>().OutputPath;            

            var exampleImage = TagCloudRenderer.GenerateImage(tagCloud, fontName, brush);
            exampleImage.Save(outputFilePath);
        }
    }
}
