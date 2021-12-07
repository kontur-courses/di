using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.FileReader;
using TagCloudContainer.Infrastructure.Filter;
using TagCloudContainer.Infrastructure.Lemmatizer;
using TagCloudContainer.Infrastructure.Painter;
using TagCloudContainer.Infrastructure.Saver;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainer.App.UI
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IFileReader fileReader;
        private readonly IPainter painter;
        private readonly IWordWeigher weigher;
        private readonly IImageSaver saver;
        private readonly ILemmatizer lemmatizer;
        private readonly IFilter filter;

        public ConsoleUI(IFileReader fileReader, IPainter painter, IWordWeigher weigher, IImageSaver saver, ILemmatizer lemmatizer, IFilter filter)
        {
            this.fileReader = fileReader;
            this.painter = painter;
            this.weigher = weigher;
            this.saver = saver;
            this.lemmatizer = lemmatizer;
            this.filter = filter;
        }

        public void Run(IAppSettings settings)
        {
            var lines = fileReader.GetLines(settings.InputPath);
            var lemmas = lemmatizer.GetLemmas(lines);
            var filtered = filter.FilterWords(lemmas);
            var weightedWords = weigher.GetWeightedWords(filtered);
            using var bitmap = painter.CreateImage(weightedWords);
            saver.Save(bitmap, settings.OutputPath, settings.OutputFormat);
        }
    }
}
