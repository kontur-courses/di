using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Visualization;
using TagsCloud.Visualization.Tag;
using TagsCloud.WordPreprocessing;
using TagsCloud.Writer;

namespace TagsCloud
{
    public class Application
    {
        private readonly IWordAnalyzer _wordStatisticGetter;
        private readonly ILayouter _layouter;
        private readonly IVisualizer _visualizer;
        private readonly Options _options;
        private readonly IWriter _writer;
        private readonly IWordGetter _wordGetter;
        private readonly IWordsProcessor _wordsProcessor;

        private readonly char[] _delimiters = new char[]
            {',', '.', ' ', ':', ';', '(', ')', '—', '–', '[', ']', '!', '?', '\n'};

        public Application(IWordAnalyzer wordStatisticGetter, ILayouter layouter, IVisualizer visualizer, Options options,
            IWriter writer, IWordGetter wordGetter, IWordsProcessor wordsProcessor)
        {
            this._wordStatisticGetter = wordStatisticGetter;
            this._layouter = layouter;
            this._visualizer = visualizer;
            this._options = options;
            this._writer = writer;
            this._wordGetter = wordGetter;
            this._wordsProcessor = wordsProcessor;
        }

        public void Run()
        {
            var tags = GetTags();
            CreateImageAndSave(tags);
        }

        private void CreateImageAndSave(IEnumerable<Tag> tags)
        {
            using (var bitmap = _visualizer.GetCloudVisualization(tags.ToList()))
            {
                var name = Path.GetFileName(_options.File);
                var jpgName = Path.ChangeExtension(name, "jpg");
                _writer.Write($"Your image located at:  {new FileInfo(jpgName).FullName}");
                bitmap.Save(jpgName, ImageFormat.Jpeg);
            }
        }

        private IEnumerable<Tag> GetTags()
        {
            var rawWords = _wordGetter.GetWords(_delimiters);
            var words = _wordsProcessor.ProcessWords(rawWords).ToList();
            var statistics = _wordStatisticGetter.GetWordsStatistics(words);
            return _layouter.GetTags(statistics);
        }
    }
}