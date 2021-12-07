using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using CommandLine;

namespace TagCloudUsageSample
{
    internal static class Program
    {
        private const int MinimumDegreesValueForVisibleNonIntersection = 5;
        private const int MinimumDensityValueForVisibleNonIntersection = 5;
        
        internal static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<CommandLineCloudOptions>(args)
                .WithParsed(options =>
                {
                    if (!options.Validate(out var message))
                    {
                        Console.WriteLine(message);
                        return;
                    }

                    CreateTags(options, out var firstFileName);
                    if (firstFileName is not null && options.OpenFirst)
                        System.Diagnostics.Process.Start(firstFileName);
                });
        }

        private static void CreateTags(CommandLineCloudOptions options, out string firstFileName)
        {
            firstFileName = null;
            
            for (var j = 0; j < options.CloudCount; j++)
            {
                var fullFileName = options.GetFullFilenameByNumber(j);
                File.Delete(fullFileName);
                firstFileName ??= fullFileName;
                    
                var rects = GetRectangles(
                    new CircularCloudLayouter(new PointSpiral(Point.Empty, MinimumDensityValueForVisibleNonIntersection, MinimumDegreesValueForVisibleNonIntersection)),
                    options.RectangleCount,
                    options.SizeCoefficient,
                    options.MinimumRectHeight);
                        
                RectanglePainter.GetBitmapWithRectangles(rects)
                    .Save(fullFileName, ImageFormat.Jpeg);
            }
        }
    
        private static IEnumerable<Rectangle> GetRectangles(
            CircularCloudLayouter layouter, int count, 
            int sizeCoefficient, int minimumRectHeight)
        {
            var rnd = new Random();
            
            for (var i = 0; i < count; i++)
            {
                var h = rnd.Next(
                    minimumRectHeight,
                    sizeCoefficient - i < minimumRectHeight ? minimumRectHeight : sizeCoefficient - i);
                
                var w = rnd.Next(
                    h,
                    sizeCoefficient - i < h ? h : sizeCoefficient - i);
                
                yield return layouter.PutNextRectangle(new Size(w, h));
            }
        }
    }
}