using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Client;

namespace CloudContainerTests
{
    [TestFixture]
    public class CommandLineClientShould
    {
        [Test]
        public void ThrowExceptionWhen_AnyNecessaryArgumentIsAbsent()
        {
            var onlyInputFile = new string[] { "input" };
            var emptyArgs = new string[] { };

            ShouldThrowArgumentNullWithArgs(onlyInputFile);
            ShouldThrowArgumentNullWithArgs(emptyArgs);
        }

        [Test]
        public void GetDefaultValuesWhen_OnlyNecessaryArgmentsAreGiven()
        {
            var onlyNecessaryArgs = new string[] { "input", "output" };
            var defaultOptions = GetDefaultOptions(onlyNecessaryArgs);
            var expectedUserConfig = new UserConfig();
            expectedUserConfig.GetConfig(defaultOptions);

            var client = new CommandLineClient(onlyNecessaryArgs);

            client.UserConfig.Should().BeEquivalentTo(expectedUserConfig);
        }

        [Test]
        public void ParseArgumentsCorrectly()
        {
            var args = new string[] { "input", "output", "-w", 
                "2000", "-h", "1500", "-n", "TimesNewRoman", "-s", "50", "-c", "0"};
            var parsedArgs = new Options();
            parsedArgs.Input = args[0];
            parsedArgs.Output = args[1];
            parsedArgs.Width = int.Parse(args[3]);
            parsedArgs.Height = int.Parse(args[5]);
            parsedArgs.FontName = args[7];
            parsedArgs.FontSize = int.Parse(args[9]);
            parsedArgs.Color = int.Parse(args[11]);
            var expectedUserConfig = new UserConfig();
            expectedUserConfig.GetConfig(parsedArgs);

            var client = new CommandLineClient(args);

            client.UserConfig.Should().BeEquivalentTo(expectedUserConfig);
        }

        private Options GetDefaultOptions(string[] onlyNecessaryArgs)
        {
            var defaultOptions = new Options();
            defaultOptions.Input = onlyNecessaryArgs[0];
            defaultOptions.Output = onlyNecessaryArgs[1];
            defaultOptions.Height = 1000;
            defaultOptions.Width = 1000;
            defaultOptions.FontName = "Arial";
            defaultOptions.FontSize = 20;
            defaultOptions.Color = 0;
            return defaultOptions;
        }

        private static void ShouldThrowArgumentNullWithArgs(string[] args)
        {
            FluentActions
                .Invoking(() => new CommandLineClient(args))
                .Should()
                .Throw<ArgumentNullException>();
        }
    }
}
