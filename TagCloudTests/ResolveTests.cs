using System;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.IServices;

namespace TagCloudTests
{
    [TestFixture]
    public class ResolveTests
    {
        private WindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            container = TagCloud.Program.GetContainer();
        }

        [Test]
        public void Container_Should_ResolveIClientCorrectly()
        { 
            Action resolve = () => container.Resolve<IClient>();
            resolve.Should().NotThrow();
        }
    }
}
