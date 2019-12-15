using System;
using System.Linq;
using TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors;
using TagCloudGenerator.GeneratorCore.Extensions;

namespace TagCloudGenerator.GeneratorCore
{
    public class CloudGenerator
    {
        private readonly CloudContextGenerator cloudContextGenerator;
        private readonly CloudVocabularyPreprocessor vocabularyPreprocessor;

        public CloudGenerator(CloudContextGenerator cloudContextGenerator,
                              CloudVocabularyPreprocessor vocabularyPreprocessor)
        {
            this.cloudContextGenerator = cloudContextGenerator;
            this.vocabularyPreprocessor = vocabularyPreprocessor;
        }

        public void CreateTagCloudImage()
        {
            var cloudContext = cloudContextGenerator.GetTagCloudContext();
            var processedVocabulary = vocabularyPreprocessor.Process(cloudContext.TagCloudContent).ToArray();

            var shuffledContentStrings = processedVocabulary
                .Take(1)
                .Concat(processedVocabulary.Skip(1).SequenceShuffle(new Random()))
                .ToArray();

            using var bitmap = cloudContext.Cloud.CreateBitmap(
                shuffledContentStrings, cloudContext.CloudLayouter, cloudContext.ImageSize);

            bitmap.Save(cloudContext.ImageName);
        }
    }
}