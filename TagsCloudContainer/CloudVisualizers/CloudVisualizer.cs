using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public class CloudVisualizer
    {
        private CloudVisualizerSettings settings;
        public CloudVisualizer(CloudVisualizerSettings settings)
        {
            this.settings = settings;
        }

        public Bitmap GetBitmap(IEnumerable<CloudVisualizationWord> words)
        {
            return settings.BitmapMaker.MakeBitmap(words, settings);
        }
    }
}