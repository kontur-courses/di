using System.Drawing;
using System.Reflection;
using TagsCloudVisualization;
using DocumentFormat.OpenXml.Packaging;
using System.Drawing.Imaging;
using WeCantSpell.Hunspell;

namespace TagCloudGenerator
{
    public class TagCloudDrawer
    {
        private TextProcessor textProcessor;
        private WordCounter wordCounter;

        public TagCloudDrawer(WordCounter wordCounter, TextProcessor textProcessor) 
        {
            this.textProcessor = textProcessor;
            this.wordCounter = wordCounter;
        }

        public Bitmap DrawWordsCloud(string filePath, VisualizingSettings visualizingSettings)
        {          
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));
           
            var words = ReadTextFromFile(filePath);        
            var deleteBoringWords = new TextProcessorRemovingBoringWords(textProcessor);
            words = deleteBoringWords.ProcessText(words);

            var wordsWithCount = wordCounter.CountWords(words);
            var orderedWords = wordsWithCount
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);            

            Console.WriteLine("The tag cloud is drawn");

            return Draw(orderedWords, visualizingSettings);
        }

        public void SaveImage(Bitmap bitmap, VisualizingSettings visualizingSettings, string filePath)
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

        private IEnumerable<string> ReadTextFromFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            if (extension == ".docx")
            {
                using (WordprocessingDocument wordDocument = 
                    WordprocessingDocument.Open(filePath, false))
                { 
                    var body = wordDocument.MainDocumentPart.Document.Body;
                    var paragraphs = body.ChildElements;
                        
                    var text = new List<string>(paragraphs.Count);
                    foreach (var paragraph in paragraphs)
                    {
                        if (paragraph.InnerText != "")
                            text.Add(paragraph.InnerText);
                    }

                    return text;
                }
            }
       
            return File.ReadAllLines(filePath);
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