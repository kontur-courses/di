using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public class TagColoringFactory : ITagColoringFactory
    {
        private static readonly Dictionary<string, Func<IEnumerable<Color>, ITagColoring>> factory = new()
        {
            {"alt", colors => new AlternatingTagColoring(colors)},
            {"random", colors => new RandomTagColoring(colors)}
        };

        public ITagColoring Create(string algorithmName, IEnumerable<Color> colors)
        {
            return factory[algorithmName](colors);
        }
    }
}
