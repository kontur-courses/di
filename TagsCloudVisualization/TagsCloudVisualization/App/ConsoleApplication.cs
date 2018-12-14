using System.Drawing;
using Fclp;

namespace TagsCloudVisualization
{
    public class ConsoleApplication : IApplication
    {
        private IFileReader fileReader;
        private IVisualizer visualizer;
        private IWordPalette wordPalette;
        private ISizeDefiner sizeDefiner;
        private ICloudLayouter cloudLayouter;
        private WordCounter counter = new WordCounter();
        private IImageSettings imageSettings;
        private string path;

        public ConsoleApplication(IFileReader fileReader, IVisualizer visualizer, 
            IWordPalette wordPalette, ISizeDefiner sizeDefiner, 
            IImageSettings imageSettings, ICloudLayouter cloudLayouter)
        {
            this.fileReader = fileReader;
            this.visualizer = visualizer;
            this.wordPalette = wordPalette;
            this.sizeDefiner = sizeDefiner;
            this.cloudLayouter = cloudLayouter;
            this.imageSettings = imageSettings;
        }

        private void ParseArguments(string[] args)
        {
            var parser = new FluentCommandLineParser<CloundArguments>();
            parser.Setup(arg => arg.FileName).As('p', "path").Required();
            parser.Setup(arg => arg.Width).As('w', "width").SetDefault(800);
            parser.Setup(arg => arg.Height).As('h', "height").SetDefault(800);
            parser.Setup(arg => arg.Font).As('f', "font").SetDefault("Arial");

            parser.Parse(args);

            var settings = parser.Object;
            imageSettings.Size = new Size(settings.Width, settings.Height);
            imageSettings.Center = new Point(settings.Width / 2, settings.Height / 2);
            counter.Font = new Font(settings.Font, 14);
            path = settings.FileName;

        }

        public void GenerateImage(string[] args)
        {
            ParseArguments(args);

            var file = fileReader.Read(path);
            var words = counter.Count(true, file);
            cloudLayouter.Process(words, sizeDefiner, imageSettings.Center);
            var image = visualizer.Render(words, imageSettings.Size.Width, imageSettings.Size.Height, wordPalette);
            ImageSaver.WriteToFile("output.png", image);

        }
    }

    class CloundArguments
    {
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Font { get; set; }

    }
}
