using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class PngSaver : ISaver
    {
        public void SaveImage(Bitmap bitmap, string fileName)
        {
            CheckValidFileName(fileName);
            var path = Directory.GetCurrentDirectory();

            bitmap.Save($"{path}\\{fileName}.png", ImageFormat.Png);
        }

        private static void CheckValidFileName(string name)
        {
            var invalidSpecialCharacters = "/\\:*?\"<>|".ToCharArray();
            if (name.Any(letter => invalidSpecialCharacters.Contains(letter)))
            {
                throw new ArgumentException("File name contains invalid characters");
            }
        }
    }
}
