using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.DataReader;
using TagsCloudContainer.Filter;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class BoringWordsFilter_Should
    {
        private IBoringWordsFilterSettings settings;
        private IDataReader dataReader;
        private BoringWordsFilter filter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            settings = A.Fake<IBoringWordsFilterSettings>();
        }

        [SetUp]
        public void SetUp()
        {
            dataReader = A.Fake<IDataReader>();
        }

        [Test]
        public void FilterOut_EmptyCollection_ReturnEmptyCollection()
        {
            var boringWords = new List<string>();
            var words = new List<string>();
            A.CallTo(() => dataReader.Read(null))
                .Returns(boringWords);

            new BoringWordsFilter(settings, dataReader).FilterOut(words)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void FilterOut_EmptyBoringWords_FilterOutNothing()
        {
            var boringWords = new List<string>();
            var words = new List<string>()
            {
                "modest",
                "half",
                "executive",
                "accumulation",
                "straw"
            };
            A.CallTo(() => dataReader.Read(null))
                .WithAnyArguments()
                .Returns(boringWords);

            new BoringWordsFilter(settings, dataReader).FilterOut(words)
                .Should()
                .BeEquivalentTo(words);
        }

        [Test]
        public void FilterOut_Correctly()
        {
            var boringWords = new List<string>
            {
                "open",
                "trouble",
                "investigation"
            };
            var words = new List<string>
            {
                "block",
                "line",
                "open",
                "trouble",
                "investigation",
                "cast",
                "reaction",
                "dangerous",
                "pole",
                "literature"
            };
            A.CallTo(() => dataReader.Read(null))
                .WithAnyArguments()
                .Returns(boringWords);

            new BoringWordsFilter(settings, dataReader).FilterOut(words)
                .Intersect(boringWords)
                .Should()
                .BeEmpty();
        }
    }
}