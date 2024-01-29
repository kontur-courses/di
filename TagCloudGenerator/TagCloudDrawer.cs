using System.Drawing;
using System.Reflection;
using TagsCloudVisualization;
using System.Drawing.Imaging;

namespace TagCloudGenerator
{
    public class TagCloudDrawer
    {
        private TextProcessor textProcessor;
        private WordCounter wordCounter;
        private TextReader textReader;

        public TagCloudDrawer(WordCounter wordCounter, TextProcessor textProcessor, TextReader textReader) 
        {
            this.textProcessor = textProcessor;
            this.wordCounter = wordCounter;
            this.textReader = textReader;
        }

        public Bitmap DrawWordsCloud(string filePath, VisualizingSettings visualizingSettings)
        {          
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));
           
            var words = textReader.ReadTextFromFile(filePath);        
            var deleteBoringWords = new BoringWordsTextProcessor(textProcessor);
            words = deleteBoringWords.ProcessText(words);
            var wordsWithCount = wordCounter.CountWords(words);

            Console.WriteLine("The tag cloud is drawn");

            return Draw(wordsWithCount, visualizingSettings);
        }

        public void SaveImage(Bitmap bitmap, VisualizingSettings visualizingSettings)
        {
            if (bitmap == null) 
                return;

            var extension = Path.GetExtension(visualizingSettings.ImageName);
            var format = GetImageFormat(extension);

            bitmap.Save(visualizingSettings.ImageName, format);
            
            Console.WriteLine($"The image is saved, the path to the image: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}");
        }

        private ImageFormat GetImageFormat(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(
                    string.Format("Unable to determine file extension for fileName: {0}", fileName));

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    throw new NotImplementedException();
            }
        }

        private Bitmap Draw(Dictionary<string, int> text, VisualizingSettings settings)
        {
            var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var center = new Point(settings.ImageSize.Width/2, settings.ImageSize.Height/2);
            var distributor = settings.PointDistributor;
            var layouter = new CircularCloudLayouter(center, distributor);
            var brush = new SolidBrush(settings.PenColor);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);

            foreach (var line in text)
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