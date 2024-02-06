using System.Drawing;

namespace TagsCloudContainer.Utility
{
    public static class Visualizer
    {
        public static Bitmap VisualizeRectangles(List<Rectangle> rectangles, HashSet<string> uniqueWords,
    int bitmapWidth, int bitmapHeight, List<int> fontSizes, string fontName, Color fontColor, Color highlightColor, double percentageToHighlight,
    Dictionary<string, int> wordFrequencies)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            using var graphics = Graphics.FromImage(bitmap);
            var mostPopularWords = GetMostPopularWords(uniqueWords, percentageToHighlight, wordFrequencies);

            for (var i = 0; i < rectangles.Count; i++)
            {
                var rect = rectangles[i];
                var word = uniqueWords.ElementAt(i).ToLower(); 

                var fontSize = fontSizes[i];
                var font = new Font(fontName, fontSize, FontStyle.Regular);

                var brushColor = mostPopularWords.Contains(word) ? highlightColor : fontColor;
                var brush = new SolidBrush(brushColor);

                graphics.DrawString(word, font, brush, rect);
            }

            return bitmap;
        }

        private static List<string> GetMostPopularWords(HashSet<string> uniqueWords, double percentage, Dictionary<string, int> wordFrequencies)
        {
            int countToHighlight = (int)Math.Ceiling(uniqueWords.Count * percentage);
            return uniqueWords.OrderByDescending(word => wordFrequencies[word]).Take(countToHighlight).ToList();
        }

        public static void SaveBitmap(Bitmap bitmap, string fileName, string pathToDirectory)
        {
            EnsureDirectoryExists(pathToDirectory);

            var safeFileName = GetSafeFileName(fileName);
            bitmap.Save(Path.Combine(pathToDirectory, safeFileName), System.Drawing.Imaging.ImageFormat.Png);
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static string GetSafeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Concat(fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
