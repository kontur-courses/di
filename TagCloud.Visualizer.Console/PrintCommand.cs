using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace TagCloud.Visualizer.Console
{
    [Verb("print")]
    public class PrintCommand
    {
        private static readonly string SavePath = Path.Combine(Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            $"{nameof(TagCloud)}.{nameof(Visualizer)}",
            "images");

        public int PrintCloudAndReturnExitCode(ICloudLayouter cloudLayouter, IEnumerable<string> words,
            ImageOptions imageOptions)
        {
            var sizes = SizesCreator.CreateSizesArray(words, imageOptions.FontSize, imageOptions.FontName);
            var rects = cloudLayouter.GetRectangles(sizes);
            var bitmap = BitmapCreator.DrawBitmap(rects, imageOptions);
            bitmap.Save(Path.Combine(SavePath, $"result.{imageOptions.ImageExtension}"));
            return 0;
        }
    }
}