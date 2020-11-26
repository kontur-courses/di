using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudVisualisationTests.Infrastructure
{
    public static class TestingHelpers
    {
        public static string BaseOutputFilePath =>
            CreateDirectory(Path.Combine(CreateDirectory(TestContext.CurrentContext.WorkDirectory), "tests-output"));

        public static string GetOutputDirectory(string directoryName) =>
            CreateDirectory(Path.Combine(BaseOutputFilePath, directoryName));

        public static FileInfo GetTestFile(string fileName) => new FileInfo(Path.Combine(BaseInputDirectory, fileName));

        public static string BaseInputDirectory => Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData");

        public static void ClearDirectory(string fullPath)
        {
            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath, true);
            CreateDirectory(fullPath);
        }

        private static string CreateDirectory(string fullPath)
        {
            Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        public static Color RandomColor => Color.FromKnownColor(Randomizer.CreateRandomizer().NextEnum<KnownColor>());

        public static DirectoryInfo GoParentUntil(this DirectoryInfo baseDirectory, Func<DirectoryInfo, bool> predicate)
        {
            while (!predicate.Invoke(baseDirectory))
                baseDirectory = baseDirectory.Parent ?? throw new InvalidOperationException($"No directories left");
            return baseDirectory;
        }
        
        public static string FileNameOnly(this FileInfo fileInfo) => 
            fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
    }
}