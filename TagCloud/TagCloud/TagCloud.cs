using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.Drawing;
using TagCloud.Extensions;
using TagCloud.Layout;
using TagCloud.TextProcessing;

namespace TagCloud
{
    public class TagCloud
    {
        private readonly IDrawer _drawer;
        private List<Dictionary<string, int>> _processedTexts = new();
        private readonly TextWriter _statusWriter;
        private readonly ITextProcessor _textProcessor;
        private readonly IWordLayouter _wordLayouter;


        public TagCloud(ITextProcessor textProcessor, IWordLayouter wordLayouter, IDrawer drawer,
            TextWriter statusWriter)
        {
            _textProcessor = textProcessor;
            _wordLayouter = wordLayouter;
            _drawer = drawer;
            _statusWriter = statusWriter;
        }

        public int ProcessText(ITextProcessingOptions options)
        {
            _statusWriter.WriteLine("Начинаю обработку текста");
            _processedTexts = _textProcessor.GetWordsWithFrequency(options).ToList();
            _statusWriter.WriteLine("Обработка завершена\n");
            return 0;
        }

        public int DrawTagClouds(IDrawerOptions options)
        {
            foreach (var text in _processedTexts)
            {
                _statusWriter.WriteLine("Раскладываю текст");
                var layoutedWords = _wordLayouter.Layout(options, text);
                _statusWriter.WriteLine("Рисую bitmap\n");
                _drawer.Draw(options, layoutedWords).SaveDefault();
            }

            _statusWriter.WriteLine("Готово!\n");
            return 0;
        }
    }
}