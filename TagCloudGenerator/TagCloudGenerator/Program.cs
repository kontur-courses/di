using System;
using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.CloudVocabularyPreprocessors;
using TagCloudGenerator.Extensions;
using TagCloudGenerator.UserInterfaces.CmdClient;

namespace TagCloudGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var cloudContext = ArgumentParser.GetTegCloudContext(args);
            cloudContext.TagCloudContent = ProcessVocabulary(cloudContext);

            CreateTagCloudImage(cloudContext);
        }

        private static IEnumerable<string> ProcessVocabulary(TagCloudContext cloudContext)
        {
            CloudVocabularyPreprocessor preprocessor = new ExcludingPreprocessors(null, cloudContext.ExcludedWords);
            preprocessor = new ToLowerPreprocessor(preprocessor);

            return preprocessor.Process(cloudContext.TagCloudContent);
        }

        private static void CreateTagCloudImage(TagCloudContext cloudContext)
        {
            var shuffledContentStrings = cloudContext.TagCloudContent.Take(1)
                .Concat(cloudContext.TagCloudContent.Skip(1).SequenceShuffle(new Random()))
                .ToArray();

            using var bitmap = cloudContext.Cloud.CreateBitmap(
                shuffledContentStrings, cloudContext.CloudLayouter, cloudContext.ImageSize);

            bitmap.Save(cloudContext.ImageName);
        }
    }
}