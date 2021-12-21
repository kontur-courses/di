using System;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudVisualization.Commands;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.ImageWriters;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Common.TagCloudPainters;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Processors
{
    public class CreateCloudProcessor : CommandProcessorBase<CreateCloudCommand>
    {
        protected override void Process(CreateCloudCommand options)
        {
            var container = ContainerConfig.ConfigureContainer();

            var canvasSettings = container.Resolve<ICanvasSettings>();
            canvasSettings.Width = options.Width;
            canvasSettings.Height = options.Height;
            canvasSettings.BackgroundColor = ColorTranslator.FromHtml(options.BackgroundColor.Trim());

            var tagStyleSettings = container.Resolve<ITagStyleSettings>();
            tagStyleSettings.ForegroundColors = options.ForegroundColors
                .Select(color => ColorTranslator.FromHtml(color.Trim()))
                .ToArray();
            tagStyleSettings.FontFamilies = options.Fonts.Select(font => font.Trim()).ToArray();
            tagStyleSettings.Size = options.TagSize;
            tagStyleSettings.SizeScatter = options.TagSizeScatter;
            
            var text = container.Resolve<IFileReader>().ReadFile(options.InputFile);
            var stat = container.Resolve<ITextAnalyzer>().GetWordStatistics(text);
            var tags = container.Resolve<ITagBuilder>().GetTags(stat);
            var bitmap = container.Resolve<ITagCloudPainter>().Paint(tags);
            container.Resolve<IImageWriter>().Save(bitmap, options.OutputFile);

            Console.WriteLine($"Облако тегов сгенерировано и сохранено '{options.OutputFile}'.");
        }
    }
}