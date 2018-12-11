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
            changer.ChangeWord("a").Should().Be("a");
        }
    
        [Test]
        public void ChangeWord_WhenUpperCase()
        {
            changer.ChangeWord("A").Should().Be("a");
        }

        [Test]
        public void ChangeWord_WhenUpperAndLowerCase()
        {
            changer.ChangeWord("Aa").Should().Be("aa");
        }

        [Test]
        public void ChangeWord_WhenSeveralUpperCase()
        {
            changer.ChangeWord("AA").Should().Be("aa");
        }
    }
}
