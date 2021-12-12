using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization.Default;

namespace TagsCloudVisualization
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var text = "Tag cloud cloud cloud cloud penis vulva suka 4len";
            var cloudMaker = new CircularCloudMaker(Point.Empty);
            var generator = new TokenGenerator(new WordSelector(), new WordCounter(), 
                new TokenOrdererByDescendingWeight());
            var colorer = new TagSingleColor(Color.Crimson);
            var maker = new TagCloudMaker(cloudMaker, colorer, generator);
            var renderer = new TagCloudVisualiser();
            var tags = maker.CreateTagCloud(text, new Font("Comic Sans MS", 1));
            var image = renderer.Render(tags, new Size(1280, 720));
            image.Save("testimage.png", ImageFormat.Png);
            Process.Start(new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\testimage.png") 
            { UseShellExecute = true });
        }
    }
}