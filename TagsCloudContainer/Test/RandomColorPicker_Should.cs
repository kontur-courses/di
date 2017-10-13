using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer.Test
{
    [TestFixture]
    class RandomColorPicker_Should
    {
        private RandomColorPicker randomColorPicker;

        [SetUp]
        public void SetUp()
        {
            randomColorPicker = new RandomColorPicker();
        }

        [Test]
        public void DoSomething_WhenSomething()
        {
            randomColorPicker.GenerateColor().Should().BeOfType<SolidBrush>();
        }

    }
}
