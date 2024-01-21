using System.Drawing;

namespace TagsCloudContainer.Utility
{
    public static class Visualizer
    {
        public static Bitmap VisualizeRectangles(List<Rectangle> rectangles, HashSet<string> uniqueWords, int bitmapWidth, int bitmapHeight)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            using var graphics = Graphics.FromImage(bitmap);

            for (var i = 0; i < rectangles.Count; i++)
            {
                var rect = rectangles[i];
                var word = uniqueWords.ElementAt(i);

                var fontSize = rect.Width / word.Length;
                var font = new Font("Arial", fontSize, FontStyle.Regular);

                var brushColor = word == GetMostPopularWord(uniqueWords) ? Color.Green : Color.Black;
                var brush = new SolidBrush(brushColor);

                graphics.DrawString(word, font, brush, rect);
            }

            return bitmap;
        }

        private static string GetMostPopularWord(HashSet<string> uniqueWords)
        {
            return uniqueWords.FirstOrDefault();
        }

        //private static void DrawRectangles(IEnumerable<Rectangle> rectangles, Graphics graphics)
        //{
        //    var pen = new Pen(Color.Green);
        //    foreach (var rect in rectangles)
        //    {
        //        graphics.DrawRectangle(pen, rect);
        //    }
        //}

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
