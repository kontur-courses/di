using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TagsCloudLayout.CloudLayouters;
using TextConfiguration.TextReaders;
using TextConfiguration;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizator
    {
        private readonly ImageProperties properties;
        private readonly ITextReader textReader;
        private readonly TextPreprocessor textPreprocessor;
        private readonly IColorGenerator colorGenerator;
        private readonly ICloudLayouter layouter;

        public TagCloudVisualizator(
            ImageProperties properties,
            ITextReader textReader,
            TextPreprocessor textPreprocessor,
            IColorGenerator colorGenerator,
            ICloudLayouter layouter)
        {
            this.properties = properties;
            this.textReader = textReader;
            this.textPreprocessor = textPreprocessor;
            this.colorGenerator = colorGenerator;
            this.layouter = layouter;
        }

        public Bitmap GetTagCloud(string filePath)
        {
            var text = textReader.ReadText(filePath);
            var words = textPreprocessor.PreprocessText(text);
            var normalizedWordsCount = words.NormalizeByMin();

            var bitmap = new Bitmap(properties.ImageSize.Width, properties.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach(var wordPair in words)
            {
                var word = wordPair.Key;
                var font = new Font(properties.TextFont, 10 + 2 * wordPair.Value);
                var boundingBoxSize = graphics.MeasureString(wordPair.Key, font).ToSize();

                var rectangle = layouter.PutNextRectangle(boundingBoxSize);
                graphics.DrawString(wordPair.Key, 
                    font, 
                    new SolidBrush(colorGenerator.GenerateTextColor(word, rectangle)), 
                    rectangle.Location);
            }

            return bitmap;
        }
    }
}
