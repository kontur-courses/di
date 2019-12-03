using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudVisualizers
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private Func<CloudVisualizerSettings> settingsFactory;
        public CloudVisualizer(Func<CloudVisualizerSettings> settingsFactory)
        {
            this.settingsFactory = settingsFactory;
        }

        public Bitmap GetBitmap(IEnumerable<CloudVisualizationWord> words)
        {
            var settings = settingsFactory();
            return settings.BitmapMaker.MakeBitmap(words, settings);
        }
    }
}