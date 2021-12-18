using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextParsers;
namespace CloudContainerTests
{
    [TestFixture]
    public class TextParserShould
    {
        private TagsReader tagsReader = new TagsReader(new string[0]);
        private List<Func<string, string>> handlers = new List<Func<string, string>>();
        private Action<string, Dictionary<string, int>> grouper =
                (word, dict) =>
                {
                    if (word == "") return;
                    if (!dict.TryGetValue(word, out _))
                        dict.Add(word, 0);
                    dict[word]++;
                };

        [SetUp]
        public void ClearPreviosSetup()
        {
            tagsReader = new TagsReader(new string[0]);
            handlers.Clear();
        }

        [Test]
        public void Leave_Dict_Empty_When_Input_Is_Empty()
        {
            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Count.Should().Be(0);
        }

        [Test]
        public void Leave_Dict_Empty_When_Word_Was_Excluded()
        {
            tagsReader = new TagsReader(new[] {"Go"});
            handlers.Add(s => s == "Go" ? "" : s);

            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Count.Should().Be(0);
        }

        [Test]
        public void Apply_Handlers()
        {
            tagsReader = new TagsReader(new[] { "Go", "Away" });
            handlers.Add(s => "Stay");
            handlers.Add(s => s.ToUpper());
            var expectedDict = new Dictionary<string, int>
            {
                {"STAY", 2}
            };

            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Should().BeEquivalentTo(expectedDict);
        }

        [Test]
        public void Apply_Handlers_In_Right_Order()
        {
            tagsReader = new TagsReader(new[] { "Go", "Away" });
            handlers.Add(s => "Stay");
            handlers.Add(s => s.ToUpper());
            handlers.Add(s => s.ToLower());
            handlers.Add(s => s.Substring(0, 3));
            var expectedDict = new Dictionary<string, int>
            {
                {"sta", 2}
            };

            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Should().BeEquivalentTo(expectedDict);
        }

        [Test]
        public void Count_Different_Words_Correctly()
        {
            tagsReader = new TagsReader(new[] { "Go", "Away", "Please" });
            handlers.Add(s => s == "Away" ? "" : s);
            var expectedDict = new Dictionary<string, int>
            {
                {"Go", 1},
                {"Please", 1},
            };

            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Should().BeEquivalentTo(expectedDict);
        }

        [Test]
        public void Count_Same_Words_Correctly()
        {
            tagsReader = new TagsReader(new[] { "Please", "Away", "Please", "Please", "Go" });
            var expectedDict = new Dictionary<string, int>
            {
                {"Go", 1},
                {"Away", 1},
                {"Please", 3},
            };

            var tp = new TextParser(tagsReader, handlers, grouper);

            tp.GetWordsCounts().Should().BeEquivalentTo(expectedDict);
        }
    }
}
