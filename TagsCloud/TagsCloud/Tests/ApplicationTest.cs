using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Visualization.Tag;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class ApplicationTest
    {
        private string _input;

        private readonly string _text =
            "лимон\nЛиМоНа\nЛИМОН\nчаЙ\nчая\nМёд\nс\nимбирь\nимбирь\nимбирём\nбегал\nбегает\nбегают\nбег";

        [OneTimeSetUp]
        public void FirstSetUp()
        {
            var rnd = new Random();
            _input = $"test{rnd.Next(0, 2000000)}.txt";
            while (File.Exists(_input))
            {
              _input =  $"test{rnd.Next(0, 2000000)}.txt";
            }
            using (var sw = new StreamWriter(_input, false, System.Text.Encoding.Default))
            {
                sw.Write(_text);
            }

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            File.Delete(_input);
        }

        private static List<Tag> GetTags(IEnumerable<string> args)
        {
            var container = ContainerConstructor.Configure(args);
            var app = container.Resolve<Application>();
            return app.GetTags().ToList();
        }

        [Test]
        public void GetTags_ThrowException_OnWrongArgs()
        {
            Action act = () => GetTags(new[] {"--pew"});
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTags_ReturnsCorrectTagsCount_OnDefaultOptions()
        {
            var tags = GetTags(new[] {"--file", _input});
            tags.Count.Should().Be(11);
        }

        [Test]
        public void GetTags_ReturnsCorrectTagsCount_OnInfinitiveFormOption()
        {
            var tags = GetTags(new[] {"--file", _input, "--inf"});
            tags.Count.Should().Be(6);
        }

        [Test]
        public void GetTags_ReturnTagsWithCorrectTagsSize()
        {
            var tags = GetTags(new[] {"--file", _input});
            foreach (var tag1 in tags)
            {
                foreach (var tag2 in tags.Where(tag2 => tag1.Frequency > tag2.Frequency))
                    tag1.Size.Should().BeGreaterThan(tag2.Size);
            }
        }


        [Test]
        public void GetTags_ReturnsTagsWithLoweredWords()
        {
            var tags = GetTags(new[] {"--file", _input});
            foreach (var tag in tags)
            {
                (tag.Word.ToLower() == tag.Word).Should().BeTrue();
            }
        }
    }
}