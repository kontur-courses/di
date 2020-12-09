using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizer
{
    public static class BitmapCreator
    {
        internal static Bitmap DrawBitmap(List<RectangleWithWord> rectanglesWithWords, ImageOptions opts)
        {
            var bitmap = new Bitmap(opts.ImageWidth, opts.ImageHeight);
            var graph = Graphics.FromImage(bitmap);
            graph.Clear(Color.White);
            var brush = (Brush) typeof(Brushes).GetProperty($"{opts.ColorName}")?.GetValue(null);
            foreach (var rectangleWithWord in rectanglesWithWords)
            {
                graph.DrawString(rectangleWithWord.Word.Value,
                    new Font(opts.FontName, opts.FontSize * (float) rectangleWithWord.Word.Weight),
                    brush!,
                    rectangleWithWord.Rectangle);
            }

            return bitmap;
        }
    }
}