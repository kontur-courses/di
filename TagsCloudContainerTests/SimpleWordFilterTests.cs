using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.TokensAndSettings;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class SimpleWordFilterTests
    {
        [Test]
        public void IsCorrect_BoringWond_False()
        {
            var word = new ProcessedWord("на", "PR");

            var filter = new SimpleWordFilter();

            var result = filter.IsCorrect(word);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsCorrect_NormalWord_True()
        {
            var word = new ProcessedWord("дерево", "S");

            var filter = new SimpleWordFilter();

            var result = filter.IsCorrect(word);

            Assert.IsTrue(result);
        }
    }
}
