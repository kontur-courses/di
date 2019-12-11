using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Castle.Windsor;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TagCloud;
using TagCloud.IServices;

namespace TagCloudTests
{
    [TestFixture]
    public class ClientTests
    {
        private WindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            container = TagCloud.Program.GetContainer();
        }
        [Test]
        public void IClient_Should_NotThrowsARuntimeExceptions_WhenStarts()
        {
            var visualization = container.Resolve<ICloudVisualization>();
            var client = container.Resolve<IClient>();
            Stopwatch watch = Stopwatch.StartNew();
            Action action = () =>
            {
                var thread =  new Thread(() => client.Start(visualization));
                thread.Start();
                while (watch.ElapsedMilliseconds < 5000)
                { }
                thread.Abort();
            };
            action.Should().NotThrow();
        }
    }
}
