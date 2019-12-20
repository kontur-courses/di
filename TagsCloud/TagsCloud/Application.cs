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
        private readonly ImageFormat _imageFormat;

        private readonly char[] _delimiters = new char[]
            {',', '.', ' ', ':', ';', '(', ')', '—', '–', '[', ']', '!', '?', '\n'};

        private readonly Dictionary<ImageFormat, string> _imageFormatDenotation =
            new Dictionary<ImageFormat, string>
            {
                {ImageFormat.Jpeg, "jpg"},
                {ImageFormat.Png, "png"},
                {ImageFormat.Bmp, "bmp"},
                {ImageFormat.Gif, "gif"}
            };

        public Application(IWordAnalyzer wordStatisticGetter, ILayouter layouter, IVisualizer visualizer,
            Options options,
            IWriter writer, IWordGetter wordGetter, IWordsProcessor wordsProcessor, ImageFormat imageFormat = null)
        {
            this._wordStatisticGetter = wordStatisticGetter;
            this._layouter = layouter;
            this._visualizer = visualizer;
            this._options = options;
            this._writer = writer;
            this._wordGetter = wordGetter;
            this._wordsProcessor = wordsProcessor;
            this._imageFormat = imageFormat ?? ImageFormat.Jpeg;
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
                var imgName = Path.ChangeExtension(name, _imageFormatDenotation[_imageFormat]);
                _writer.Write($"Your image located at:  {new FileInfo(imgName).FullName}");
                bitmap.Save(imgName, _imageFormat);
            }
        }

        public IEnumerable<Tag> GetTags()
        {
            var rawWords = _wordGetter.GetWords(_delimiters);
            var words = _wordsProcessor.ProcessWords(rawWords);
            var statistics = _wordStatisticGetter.GetWordsStatistics(words);
            return _layouter.GetTags(statistics);
        }
    }
}