using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    internal class TagCreator
    {
        private readonly IDrawableProvider drawableProvider;
        private readonly IDrawer drawer;
        private readonly IFrequencyProvider frequencyProvider;
        private readonly ISizableProvider sizableProvider;
        private readonly IWordsProvider wordsProvider;

        public TagCreator(IWordsProvider wordsProvider,
            IFrequencyProvider frequencyProvider,
            ISizableProvider sizableProvider,
            IDrawableProvider drawableProvider,
            IDrawer drawer)
        {
            this.wordsProvider = wordsProvider;
            this.frequencyProvider = frequencyProvider;
            this.sizableProvider = sizableProvider;
            this.drawableProvider = drawableProvider;
            this.drawer = drawer;
        }

        public Result<Bitmap> DrawTag(ReaderSettings readerSettings, DrawerSettings drawerSettings,
            LayouterSettings layouterSettings)
        {
            var words = wordsProvider.GetObjectSource(readerSettings);
            if (!words.IsSuccess)
            {
                return Result.Fail<Bitmap>(words.Error);
            }

            var frequency = frequencyProvider.GetFrequencyDictionary(words.Value);
            var orderedSource = frequency.OrderByDescending(kv => kv.Value)
                .Take(readerSettings.MaxObjectsCount).ToList();
            var sizableSource = sizableProvider.GetSizableSource(orderedSource, drawerSettings);
            var drawableWordSource = drawableProvider.GetDrawableSource(sizableSource, layouterSettings);
            if (!drawableWordSource.IsSuccess)
            {
                return Result.Fail<Bitmap>(drawableWordSource.Error);
            }

            var cloudInfo = new CloudInfo(drawableWordSource.Value);
            var bitmap = drawer.GetBitmap(cloudInfo, drawerSettings);
            return bitmap;
        }
    }
}