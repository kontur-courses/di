using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using TagsCloudVisualization.Default;

namespace TagsCloudVisualization
{
    internal static class Program
    {
         private static void Main(string[] args)
         { 
             var tagCloud = new TagCloudFactory().CreateInstance(false, "mixed");
            var font = new Font("Comic Sans MS", 9);
            var resolution = new Size(1280, 720);
            tagCloud.CreateTagCloudFromFile("Example.txt", "testimage.png", font,
                                                    Color.Transparent, 100, resolution);
            Process.Start(new ProcessStartInfo("testimage.png")
            { UseShellExecute = true });
        }
        
    }
}