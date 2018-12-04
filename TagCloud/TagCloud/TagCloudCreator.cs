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
        private readonly IImageOptions _options;

        public  TagCloudCreator(CircularCloudLayouter layouter, ITextReader reader, IWordsPreparer preparer, ITagCloudStatsGenerator generator, ITagCloudSaver saver, IImageOptions options)
        {
            _layouter = layouter;
            _reader = reader;
            _preparer = preparer;
            _generator = generator;
            _saver = saver;
            _options = options;
        }

        public TagCloudImage CreateImage()
        {
            var words = _reader.ReadWords();
            words = _preparer.PrepareWords(words);
            var stats = _generator.GenerateStats(words);
            var wordPairs = stats.Select(s => (_layouter.PutNextRectangle(s.CreateRectangle()), s));
            return _saver.CreateTagCloudImage(wordPairs, _options);
        }
    }
}
