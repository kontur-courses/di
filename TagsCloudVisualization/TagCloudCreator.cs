#region

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IFileReader fileReader;
        private readonly IFrequencyCounter frequencyCounter;
        private readonly IImageGenerator imageGenerator;
        private readonly IWordPreparator wordPreparator;

        public TagCloudCreator(IFileReader fileReader,
            IWordPreparator wordPreparator,
            ICloudLayouter cloudLayouter,
            IImageGenerator imageGenerator,
            IFrequencyCounter frequencyCounter)
        {
            this.fileReader = fileReader;
            this.wordPreparator = wordPreparator;
            this.cloudLayouter = cloudLayouter;
            this.imageGenerator = imageGenerator;
            this.frequencyCounter = frequencyCounter;
        }

        public Bitmap CreateAndSaveCloudFromTextFile(string inputPath)
        {
            var words = fileReader.GetWordsFromFile(inputPath, new[] { ' ' });
            var preparedWords = wordPreparator.GetPreparedWords(words.ToList()).ToList();
            var freqDictionary = frequencyCounter.GetFrequencyDictionary(preparedWords);

            var tags = new List<ITag>();
            foreach (var word in freqDictionary.Keys.OrderByDescending(x => freqDictionary[x]))
            {
                var rectangle = cloudLayouter.PutNextRectangle(word, 20);
                tags.Add(new Tag(rectangle, word, freqDictionary[word]));
            }

            var image = imageGenerator.GenerateTagCloudBitmap(tags.OrderByDescending(tag => tag.Frequency));

            return image;
        }

        public static Assembly GetAssemblyInfo()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}