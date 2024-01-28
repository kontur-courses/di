using System.Drawing;
using TagsCloudVisualization;

namespace TagCloudGenerator
{
    public class TagCloudDrawer
    {
        public Bitmap DrawWordsCloud(string filePath, VisualizingSettings visualizingSettings)
        {          
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            var words = ReadTextFromFile(filePath);

            WordCounter wordCounter = new WordCounter();
            var wordsWithCount = wordCounter.CountWords(words);
            var orderedWords = wordsWithCount
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine($"The tag cloud is drawn");

            return Draw(orderedWords, visualizingSettings);
        }

        public void SaveImage(Bitmap bitmap)
        {
            if (bitmap == null) 
                return;

            bitmap.Save("TagCloud.png");
            Console.WriteLine("The image is saved, the path to the image: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}");
        }

        private IEnumerable<string> ReadTextFromFile(string filePath)
        {
            TextProcessor textProcessor = new TextProcessor();

            var text = File.ReadAllLines(filePath);
            return textProcessor.ProcessText(text);
        }

        private Bitmap Draw(Dictionary<string, int> text, VisualizingSettings settings)
        {
            var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var center = new Point(settings.ImageSize.Width/2, settings.ImageSize.Height/2);

            var distributor = settings.PointDistributor;
            var layouter = new CircularCloudLayouter(center, distributor);

            var brush = new SolidBrush(settings.PenColor);
            var graphics = Graphics.FromImage(bitmap);

            foreach(var line in text)
            {
                var font = new Font(settings.Font, 24 + (line.Value * 6));
                SizeF size = graphics.MeasureString(line.Key, font);
                var rect = layouter.PutNextRectangle(size.ToSize());          

                graphics.DrawString(line.Key, font, brush, rect.X, rect.Y);
            }

           return bitmap;
        }  
    }
}