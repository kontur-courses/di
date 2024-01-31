using TagsCloudContainer.Infrastucture;
using TagsCloudContainer.Infrastucture.Settings;

namespace TagsCloudContainer.Algorithm
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ImageSettings imageSettings;
        private readonly IRectanglePlacer rectanglePlacer;


        public CircularCloudLayouter(ImageSettings imageSettings, IRectanglePlacer rectanglePlacer)
        {
            this.imageSettings = imageSettings;
            this.rectanglePlacer = rectanglePlacer;
        }

        public List<TextRectangle> GetRectangles(Dictionary<string, int> wordFrequencies)
        {
            var rectangles = new List<TextRectangle>();
            var bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in wordFrequencies.Keys)
            {
                var fontSize = CalculateFontSize(wordFrequencies, word, imageSettings.Font.Size);
                var font = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style, imageSettings.Font.Unit);
                var textSize = graphics.MeasureString(word, font);
                var rectangle = rectanglePlacer.GetPossibleNextRectangle(rectangles, textSize);
                rectangles.Add(new TextRectangle(rectangle, word, font));
            }

            return rectangles;
        }

        private float CalculateFontSize(Dictionary<string, int> wordFrequencies, string word, float fontSize)
        {
            return fontSize + (wordFrequencies[word] - wordFrequencies.Values.Min()) * 20 / (wordFrequencies.Values.Max());
        }
    }
}
