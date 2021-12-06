using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using TagCloud.Extensions;

namespace TagCloud
{
    public class TagCloud
    {
        private ICloudLayouter _layouter;
        private ITagCloudDrawer _drawer;
        
        private IDrawerSettings _drawerSettings;
        private ITextProcessingSettings _textProcessingSettings;
        private IContainer _container;
        private TextReader _reader;
        private TextProcessor _processor;

        private string _filePath;
        private WordLayouter _wordLayouter;

        public TagCloud()
        {
            _drawerSettings = new DefaultDrawerSettings();
            _textProcessingSettings = new DefaultTextProcessingSettings();

            _layouter = new CircularCloudLayouter(Point.Empty, new ArchimedeanSpiral(new CoordinatesConverter()));
            _drawer = new TagCloudDrawer(_drawerSettings);
            _reader = new TextReader();
            _processor = new TextProcessor(_textProcessingSettings);
            _wordLayouter = new WordLayouter();
        }

        public TagCloud FromFile(string filePath)
        {
            _filePath = filePath;
            return this;
        }

        public void Draw()
        {
            var wordsWithFrequency = _processor.GetInterestingWords(_filePath);
            var layoutedWords = _wordLayouter.Layout(_layouter, wordsWithFrequency);
            _drawer.Draw(layoutedWords).SaveDefault();
        }
    }
}