using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;

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

        public Bitmap DrawTag(ReaderSettings readerSettings, DrawerSettings drawerSettings,
            LayouterSettings layouterSettings)
        {
            var words = wordsProvider.GetObjectSource(readerSettings);
            var frequency = frequencyProvider.GetFrequencyDictionary(words);
            var orderedSource = frequency.OrderByDescending(kv => kv.Value).Take(readerSettings.MaxObjectsCount);
            var sizableSource = sizableProvider.GetSizableSource(orderedSource, drawerSettings);
            var drawableWordSource = drawableProvider.GetDrawableSource(sizableSource, layouterSettings);
            var cloudInfo = new CloudInfo(drawableWordSource);
            var bitmap = drawer.GetBitmap(cloudInfo, drawerSettings);
            return bitmap;
        }
    }
}