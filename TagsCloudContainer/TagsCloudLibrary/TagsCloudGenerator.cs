using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudLibrary.Colorers;
using TagsCloudLibrary.Layouters;
using TagsCloudLibrary.Preprocessors;
using TagsCloudLibrary.Readers;
using TagsCloudLibrary.WordsExtractor;
using TagsCloudLibrary.Writers;

namespace TagsCloudLibrary
{
    public class TagsCloudGenerator
    {

        private readonly IReader reader;
        private readonly IWordsExtractor extractor;
        private readonly List<IPreprocessor> preprocessors;
        private readonly ILayouter layouter;
        private readonly IColorer colorer;
        private readonly FontFamily wordsFontFamily;
        private readonly IImageWriter imageWriter;

        public TagsCloudGenerator(
            IReader reader,
            IWordsExtractor extractor,
            IEnumerable<IPreprocessor> preprocessors,
            ILayouter layouter, 
            IColorer colorer,
            FontFamily wordsFontFamily,
            IImageWriter imageWriter)
        {
            this.reader = reader;
            this.extractor = extractor;

            this.preprocessors = preprocessors.ToList();
            this.preprocessors = this.preprocessors.OrderBy(p => p.Priority).ToList();

            this.layouter = layouter;
            this.colorer = colorer;
            this.wordsFontFamily = wordsFontFamily;
            this.imageWriter = imageWriter;
        }

        public void GenerateFromFile(string inputFile, string outputFile, int imageWidth, int imageHeight)
        {
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException($"File {inputFile} not found");
            }

            using (var fs = File.OpenRead(inputFile))
            {
                var text = reader.Read(fs);
                var words = extractor.ExtractWords(text);

                foreach (var preprocessor in preprocessors)
                {
                    words = preprocessor.Act(words);
                }

                var wordStatistics = new Dictionary<string, int>();
                var wordsArray = words.ToArray();
                foreach (var word in wordsArray)
                {
                    if (wordStatistics.ContainsKey(word))
                    {
                        ++wordStatistics[word];
                    }
                    else
                    {
                        wordStatistics[word] = 1;
                    }
                }

                var totalWords = wordsArray.Length;

                var wordsWithSizes = wordStatistics
                    .Select(pair => new Tuple<string, int>(pair.Key, pair.Value))
                    .OrderByDescending(tuple => tuple.Item2)
                    .ToList();

                var image = new Bitmap(imageWidth, imageHeight);
                var graphics = Graphics.FromImage(image);
                foreach (var wordWithSize in wordsWithSizes)
                {
                    var (word, size) = wordWithSize;
                    var font = new Font(wordsFontFamily, (float) size * imageHeight / totalWords * 2);
                    var color = colorer.ColorForWord(word, (double) size / totalWords);
                    var textSize = graphics.MeasureString(word, font).ToSize();
                    if (textSize.Width <= 0 || textSize.Height <= 0) continue;
                    var rectangle = layouter.PutNextRectangle(textSize);
                    graphics.DrawString(
                        word,
                        font,
                        new SolidBrush(color),
                        rectangle.X + imageWidth / 2,
                        rectangle.Y + imageHeight / 2);   
                }
                
                imageWriter.WriteBitmapToFile(image, outputFile);
            }
        }
    }
}
