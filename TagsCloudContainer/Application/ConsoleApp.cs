using System;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.TextReaders;
using TagsCloudContainer.Visualisators;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Application
{
    public class ConsoleApp : IApp
    {
        private TextReaderGenerator _readerGenerator;
        private Settings _settings;
        private WordHandler _handler;
        private readonly IVisualisator _visualisator;

        public ConsoleApp(TextReaderGenerator readerGenerator, Settings settings,
            WordHandler handler, IVisualisator visualisator)
        {
            _readerGenerator = readerGenerator;
            _settings = settings;
            _handler = handler;
            _visualisator = visualisator;
        }

        public void Run()
        {
            if (_settings.BoringWordsFileName != String.Empty)
                _handler.SetUpBoringWords(GetText(_settings.BoringWordsFileName));

            var words = _handler.ProcessWords(GetText(_settings.FileName));
            var bitmap = _visualisator.Paint(words);

            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            bitmap.Save(projectDirectory + "\\Results", "Rectangles", ImageFormat.Png);
        }

        private string GetText(string fileName)
        {
            var reader = _readerGenerator.GetReader(fileName);
            return reader.GetTextFromFile(fileName);
        }
    }
}