using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainerCore.CircularLayouter;

namespace TagCloudContainerTests.CircularCloudLayouterTests
{
    [TestFixture]
    public class CloudLayouterParametersTests
    {
#pragma warning disable CS8618
        private CircularCloudLayoutParameters circularCloudLayoutParameters;
#pragma warning restore CS8618

        [SetUp]
        public void SetParameters()
        {
            circularCloudLayoutParameters = new CircularCloudLayoutParameters();
        }

        [Test]
        public void BoostRadius_ShouldChange_CurrentRadius()
        {
            var firstRadius = circularCloudLayoutParameters.Radius;

            circularCloudLayoutParameters.BoostRadius();

            circularCloudLayoutParameters.Radius.Should().NotBe(firstRadius);
        }

        [Test]
        public void BoostRadius_ShouldChange_StepAngle()
        {
            circularCloudLayoutParameters = new CircularCloudLayoutParameters(startAngle: 2, countBoostsForChangeAngle: 2);
            
            circularCloudLayoutParameters.BoostRadius();
            circularCloudLayoutParameters.BoostRadius();

            circularCloudLayoutParameters.StepAngle.Should().BeApproximately(1f, 0.001f);
        }

        [Test]
        public void ResetRadius_ShouldSetCurrentRadius_ToFirstRadius()
        {
            var firstRadius = circularCloudLayoutParameters.Radius;

            circularCloudLayoutParameters.BoostRadius();
            circularCloudLayoutParameters.BoostRadius();
            circularCloudLayoutParameters.ResetRadius();

            circularCloudLayoutParameters.Radius.Should().Be(firstRadius);
        }

        [Test]
        public void CurrentAngle_ShouldChangeSelf_AfterInvoke()
        {
            var firstAngle = circularCloudLayoutParameters.NextAngle;
            
            circularCloudLayoutParameters.NextAngle.Should().NotBe(firstAngle);
        }
    }
}