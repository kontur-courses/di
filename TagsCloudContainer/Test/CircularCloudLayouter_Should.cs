using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudContainer
{
    [TestFixture]
    class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter ccl;

        [SetUp]
        public void SetUp()
        {
            ccl = new CircularCloudLayouter(new Point(500, 300));
        }


        [Test]
        public void ThrowException_IfSizeIsDefault()
        {
            Action act = () => ccl.PutNextRectangle(default(Size));
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void CloudData_NotBeEmpty_AfterAddRectangle()
        {
            var size = new Size(200, 100);

            ccl.PutNextRectangle(size);

            ccl.CloudData.Should().NotBeEmpty();
        }

        [Test, Timeout(1000)]
        public void CloudData_HaveSomeReactangle_AfterAddSomeRectangle()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                ccl.PutNextRectangle(new Size(random.Next(20, 80), random.Next(10, 40)));
            }

            ccl.CloudData.Should().HaveCount(10);

        }

        [TearDown]
        public void TearDown()
        {
            var testState = TestContext.CurrentContext.Result.Outcome;

            if (Equals(testState, ResultState.Failure) || Equals(testState, ResultState.Error))
            {
                //Console.WriteLine(TestContext.CurrentContext.Result.Message);
            }
        }

    }
}
