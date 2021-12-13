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
            var reader = new TxtFileReader();
            var text = reader.ReadFile(new FileInfo("Example.txt"));
            var cloudMaker = new CircularCloudMaker(Point.Empty);
            var generator = new TokenGenerator(new WordSelector(), new WordCounter(), 
                new TokenOrdererByDescendingWeight());
            var colorer = new RandomTagColor();
            var maker = new TagCloudMaker(cloudMaker, colorer, generator);
            var renderer = new TagCloudVisualiser();
            Console.WriteLine(SystemFonts.MenuFont.Size);
            var tags = maker.CreateTagCloud(text, SystemFonts.MenuFont, 200);
            var image = renderer.Render(tags, new Size(1280, 720));
            image.Save("testimage.png", ImageFormat.Png);
            Process.Start(new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\testimage.png") 
            { UseShellExecute = true });
        }
        
    }
}