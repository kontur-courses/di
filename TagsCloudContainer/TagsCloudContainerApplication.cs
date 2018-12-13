using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation;

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


        public TagsCloudContainerApplication
        (IUI ui, IWordsReader reader, FormattingComponent formattingComponent,
            FilteringComponent filteringComponent, TagsCloudGenerator generator, ITagsCloudRenderer renderer)
        {
            this.reader = reader;
            this.formattingComponent = formattingComponent;
            this.filteringComponent = filteringComponent;
            this.generator = generator;
            this.renderer = renderer;
            this.ui = ui;
        }


        public void Run()
        {
            var words = reader.ReadWords(ui.InputPath);
            words = formattingComponent.FormatWords(words);
            words = filteringComponent.FilterWords(words);

            var cloud = generator.CreateCloud(words);
            renderer.RenderIntoFile(ui.OutputPath, cloud, true);
        }
    }
}