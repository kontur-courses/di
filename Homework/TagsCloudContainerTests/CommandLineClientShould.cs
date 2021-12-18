using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CLI;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Client;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;

namespace CloudContainerTests
{
    [TestFixture]
    public class CommandLineClientShould
    {
        private static string[] commonArgs =
        {
            "--input", "input.txt", "-o", "tagcloud.png", "-c", "2",
            "-h", "5000", "-w", "5000", "-i", "jpeg", "-r", "log", "-s", "20"
        };

        private static IUserConfig commonConfig = new CommandLineConfig
        {
            InputFile = "input.txt",
            OutputFile = "tagcloud.png",
            TagsColors = new CyberpunkScheme(),
            ImageSize = new Size(5000, 5000),
            ImageFormat = ImageFormat.Jpeg,
            TagsFontName = "Arial",
            Spiral = new LogarithmSpiral(new Point(2500, 2500)),
            TagsFontSize = 20

        };

        [SetUp]
        public void CreateInput()
        {
            using (File.Create("input.txt")) { }
        }


        [TearDown]
        public void DeleteInput()
        {
            File.Delete("input.txt");
        }

        [TestCase("-w", "0", TestName = "width is equal zero")]
        [TestCase("-h", "-5", TestName = "height is less than zero")]
        [TestCase("-c", "4", TestName = "unknown color is given")]
        [TestCase("-f", "png", TestName = "text format is not txt")]
        [TestCase("-i", "doc", TestName = "output image format is unknown")]
        [TestCase("-r", "big", TestName = "unknown spiral is given")]
        [TestCase("-s", "-100", TestName = "font size is equal zero")]
        [TestCase("-n", "Aerials", TestName = "unknown font name is given")]
        public void Throw_Argument_Exception_When(string key, string arg)
        {
            var arrayArgs = new string[] {"--input", "input.txt", key, arg};

            FluentActions.Invoking(() => new Client(arrayArgs)).Should()
                .Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(Args))]
        public void Make_Config_Correctly_When(string[] args, IUserConfig expectedConfig)
        {
            var config = new Client(args).UserConfig;

            config.Should().BeEquivalentTo(expectedConfig, options =>
                options.Excluding(c => c.Tags)
                    .Excluding(c => c.ImageCenter)
                    .Excluding(c => c.TagsColors)
                    .Excluding(c => c.TextParser));
        }

        private static IEnumerable<TestCaseData> Args()
        {
            yield return new TestCaseData(
                commonArgs,
                commonConfig
            ).SetName("common case is given");
            commonArgs[11] = "png";
            commonConfig.ImageFormat = ImageFormat.Png;
            yield return new TestCaseData(
                commonArgs,
                commonConfig
            ).SetName("image format is changed");
            commonArgs[13] = "sqr";
            commonConfig.Spiral = new SquareSpiral(commonConfig.ImageCenter);
            yield return new TestCaseData(
                commonArgs,
                commonConfig
            ).SetName("spiral is changed");
            commonArgs[15] = "25";
            commonConfig.TagsFontSize = 25;
            yield return new TestCaseData(
                commonArgs,
                commonConfig
            ).SetName("font size is changed");
            commonArgs[7] = "4000";
            commonConfig.ImageSize = new Size(5000, 4000);
            commonConfig.ImageCenter = new Point(4000 / 2, 5000 /2);
            yield return new TestCaseData(
                commonArgs,
                commonConfig
            ).SetName("image size is changed");
        }
    }
}
