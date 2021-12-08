using System.Linq;
using TagsCloud.Visualization;
using TagsCloud.Visualization.Drawer;
using TagsCloud.Visualization.ImagesSavior;
using TagsCloud.Visualization.LayoutContainer.ContainerBuilder;

namespace TagsCloud.Words
{
    public class LayouterCore
    {
        private readonly IDrawer drawer;
        private readonly IImageSavior imageSavior;
        private readonly AbstractWordsContainerBuilder wordsContainerBuilder;
        private readonly IWordsService wordsService;

        public LayouterCore(
            IWordsService wordsService,
            AbstractWordsContainerBuilder wordsContainerBuilder,
            IDrawer drawer,
            IImageSavior imageSavior)
        {
            this.wordsService = wordsService;
            this.wordsContainerBuilder = wordsContainerBuilder;
            this.drawer = drawer;
            this.imageSavior = imageSavior;
        }

        public void Run()
        {
            var parsedWords = wordsService.GetWords();

            var maxCount = parsedWords.Max(x => x.Count);
            var minCount = parsedWords.Min(x => x.Count);

            var wordsContainer = wordsContainerBuilder
                .AddWords(parsedWords, minCount, maxCount)
                .Build();

            using var image = drawer.Draw(wordsContainer);
            imageSavior.Save(image);
        }
    }
}