using System.IO;
using System.Text;
using NUnit.Framework;
using Autofac;
using FluentAssertions;
using TagsCloudContainer;
using TagsCloudContainer.Clients;
using TagsCloudContainer.ContainerConfigurers;

namespace CloudContainerTests
{
    [TestFixture]
    public class ApplicationShould
    {
        private string inputFilePath = "words.txt";
        private string outputFilePath = "tagcloud.png";

        [SetUp]
        public void CreateInputFile()
        {
            var word = "C#";
            using (var fs = File.Create(inputFilePath)) { }
            using (StreamWriter sw = new StreamWriter(inputFilePath, false, Encoding.Default))
                sw.WriteLine(word);
        }

        [TearDown]
        public void DeleteInputFile()
        {
            DeleteFileIfExists(inputFilePath);
        }

        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        [Test]
        public void CreateOutputFile()
        {
            var args = new string[] { inputFilePath, outputFilePath };
            var container = GetContainerFromArgs(args);

            using (var scope = container.BeginLifetimeScope())
            {
                var painter = scope.Resolve<CloudPainter>();
                painter.Draw(scope.Resolve<UserConfig>().OutputFile);
            }

            File.Exists(outputFilePath).Should().BeTrue();
            DeleteFileIfExists(outputFilePath);
        }

        private IContainer GetContainerFromArgs(string[] args)
        {
            var builder = new ContainerBuilder();
            var ac = new AutofacConfigurer(args, builder);
            var container = ac.GetContainer();
            return container;
        }
    }
}