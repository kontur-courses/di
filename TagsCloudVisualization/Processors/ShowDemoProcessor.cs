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
    public class ShowDemoProcessor : CommandProcessorBase<ShowDemoCommand>
    {
        private static readonly string[] TestFiles =
        {
            @"\demo\Test_Облако.txt",
            @"\demo\Test_Литературный_текст.txt",
            @"\demo\Text_Большой_текст.txt"
        };
        
        protected override void Process(ShowDemoCommand options)
        {
            var container = ContainerConfig.ConfigureContainer();
            var executingPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            foreach (var testFile in TestFiles)
            {
                var text = container.Resolve<IFileReader>().ReadFile(executingPath + testFile);
                var stat = container.Resolve<ITextAnalyzer>().GetWordStatistics(text);
                var tags = container.Resolve<ITagBuilder>().GetTags(stat);
                var bitmap = container.Resolve<ITagCloudPainter>().Paint(tags);

                var saveFilePath = options.OutputPath + Path.GetFileNameWithoutExtension(testFile) + ".png";
                container.Resolve<IImageWriter>()
                    .Save(bitmap, saveFilePath);

                Console.WriteLine(
                    $"Облако тегов сгенерировано и сохранено '{saveFilePath}'.");
            }
        }
    }
}