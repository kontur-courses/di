using System.IO;
using System.Text;
using NUnit.Framework;
using Autofac;
using CLI;
using FluentAssertions;
using TagsCloudContainer;
using TagsCloudContainer.Client;
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
        public void Create_Output_File_When_Path_Is_Given()
        {
            var args = new[] { inputFilePath, outputFilePath};
            CheckIfOutptFileExists(outputFilePath, args);
        }

        [Test]
        public void Create_Output_File_When_Path_Is_Not_Given()
        {
            var args = new[] { inputFilePath};
            CheckIfOutptFileExists(outputFilePath, args);
        }

        private void CheckIfOutptFileExists(string outputFilePath, string[] args)
        {
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
            builder.RegisterInstance(new Client(args)).As<IClient>();
            var ac = new AutofacConfigurer(builder);
            var container = ac.GetContainer();
            return container;
        }
    }
}