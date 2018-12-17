using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.TagsClouds;
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
        private readonly ITagsCloudRenderer renderer;
        private readonly IColorManager colorManager;
        private readonly IBoringWordsRepository boringWordsRepository;
        private readonly IWordsSizer wordsSizer;
        private readonly ITagsCloudLayouterFactory layouterFactory;
        private readonly ITagsCloudFactory tagsCloudFactory;


        public TagsCloudContainerApplication
        (IWordsReader reader, FormattingComponent formattingComponent,
            FilteringComponent filteringComponent, IWordsSizer wordsSizer, ITagsCloudRenderer renderer,
            IColorManager colorManager, IBoringWordsRepository boringWordsRepository,
            ITagsCloudFactory tagsCloudFactory, ITagsCloudLayouterFactory layouterFactory)
        {
            this.reader = reader;
            this.formattingComponent = formattingComponent;
            this.filteringComponent = filteringComponent;
            this.renderer = renderer;
            this.colorManager = colorManager;
            this.boringWordsRepository = boringWordsRepository;
            this.wordsSizer = wordsSizer;
            this.tagsCloudFactory = tagsCloudFactory;
            this.layouterFactory = layouterFactory;
        }


        public void Run(string[] args)
        {
            var ui = new CLI(args);
            var appSettings = ui.ApplicationSettings;
            var words = reader.ReadWords(appSettings.InputPath);
            words = formattingComponent.FormatWords(words);
            var generator = new TagsCloudGenerator
                (wordsSizer, layouterFactory.CreateTagsCloudLayouter(appSettings.TagsCloudCenter, tagsCloudFactory));
            boringWordsRepository.LoadWords(appSettings.BlackListPath);
            words = filteringComponent.FilterWords(words);
            var cloud = generator.CreateCloud(words, appSettings.ImageSettings.LetterSize);
            renderer.RenderIntoFile(appSettings.ImageSettings, colorManager, cloud);
        }
    }
}