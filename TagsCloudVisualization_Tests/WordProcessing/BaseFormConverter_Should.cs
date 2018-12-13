using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;
// ReSharper disable StringLiteralTypo

namespace TagsCloudVisualization_Tests.WordProcessing
{
    public class BaseFormConverter_Should
    {
        private BaseFormConverter converter;

        [SetUp]
        public void SetUp()
        {
            var path = TestContext.CurrentContext.TestDirectory;
            converter = new BaseFormConverter($"{path}/Dictionaries/ru.aff", $"{path}/Dictionaries/ru.dic");
        }

        [Test]
        public void ChangeWordsToBase_WhenRussiaWord()
        {
            converter.ChangeWords(new[] {"сознания"}).Should().BeEquivalentTo("сознание");
        }

        [Test]
        public void ChangeWordsToBase_Not_WhenNotRussianWord()
        {
            converter.ChangeWords(new[] {"unchanged"}).Should().BeEquivalentTo("unchanged");
        }

        [Test]
        public void ChangeWordsToBase_OnlyRussia_WhenDifferentWord()
        {
            converter.ChangeWords(new[] {"сознания", "unchanged"}).Should().BeEquivalentTo("сознание", "unchanged");
        }

        [Test] public void ChangeWordsToBase_WhenMultipleRussiaWord()
        {
            converter.ChangeWords(new[] {"сознания", "горения"}).Should().BeEquivalentTo("сознание", "горение");
        }    
    }
}
