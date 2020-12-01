using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.Layouter.Factory
{
    public class LayouterFactory : ILayouterFactory
    {
        private readonly Dictionary<string, Func<Point, ILayouter>> layouters;
        private readonly IWordsConfig wordsConfig;

        public LayouterFactory(IWordsConfig wordsConfig)
        {
            layouters = new Dictionary<string, Func<Point, ILayouter>>();
            this.wordsConfig = wordsConfig;
        }

        public ILayouter Create(Point center) => layouters[wordsConfig.LayoutName](center);

        public IEnumerable<string> GetLayouterNames() => layouters.Select(pair => pair.Key);

        public void Register(string layouterName, Func<Point, ILayouter> creationFunc)
        {
            layouters[layouterName] = creationFunc;
        }
    }
}
