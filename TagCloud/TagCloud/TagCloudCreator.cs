using System;
using System.Collections;
using NUnit.Framework;
using System.Linq;
using TagsCloudVisualization;

namespace TagCloud
{
    
    internal class TagCloudCreator
    {
        private readonly CircularCloudLayouter _layouter;
        private readonly ITextReader _reader;
        private readonly IWordsPreparer _preparer;
        private readonly ITagCloudStatsGenerator _generator;
        private readonly ITagCloudSaver _saver;

        public  TagCloudCreator(CircularCloudLayouter layouter, ITextReader reader, IWordsPreparer preparer, ITagCloudStatsGenerator generator, ITagCloudSaver saver)
        {
            _layouter = layouter;
            _reader = reader;
            _preparer = preparer;
            _generator = generator;
            _saver = saver;
        }

        public TagCloudImage CreateImage()
        {
            var words = _reader.ReadWords();
            words = _preparer.PrepareWords(words);
            var stats = _generator.GenerateStats(words);
            _layouter.PutNextRectangles(stats.Select(w => w.CreateRectangle()));
            return _saver.CreateTagCloudImage(_layouter.Rectangles);
        }
    }
}
