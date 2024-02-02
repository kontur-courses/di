using System.Drawing;
using System.Reflection;
using TagsCloudVisualization;
using System.Drawing.Imaging;
using TagCloudGenerator.TextReaders;
using TagCloudGenerator.TextProcessors;

namespace TagCloudGenerator
{
    public class TagCloudDrawer
    {
        private ITextProcessor[] textProcessors;
        private ITextReader[] textReaders;
        private WordCounter wordCounter;

        public TagCloudDrawer(WordCounter wordCounter, IEnumerable<ITextProcessor> textProcessors, IEnumerable<ITextReader> textReaders) 
        {
            this.textProcessors = textProcessors.ToArray();
            this.textReaders = textReaders.ToArray();
            this.wordCounter = wordCounter;
        }

        public Bitmap DrawWordsCloud(string filePath, VisualizingSettings visualizingSettings)
        {          
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            var words = new List<string>();
            var extension = Path.GetExtension(filePath);

            foreach (var textReader in textReaders)
            {                
                if (extension == textReader.GetFileExtension())
                {
                    words = textReader.ReadTextFromFile(filePath).ToList();
                    break;
                }                
            }
            
            foreach (var processor in textProcessors)
                words = processor.ProcessText(words).ToList();
          
            var wordsWithCount = wordCounter.CountWords(words);
            ImageScaler imageScaler = new ImageScaler(wordsWithCount);
            var rectangles = GetRectanglesToDraw(wordsWithCount, visualizingSettings);

            var smallestSizeOfRectangles = imageScaler.GetMinPoints(rectangles);
            var unscaledImageSize = imageScaler.GetImageSizeWithRealSizeRectangles(rectangles, smallestSizeOfRectangles);

            if (imageScaler.NeedScale(visualizingSettings, unscaledImageSize))
            {
                var bitmap = imageScaler.DrawScaleCloud(visualizingSettings, rectangles, unscaledImageSize, smallestSizeOfRectangles);
                Console.WriteLine("The tag cloud is drawn");
                return bitmap;
            }
            
            return Draw(wordsWithCount, visualizingSettings, rectangles);
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
    
        private Bitmap Draw(Dictionary<string, int> tags, VisualizingSettings settings, RectangleF[] rectangles)
        {
            var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using var brush = new SolidBrush(settings.PenColor);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);

            for (var i = 0; i < rectangles.Length; i++)          
                foreach (var tag in tags)
                {
                    var rectangle = rectangles[i];
                    var font = new Font(settings.Font, 24 + (tag.Value * 6));
                    graphics.DrawString(tag.Key, font, brush, rectangle.X, rectangle.Y);
                    i++;
                }

            Console.WriteLine("The tag cloud is drawn");

            return bitmap;
        }

        private RectangleF[] GetRectanglesToDraw(Dictionary<string, int> text, VisualizingSettings settings)
        {
            using var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using var graphics = Graphics.FromImage(bitmap);
            var layouter = new CircularCloudLayouter(settings.PointDistributor);
            var rectangles = new List<RectangleF>();
            foreach (var line in text)
            {
                using var font = new Font(settings.Font, 24 + (line.Value * 6));
                SizeF size = graphics.MeasureString(line.Key, font);
                var rectangle = layouter.PutNextRectangle(size.ToSize());

                rectangles.Add(rectangle);              
            }

            return rectangles.ToArray();
        }
    }
}