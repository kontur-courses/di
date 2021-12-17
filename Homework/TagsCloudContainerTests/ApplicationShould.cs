using Autofac;
using CLI;
using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Text;
using TagsCloudContainer;
using TagsCloudContainer.Client;
using TagsCloudContainer.ContainerConfigurers;

namespace CloudContainerTests
{
    [TestFixture]
    public class ApplicationShould
    {
        private const string InputFilePath = "words.txt";
        private const string OutputFilePath = "tagcloud.png";

        [SetUp]
        public void CreateInputFile()
        {
            var word = "C#";
            using (File.Create(InputFilePath)) { }
            using (var sw = new StreamWriter(InputFilePath, false, Encoding.Default))
                sw.WriteLine(word);
        }

        [TearDown]
        public void DeleteInputFile()
        {
            DeleteFileIfExists(InputFilePath);
        }

        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        [Test]
        public void Create_Output_File_When_Path_Is_Given()
        {
            var args = new[] { InputFilePath, OutputFilePath };
            CheckIfOutptFileExists(OutputFilePath, args);
        }

        [Test]
        public void Create_Output_File_When_Path_Is_Not_Given()
        {
            var args = new[] { InputFilePath };
            CheckIfOutptFileExists(OutputFilePath, args);
        }

        private void CheckIfOutptFileExists(string output, string[] args)
        {
            var container = GetContainerFromArgs(args);

            using (var scope = container.BeginLifetimeScope())
            {
                var painter = scope.Resolve<CloudPainter>();
                painter.Draw(scope.Resolve<UserConfig>().OutputFile);
            }

            File.Exists(output).Should().BeTrue();
            DeleteFileIfExists(output);
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