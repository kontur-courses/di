using System.Drawing;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer
{
    public class TagsCloudContainerApplication
    {
        private readonly IWordsReader reader;
        private readonly FormattingComponent formattingComponent;
        private readonly FilteringComponent filteringComponent;
        private readonly TagsCloudGenerator generator;
        private readonly ITagsCloudRenderer renderer;
        private readonly IUI ui;
        private readonly IColorManager colorManager;


        public TagsCloudContainerApplication
        (IUI ui, IWordsReader reader, FormattingComponent formattingComponent,
            FilteringComponent filteringComponent, TagsCloudGenerator generator, ITagsCloudRenderer renderer,
            IColorManager colorManager)
        {
            this.reader = reader;
            this.formattingComponent = formattingComponent;
            this.filteringComponent = filteringComponent;
            this.generator = generator;
            this.renderer = renderer;
            this.ui = ui;
            this.colorManager = colorManager;
        }


        public void Run()
        {
            var words = reader.ReadWords(ui.InputPath);
            words = formattingComponent.FormatWords(words);
            words = filteringComponent.FilterWords(words);
            var cloud = generator.CreateCloud(words);
            renderer.RenderIntoFile(ui.OutputPath, cloud, colorManager, true);
        }
    }
}