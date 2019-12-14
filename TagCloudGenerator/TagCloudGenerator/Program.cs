using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.CloudVocabularyPreprocessors;
using TagCloudGenerator.Extensions;
using TagCloudGenerator.TagClouds;
using TagCloudGenerator.Tags;
using TagCloudGenerator.UserInterfaces;
using TagCloudGenerator.UserInterfaces.CmdClient;
using TagCloudGenerator.UserInterfaces.CmdClient.CommandLineVerbs;

namespace TagCloudGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var cloudOptions = ArgumentParser.ReadCommandLineOptions(args);
            var cloudContext = TagCloudOptionsParser.GetTegCloudContext(cloudOptions, CloudConstructor);

            cloudContext.TagCloudContent = ProcessVocabulary(cloudContext);

            CreateTagCloudImage(cloudContext);

            TagCloud<TagType> CloudConstructor(Color backgroundColor,
                                               Dictionary<TagType, TagStyle> tagStyleByTagType) =>
                cloudOptions is DoubleFontsCloud
                    ? new CommonWordsCloud(backgroundColor, tagStyleByTagType)
                    : (TagCloud<TagType>)new WebCloud(backgroundColor, tagStyleByTagType);
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