using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly IFileReader fileReader;
        private readonly IWordPreparator wordPreparator;
        private readonly ICloudLayouter cloudLayouter;
        private readonly IImageGenerator imageGenerator;
        private readonly IFrequencyCounter frequencyCounter;

        public TagCloudCreator(IFileReader fileReader, IWordPreparator wordPreparator, ICloudLayouter cloudLayouter,
            IImageGenerator imageGenerator, IFrequencyCounter frequencyCounter)
        {
            this.fileReader = fileReader;
            this.wordPreparator = wordPreparator;
            this.cloudLayouter = cloudLayouter;
            this.imageGenerator = imageGenerator;
            this.frequencyCounter = frequencyCounter;
        }

        public void CreateAndSaveCloudFromTo(string inputPath, string outputPath)
        {
            var words = fileReader.GetWordsFromFile(new StreamReader(inputPath), new[] { ' ' });
            var preparedWords = wordPreparator.GetPreparedWords(words.ToList()).ToList();
            var freqDictionary = frequencyCounter.GetFrequencyDictionary(preparedWords);

            var tags = new List<ITag>();
            foreach (var word in freqDictionary.Keys.OrderByDescending(x => freqDictionary[x]))
            {
                var rectangle = cloudLayouter.PutNextRectangle(word, 20);
                tags.Add(new Tag(rectangle, word, freqDictionary[word]));
            }

            var image = imageGenerator.GenerateTagCloudBitmap(tags.OrderByDescending(tag => tag.Frequency));

            image.Save(outputPath);
        }
    }
}