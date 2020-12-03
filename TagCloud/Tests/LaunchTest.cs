using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class LaunchTest
    {
        [Test]
        public void Test()
        {
            var result = Program.Main(new string[0]);
            result.Should().Be(0);
        }
    }
}