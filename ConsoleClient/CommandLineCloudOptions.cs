using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CommandLine;
using TagCloudUsageSample;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Printing;

namespace ConsoleClient
{
    public class CommandLineCloudOptions : BaseOptions
    {
        private const int MinimumDegreesValueForVisibleNonIntersection = 5;
        private const int MinimumDensityValueForVisibleNonIntersection = 5;
        
        public string GetFullFilenameByNumber(int number)
            => SavePath.TrimEnd(Path.DirectorySeparatorChar) +
               Path.DirectorySeparatorChar +
               FileName + 
               (CloudCount == 1 ? "" : $"({number})") +
               ".jpg";

        [RangeValidatorAttribute(1, 100, nameof(CloudCount))]
        [Option('c', "count", Default = 1, HelpText = "Set required tag cloud count.")]
        public int CloudCount { get; private set; }
        
        [Option('o', "openFirst", Default = false, HelpText = "Open first created file")]
        public bool OpenFirst { get; set; }
        
        [RangeValidatorAttribute(1, 10000, nameof(RectangleCount))]
        [Option("rectangleCount", Default = 100, HelpText = "Set required rectangles count.")]
        public int RectangleCount { get; private set; }

        [PathValidatorAttribute("unknown directory")]
        [Option('p', "path", Default = "..\\..\\CloudSamples", HelpText = "Set path to save tag clouds.")]
        public string SavePath{ get; private set; }

        [FileValidatorAttribute("invalid file name")]
        [Option('n', "name", Required = true, HelpText = "Set name to save tag clouds.")]
        public string FileName { get; private set; }

        [RangeValidatorAttribute(1, 500, nameof(SizeCoefficient))]
        [Option('s', "size", Default = 100, HelpText = "Set rectangle size coefficient.")]
        public int SizeCoefficient { get; private set; }

        [RangeValidatorAttribute(1, 250, nameof(MinimumRectHeight))]
        [Option('m', "minimumRectHeight", Default = 2, HelpText = "Set minimum rectangle height.")]
        public int MinimumRectHeight{ get; private set; }
        
        
        public void CreateTags(out string firstFileName)
        {
            firstFileName = null;
            
            for (var j = 0; j < CloudCount; j++)
            {
                var fullFileName = GetFullFilenameByNumber(j);
                File.Delete(fullFileName);
                firstFileName ??= fullFileName;
                    
                var rects = GetRectangles(
                    new CircularCloudLayouter(new PointSpiral(Point.Empty, MinimumDensityValueForVisibleNonIntersection, MinimumDegreesValueForVisibleNonIntersection)),
                    RectangleCount,
                    SizeCoefficient,
                    MinimumRectHeight);
                
                new RectanglePrinter(new RectanglesReCalculator())
                    .GetBitmap(new RandomColorScheme(), rects)
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