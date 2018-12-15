using Autofac;
using NUnit.Framework;
using TagCloud.Utility.Container;

namespace TagCloud.Tests
{
    [TestFixture]
    public class TestBase
    {
        protected readonly IContainer container = ContainerConfig.StandartContainer;
    }
}