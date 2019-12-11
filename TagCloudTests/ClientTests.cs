using System;
using System.Diagnostics;
using System.Threading;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.IServices;

namespace TagCloudTests
{
    [TestFixture]
    public class ClientTests
    {
        [SetUp]
        public void SetUp()
        {
            container = TagCloud.Program.GetContainer();
        }

        private WindsorContainer container;

        [Test]
        public void IClient_Should_NotThrowsARuntimeExceptions_WhenStarts()
        {
            var visualization = container.Resolve<ICloudVisualization>();
            var client = container.Resolve<IClient>();
            var watch = Stopwatch.StartNew();
            Action action = () =>
            {
                var thread = new Thread(() => client.Start(visualization));
                thread.Start();
                while (watch.ElapsedMilliseconds < 5000)
                {
                }

                thread.Abort();
            };
            action.Should().NotThrow();
        }
    }
}