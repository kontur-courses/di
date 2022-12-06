using System.Drawing;
using FluentAssertions;
using TagsCloud.FigurePatterns.Implementation;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class SpiralPatternShould
    {
        private SpiralPatterPointProvider spiralPatterPointProvider;

        [SetUp]
        public void SetUp()
        {
            spiralPatterPointProvider = new SpiralPatterPointProvider(Point.Empty, 1);
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.IncorrectStepCount))]
        [Parallelizable(scope: ParallelScope.All)] 
        public void Ctor_IncorrectStep_ArgumentException(int steps)
        {
            // ReSharper disable once ObjectCreationAsStatement
            var createSpiralPattern = (Action) (() => new SpiralPatterPointProvider(Point.Empty, steps));
            createSpiralPattern.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.IncorrectStepCount))]
        [Parallelizable(scope: ParallelScope.All)] 
        public void StepSetter_SetIncorrectStep_ArgumentException(int steps)
        {
            var setStep = (Action) (() => spiralPatterPointProvider.Step = steps);
            setStep.Should().Throw<ArgumentException>();
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.CorrectStepCount))]
        [Parallelizable(scope: ParallelScope.None)] 
        public void Ctor_CorrectStep_EqualSteps(int steps)
        {
            var spiralInstance = new SpiralPatterPointProvider(Point.Empty, steps);
            spiralInstance.Step.Should().Be(steps);
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.CorrectStepCount))]
        [Parallelizable(scope: ParallelScope.None)] 
        public void StepGetter_CorrectStep_EqualSteps(int steps)
        {
            spiralPatterPointProvider.Step = steps;
            spiralPatterPointProvider.Step.Should().Be(steps);
        }
    }
}