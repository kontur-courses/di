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

        private IReader reader;
        private IWordsExtractor extractor;
        private List<IPreprocessor> preprocessors = new List<IPreprocessor>();
        private ILayouter layouter;
        private IColorer colorer;
        private readonly FontFamily wordsFontFamily;
        private IImageWriter imageWriter;

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
            this.preprocessors.Sort((p1, p2) => p1.Priority - p2.Priority);

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
                foreach (var word in words)
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

                var totalWords = words.Count();

                var wordsWithSizes = wordStatistics
                    .Select(pair => new Tuple<string, int>(pair.Key, pair.Value))
                    .ToList();
                wordsWithSizes.Sort((t1, t2) => (t2.Item2 - t1.Item2));

                var image = new Bitmap(imageWidth, imageHeight);
                var graphics = Graphics.FromImage(image);
                foreach (var wordWithSize in wordsWithSizes)
                {
                    var (word, size) = wordWithSize;
                    var font = new Font(wordsFontFamily, (float) size * imageHeight / totalWords * 2);
                    var color = colorer.ColorForWord(word, size);
                    var rectangle = layouter.PutNextRectangle(graphics.MeasureString(word, font).ToSize());
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
