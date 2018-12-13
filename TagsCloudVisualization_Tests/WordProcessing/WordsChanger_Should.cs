using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    public class WordsChanger_Should
    {
        private WordsChanger changer;

        [SetUp]
        public void SetUp()
        {
            changer = new WordsChanger();
        }

        [Test]
        public void ChangeWord_WhenLowerCase()
        {
            changer.ChangeWords(new []{"a"}).Should().BeEquivalentTo("a");
        }
    
        [Test]
        public void ChangeWord_WhenUpperCase()
        {
            changer.ChangeWords(new []{"A"}).Should().BeEquivalentTo("a");
        }

        [Test]
        public void ChangeWord_WhenUpperAndLowerCase()
        {
            changer.ChangeWords(new []{"Aa"}).Should().BeEquivalentTo("aa");
        }

        [Test]
        public void ChangeWord_WhenSeveralUpperCase()
        {
            changer.ChangeWords(new []{"AA"}).Should().BeEquivalentTo("aa");
        }
    }
}
