using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer
{
    [TestFixture]
    class TagSizeNormalizer_Should
    {
        private ITagSizeNormalizer tagSizeNormalizer;

        [SetUp]
        public void SetUp()
        {
            tagSizeNormalizer = new TagSizeNormalizer(new Font(FontFamily.GenericMonospace, 16));
        }

        [Test]
        public void GetSize_ForWord()
        {
            tagSizeNormalizer.GetTagSize("Альбина").Should().BeOfType<Size>();
        }
    }
}
