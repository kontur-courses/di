using TagsCloudApp.WordsLoading;
using TagsCloudContainer;

namespace TagsCloudApp.RenderCommand
{
    public class RenderCommand : IRenderCommand
    {
        private readonly ITagsCloudDirector tagsCloudDirector;
        private readonly IBitmapSaver bitmapSaver;
        private readonly IWordsProvider wordsProvider;

        public RenderCommand(
            ITagsCloudDirector tagsCloudDirector,
            IBitmapSaver bitmapSaver,
            IWordsProvider wordsProvider)
        {
            this.tagsCloudDirector = tagsCloudDirector;
            this.bitmapSaver = bitmapSaver;
            this.wordsProvider = wordsProvider;
        }

        public void Render()
        {
            var words = wordsProvider.GetWords();
            using var bitmap = tagsCloudDirector.RenderWords(words);
            bitmapSaver.Save(bitmap);
        }
    }
}