using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Visualization.WordPainting;

namespace TagCloud.App
{
    public class WordPainterProvider : IWordPainterProvider
    {
        private readonly IEnumerable<IWordPainter> painters;
        private readonly ISettingsProvider settingsProvider;
        private IWordPainter painter;
        private string Name => settingsProvider.GetSettings().WordPainterAlgorithmName;

        public WordPainterProvider(IEnumerable<IWordPainter> painters, ISettingsProvider settingsProvider)
        {
            this.painters = painters;
            this.settingsProvider = settingsProvider;
        }

        public IWordPainter GetWordPainter()
        {
            if (painter != null)
                return painter;
            painter = painters.FirstOrDefault(p => p.Name == Name);
            if (painter == null)
                throw new ArgumentException($"Unknown painter algorithm: {Name}");
            return painter;
        }
    }
}