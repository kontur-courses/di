using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization;
using TagsCloudVisualization.Printing;

namespace TagsCloudVisualizationTest
{
    internal class SaveBitmapWhenFailure : Attribute
    {
        public string SavePath { get; private set; }
        private readonly RectanglePrinter rectanglePrinter;
        private readonly IColorScheme colorScheme;

        public SaveBitmapWhenFailure() : this(new RectanglePrinter(new RectanglesReCalculator()), new RandomColorScheme())
        {
            
        }

        public SaveBitmapWhenFailure(RectanglePrinter rectanglePrinter, IColorScheme colorScheme)
        {
            this.rectanglePrinter = rectanglePrinter;
            this.colorScheme = colorScheme;
        }
        
        public bool TrySave(IEnumerable<Rectangle> rectangles, string methodName)
        {
            SavePath = Path.GetFullPath($"..\\..\\TestBitmaps{Path.DirectorySeparatorChar}Failure__{methodName}.jpg");
            
            try
            {
                rectanglePrinter
                    .GetBitmap(colorScheme, rectangles).GetValueOrThrow()  // Throw ArgumentException while creating too big bmp.
                    .Save(SavePath, ImageFormat.Jpeg);
            }
            catch (ArgumentException _)
            {
                return false;
            }

            return true;
        }
    }
}