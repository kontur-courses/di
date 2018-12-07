using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class CreateLayout
    {
        private readonly ICloudLayouter layouter;

        public CreateLayout(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public Dictionary<string, Rectangle> GetLayout(Dictionary<string, double> words)
        {
            var wordsPosition = new Dictionary<string, Rectangle>();
            foreach (var keyValuePair in words)
            {
                var width = (int) Math.Round(keyValuePair.Key.Length * keyValuePair.Value);
                var height = (int) Math.Round(keyValuePair.Value);
                var size = new Size(width, height);
                wordsPosition.Add(keyValuePair.Key, layouter.PutNextRectangle(size));
            }

            return wordsPosition;
        }
    }
}