using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using TagCloudGenerator.TagClouds;

namespace TagCloudGenerator.Tests.WrongVisualization
{
    public static class WrongVisualizationSaver
    {
        public static string SaveAndGetPathToWrongVisualization(
            TagCloud<TagType> tagCloud, Size imageSize, string directoryName)
        {
            var failedTestFilename = $"{GetCurrentTestName()}_{DateTime.Now:dd.MM.yyyy-HH.mm.ss}.png";

            Directory.CreateDirectory(directoryName);
            var wrongVisualisationImageFilepath = Path.Combine(
                TestContext.CurrentContext.TestDirectory, directoryName, failedTestFilename);

            using var image = tagCloud.CreateBitmap(null, null, imageSize);
            image.Save(wrongVisualisationImageFilepath);

            TestContext.WriteLine($@"Tag cloud visualization saved to file:{Environment.NewLine
                                      }{wrongVisualisationImageFilepath}");

            return wrongVisualisationImageFilepath;

            string GetCurrentTestName() => TestContext.CurrentContext.Test is var test &&
                                           test.MethodName == test.Name
                                               ? test.Name
                                               : test.MethodName + test.Name;
        }
    }
}