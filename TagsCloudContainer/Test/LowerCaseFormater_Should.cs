using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer
{
    [TestFixture]
    class LowerCaseFormater_Should
    {
        private LowerCaseFormater lowerCaseFormater;

        [SetUp]
        public void SetUp()
        {
            lowerCaseFormater = new LowerCaseFormater();
        }

        [Test]
        public void HandleWords_ToLowerCase()
        {
            lowerCaseFormater.HandleWords(new string[] { "A", "B", "c" }).Should()
                .BeEquivalentTo(new string[] { "a", "b", "c" });
        }
    }

}
