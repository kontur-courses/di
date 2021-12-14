using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Clients;

namespace CloudContainerTests
{
    [TestFixture]
    public class CommandLineClientShould
    {
        [Test]
        public void ThrowExceptionWhen_AnyNecessaryArgumentIsAbsent()
        {
            var onlyInputFile = new string[] { "input" };
            var emptyArgs = new string[] { };

            ShouldThrowArgumentNullWithArgs(onlyInputFile);
            ShouldThrowArgumentNullWithArgs(emptyArgs);
        }

        private static void ShouldThrowArgumentNullWithArgs(string[] args)
        {
            FluentActions
                .Invoking(() => new CommandLineClient(args))
                .Should()
                .Throw<ArgumentNullException>();
        }
    }
}
