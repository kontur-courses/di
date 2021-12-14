using System;
using System.IO;
using Autofac;
using TagsCloudVisualization.Commands;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.ImageWriters;
using TagsCloudVisualization.Common.TagCloudPainters;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Processors
{
    public static class ShowDemoProcessor
    {
        private static readonly string[] TestFiles =
        {
            @"TestData\txt\Test_Облако.txt",
            @"TestData\txt\Test_Литературный_текст.txt",
            @"TestData\txt\Text_Большой_текст.txt"
        };

        public static int Run(ShowDemoCommand options)
        {
            try
            {
                var container = ContainerConfig.ConfigureContainer(new CommandLineOptions());
                var workDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var testFile in TestFiles)
                {
                    var text = container.Resolve<IFileReader>().ReadFile(workDir + testFile);
                    var stat = container.Resolve<ITextAnalyzer>().GetWordStatistics(text);
                    var tags = container.Resolve<ITagBuilder>().GetTags(stat);
                    var bitmap = container.Resolve<ITagCloudPainter>().Paint(tags);
                    container.Resolve<IImageWriter>()
                        .Save(bitmap, options.OutputPath + Path.GetFileName(testFile) + ".png");

                    Console.WriteLine(
                        $"Облако тегов сгенерировано и сохранено '{workDir + Path.GetFileName(testFile) + ".png"}'.");
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }
    }
}