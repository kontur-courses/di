
using System.Collections.Generic;
using Autofac;
using System.Drawing;
using TagsCloudContainer.Util;
using TagsCloudContainer.Cloud;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var exampleImage = TagCloudRenderer.GenerateImage(tagCloud, fontName, brush);
            exampleImage.Save($"./output/example_{"4"}.png");
        }
    }
}
