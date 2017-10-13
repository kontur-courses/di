using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer.Test
{
    [TestFixture]
    class WhiteColorPicker_Should
    {
        private WhiteColorPicker whiteColorPicker;

        [SetUp]
        public void SetUp()
        {
            whiteColorPicker = new WhiteColorPicker();
        }

        [Test]
        public void WhiteColorPicker_Generate_WhiteBrush()
        {
            whiteColorPicker.GenerateColor().Should().Be(Brushes.White);
        }
    }
}
