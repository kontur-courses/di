using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using CircularCloudLayouter;

namespace WordCloudGenerator
{
    public class Painter
    {
        private readonly ImageFormat imageFormat;
        private readonly CircularLayouter layouter;

        public Painter(ImageFormat imageFormat, CircularLayouter layouter)
        {
            this.imageFormat = imageFormat;
            this.layouter = layouter;
        }

        public void Paint(Dictionary<string, int> words)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(string path)
        {
            throw new NotImplementedException();
        }
    }
}