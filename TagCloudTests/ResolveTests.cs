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
        public static WindsorContainer Container { get; set; } = TagCloud.Program.GetContainer();

        [Test]
        public void Container_Should_ResolveIClientCorrectly()
        { 
            Action resolve = () => Container.Resolve<IClient>();
            resolve.Should().NotThrow();
        }
    }
}
