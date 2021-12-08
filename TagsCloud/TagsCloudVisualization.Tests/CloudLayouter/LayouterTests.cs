using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization.Tests.CloudLayouter
{
    public abstract class LayouterTests
    {
        private readonly CloudLayouterTestsLogger _logger = new();
        protected Rectangle[] _rectangles;

        [OneTimeSetUp]
        public void Initialize()
        {
            _logger.Init(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFails"));
        }

        [TearDown]
        public void TearDown()
        {
            var ctx = TestContext.CurrentContext;
            if (ctx.Result.Outcome.Status == TestStatus.Failed)
            {
                var testName = ctx.Test.Name;
                try
                {
                    _logger.Log(_rectangles, testName);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Cannot log test fail image due to exception: {e.Message}");
                }
            }
        }
    }
}