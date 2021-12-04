using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.FileReader;
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

        public ConsoleUI(IFileReader fileReader, IPainter painter, IWordWeigher weigher, IImageSaver saver)
        {
            this.fileReader = fileReader;
            this.painter = painter;
            this.weigher = weigher;
            this.saver = saver;
        }

        public void Run(IAppSettings settings)
        {
            var lines = fileReader.GetLines($"{Directory.GetCurrentDirectory()}\\{settings.InputPath}");
            var weightedWords = weigher.GetWeightedWords(lines);
            using var bitmap = painter.CreateImage(weightedWords, settings.ImageWidth, settings.ImageHeight, settings.FontName);
            saver.Save(bitmap, settings.OutputPath, settings.OutputFormat);
        }
    }
}
