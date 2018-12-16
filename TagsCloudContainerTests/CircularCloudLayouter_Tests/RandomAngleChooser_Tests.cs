using System;
using NUnit.Framework;
using TagsCloudContainer.CircularCloudLayouters;

namespace TagsCloudContainerTests.CircularCloudLayouter_Tests
{
    [TestFixture]
    public class RandomAngleChooser_Tests
    {
        private RandomAngleChooser randomAngleChooser;
        private readonly Random random = new Random();

        [SetUp]
        public void SetUp()
        {
            randomAngleChooser = new RandomAngleChooser(random);
        }
        


    }
}