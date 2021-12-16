using System;
using System.IO;
using CLI;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Client;

namespace CloudContainerTests
{
    [TestFixture]
    public class CLIShould
    {
        private string inputFile = "input.txt";

        [SetUp]
        public void CreateInputFile()
        {
            using (var fs = File.Create(inputFile)) { }
        }

        [TearDown]
        public void DeleteInputFile()
        {
            File.Delete(inputFile);
        }

        [Test]
        public void Throw_Exception_When_Necessary_Argument_Is_Absent()
        {
            var emptyArgs = new string[] { };

            ShouldThrowArgumentNullWithArgs(emptyArgs);
        }

        [Test]
        public void Get_Default_Values_When_Only_InputFile_Is_Given()
        {
            var onlyNecessaryArgs = new[] { inputFile};
            var defaultOptions = GetDefaultOptions(onlyNecessaryArgs);
            var expectedUserConfig = new UserConfig();
            expectedUserConfig.GetConfig(defaultOptions);

            var client = new Client(onlyNecessaryArgs);

            client.UserConfig.Should().BeEquivalentTo(expectedUserConfig);
        }

        [Test]
        public void Parse_Arguments_Correctly()
        {
            var args = new[] { inputFile, "-o", "output", "-w", 
                "2000", "-h", "1500", "-n", "Times New Roman", "-s", "50", "-c", "0"};
            var parsedArgs = new Options
            {
                Input = inputFile, Output = "output",
                Width = 2000, Height = 1500,
                FontName = "Times New Roman", FontSize = 50, Color = 0,
                InputFileFormat = "txt", OutputFileFormat = "png",
                Spiral = "log", ExcludedWords = new[] { "" }
            };
            var expectedUserConfig = new UserConfig();
            expectedUserConfig.GetConfig(parsedArgs);

            var client = new Client(args);

            client.UserConfig.Should().BeEquivalentTo(expectedUserConfig);
        }

        private Options GetDefaultOptions(string[] onlyNecessaryArgs)
        {
            return new Options()
            {
                Input = onlyNecessaryArgs[0], Output = "tagcloud",
                Height = 1000, Width = 1000,
                FontName = "Arial", FontSize = 20, Color = 0,
                InputFileFormat = "txt", OutputFileFormat = "png",
                Spiral = "log", ExcludedWords = new [] {""}
            };
        }

        private static void ShouldThrowArgumentNullWithArgs(string[] args)
        {
            FluentActions
                .Invoking(() => new Client(args))
                .Should()
                .Throw<ArgumentException>();
        }
    }
}
