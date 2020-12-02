using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.Layouter.Factory
{
    public class RectanglesLayoutersFactory : IRectanglesLayoutersFactory
    {
        private readonly Dictionary<string, Func<Point, IRectanglesLayouter>> layouters;
        private readonly IWordsConfig wordsConfig;
        private readonly IImageConfig imageConfig;

        public RectanglesLayoutersFactory(IWordsConfig wordsConfig, IImageConfig imageConfig)
        {
            layouters = new Dictionary<string, Func<Point, IRectanglesLayouter>>();
            this.imageConfig = imageConfig;
            this.wordsConfig = wordsConfig;
        }

        public IRectanglesLayouter Create() =>
            layouters[wordsConfig.LayoutName](
                new Point(imageConfig.ImageSize.Width / 2, imageConfig.ImageSize.Height / 2));

        public IEnumerable<string> GetLayouterNames() => layouters.Select(pair => pair.Key);

        public IRectanglesLayoutersFactory Register(string layouterName, Func<Point, IRectanglesLayouter> creationFunc)
        {
            layouters[layouterName] = creationFunc;
            return this;
        }
    }
}
