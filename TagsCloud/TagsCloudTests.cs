using System;
using System.IO;
using NUnit.Framework;

namespace TagsCloud
{
    [TestFixture]
    public class TagsCloudTests
    {
        [Test]
        public void CreateCloud()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent;
            var samplePath = $"{directoryInfo.FullName}\\Samples\\sample.png";
            new FileInfo(samplePath).Delete();
            var settings = $"{directoryInfo.FullName}\\settings.txt";
            using var writer = new StreamWriter($"{directoryInfo.FullName}\\settings.txt", false);
            writer.WriteLine($"{directoryInfo.FullName}\\example.txt");
            writer.WriteLine("4000");
            writer.WriteLine("4000");
            writer.WriteLine("Calibri");
            writer.Dispose();
            
            using var reader = new StreamReader(settings);
            Console.SetIn(reader);
            Program.MakeCloud();

            var actual = new FileInfo(samplePath);
            Assert.True(actual.Exists);
        }
    }
}