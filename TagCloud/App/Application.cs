using System.Linq;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.Visualization;
using TagCloud.WordsProcessing;

namespace TagCloud.App
{
    public class Application
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly ITextReader textReader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordSizeSetter wordSizeSetter;
        private readonly ITagCloudGenerator tagCloudGenerator;
        private readonly IImageFormat imageFormat;

        public Application(
            ISettingsProvider settingsProvider, 
            ITextReader textReader,
            IWordProcessor wordProcessor, 
            IWordSizeSetter wordSizeSetter,
            ITagCloudGenerator tagCloudGenerator,
            IImageFormat imageFormat)
        {
            this.settingsProvider = settingsProvider;
            this.textReader = textReader;
            this.wordProcessor = wordProcessor;
            this.wordSizeSetter = wordSizeSetter;
            this.tagCloudGenerator = tagCloudGenerator;
            this.imageFormat = imageFormat;
        }

        public void Run()
        {
            var settings = settingsProvider.GetSettings();
            var rawWords = textReader.ReadWords(settings.InputFilePath).ToList();
            var preparedWords = wordProcessor.PrepareWords(rawWords).ToList();
            var sizedWords = wordSizeSetter.GetSizedWords(preparedWords, settings.PictureConfig).ToList();
            var bitmap = tagCloudGenerator.GetTagCloudBitmap(sizedWords);
            imageFormat.SaveImage(bitmap, settings.OutputFilePath);
        }


    }
}
