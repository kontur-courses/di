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
    internal class TagCreator<T>
    {
        private readonly IDrawableProvider<T> drawableProvider;
        private readonly IDrawer<T> drawer;
        private readonly IFrequencyProvider<T> frequencyProvider;
        private readonly IObjectProvider<T> objectProvider;
        private readonly ISizableProvider<T> sizableProvider;

        public TagCreator(IObjectProvider<T> objectProvider,
            IFrequencyProvider<T> frequencyProvider,
            ISizableProvider<T> sizableProvider,
            IDrawableProvider<T> drawableProvider,
            IDrawer<T> drawer)
        {
            this.objectProvider = objectProvider;
            this.frequencyProvider = frequencyProvider;
            this.sizableProvider = sizableProvider;
            this.drawableProvider = drawableProvider;
            this.drawer = drawer;
        }

        public Bitmap DrawTag(ReaderSettings readerSettings, DrawerSettings drawerSettings,
            LayouterSettings layouterSettings)
        {
            var words = objectProvider.GetObjectSource(readerSettings);
            var frequency = frequencyProvider.GetFrequencyDictionary(words);
            var orderedSource = frequency.OrderByDescending(kv => kv.Value).Take(readerSettings.MaxObjectsCount);
            var sizableSource = sizableProvider.GetSizableObjects(orderedSource, drawerSettings);
            var drawableSource = drawableProvider.GetDrawableObjects(sizableSource, layouterSettings);
            var cloudInfo = new CloudInfo<T>(drawableSource);
            var bitmap = drawer.GetBitmap(cloudInfo, drawerSettings);
            return bitmap;
        }
    }
}