using System;
using Autofac;
using TagsCloudVisualization.Commands;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.ImageWriters;
using TagsCloudVisualization.Common.TagCloudPainters;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Processors
{
    public static class CreateCloudProcessor
    {
        public static int Run(CreateCloudCommand options)
        {
            try
            {
                var container = ContainerConfig.ConfigureContainer(options);
                var text = container.Resolve<IFileReader>().ReadFile(options.InputFile);
                var stat = container.Resolve<ITextAnalyzer>().GetWordStatistics(text);
                var tags = container.Resolve<ITagBuilder>().GetTags(stat);
                var bitmap = container.Resolve<ITagCloudPainter>().Paint(tags);
                container.Resolve<IImageWriter>().Save(bitmap, options.OutputFile);

                Console.WriteLine($"Облако тегов сгенерировано и сохранено '{options.OutputFile}'.");
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