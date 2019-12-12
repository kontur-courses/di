using System.Drawing;
using System.Linq;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.WordsPreprocessing.DocumentParsers;

namespace TagCloud.Interfaces
{
    public class BaseApplication
    {
        public ApplicationSettings AppSettings { get; }
        private readonly CloudVisualizer.CloudVisualizer visualizer;
        public CloudViewConfiguration CloudConfiguration { get; }
        public IDocumentParser[] Parsers { get; }

        public BaseApplication(CloudVisualizer.CloudVisualizer visualizer, CloudViewConfiguration cloudConfiguration,
            IDocumentParser[] parsers, ApplicationSettings settings)
        {
            AppSettings = settings;
            this.visualizer = visualizer;
            CloudConfiguration = cloudConfiguration;
            Parsers = parsers;
        }

        public Image GetImage()
        {
            var format = $".{AppSettings.FilePath.Split('.').Last()}";
            var parser = Parsers.First(p => p.AllowedTypes.Contains(format));
            var words = AppSettings.CurrentTextAnalyzer.GetWords(parser.GetWords(AppSettings), CloudConfiguration.WordsCount);
            return visualizer.GetCloud(words);
        }

        public void Close()
        {
            foreach (var parser in Parsers)
            {
                parser.Close();
            }
        }

        public void SetFontFamily(string fontFamily)
        {
            CloudConfiguration.FontFamily = new FontFamily(fontFamily);
        }
    }
}
