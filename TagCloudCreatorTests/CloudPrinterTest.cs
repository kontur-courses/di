using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CloudLayouters;
using FluentAssertions;
using NUnit.Framework;
using TagCloudCreator;

namespace TagCloudTests
{
    public class CloudPrinterTest
    {
        [Test]
        public void DrawCloud_TestData_CorrectBitmap()
        {
            var imageSize = new Size(1920, 1080);
            var cloudPrinter = new CloudPrinter(new[] {new TxtFileReader()});
            var bitmap = cloudPrinter.DrawCloud("test.txt", new RectangleLayouter(new Point(imageSize / 2)), imageSize,
                FontFamily.Families.First(x => x.Name == "Arial"), new SingleColorSelector(Color.Black));
            var correctResult = (Bitmap) Image.FromFile("test.png");
            Comparison(bitmap, correctResult).Should().BeTrue();
        }

        private bool Comparison(Bitmap first, Bitmap second)
        {
            if (first.Size != second.Size) return false;
            for (var i = 0; i < first.Width; i++)
            for (var j = 0; j < first.Height; j++)
                if (first.GetPixel(i, j) != second.GetPixel(i, j))
                    return false;
            return true;
        }
    }
}