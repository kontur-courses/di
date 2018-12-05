using System.Collections.Generic;
using TagCloud.Layouters;
using TagCloud.Painters;
using TagCloud.Util;

namespace TagCloud.Visualizers
{
    public abstract class AbstractTagCloudVisualizer
    {
        protected AbstractCloudLayouter layouter;
        protected ImageSettings imageSettings;
        protected IPainter painter;

        protected AbstractTagCloudVisualizer(AbstractCloudLayouter layouter, ImageSettings imageSettings, IPainter painter)
        {
            this.layouter = layouter;
            this.imageSettings = imageSettings;
            this.painter = painter;
        }

        public abstract void Render(List<TagStat> tagStats, string pathForImage);
    }
}