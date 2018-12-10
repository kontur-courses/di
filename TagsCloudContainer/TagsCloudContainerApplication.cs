using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloudContainerApplication
    {
        private IWordsReader reader;
        private TagsCloudGenerator generator;
        private ITagsCloudRenderer renderer;
        private IUI ui;

        public TagsCloudContainerApplication
            (IUI ui, IWordsReader reader, TagsCloudGenerator generator, ITagsCloudRenderer renderer)
        {
            this.reader = reader;
            this.generator = generator;
            this.renderer = renderer; 
            this.ui = ui;
        }

        public void Run()
        {
            var words = reader.ReadWords(ui.InputPath);
            var cloud = generator.CreateCloud(words);
            renderer.RenderIntoFile(ui.OutputPath, cloud);
        }
    }
}