using System;
using System.Diagnostics;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Factories;

namespace TagCloudTests.CloudLayouter.FigurePaths
{
    [TestFixture]
    class Spiral_Should
    {
        private SpiralFactory spiralFactory;

        [OneTimeSetUp]
        public void SetUp()
        {
            spiralFactory = TagCloud.Program.InitializeContainer().Get<SpiralFactory>();
        }

        [Test]
        public void SpiralFactory_ReturnDifferentObjects()
        {
            var spiral1 = spiralFactory.GetFigurePath();
            var spiral2 = spiralFactory.GetFigurePath();

            spiral2.Should().NotBeSameAs(spiral1);
        }

        [TestCase(2, TestName = "Little Distance")]
        [TestCase(10, TestName = "Common Distance")]
        public void Spiral_DifferentTurnsHaveNeedingDistance(int distanceBetweenTurns)
        {
            var tempDistance = spiralFactory.DistanceBetweenTurns;
            var tempAngle = spiralFactory.DeltaAngle;
            spiralFactory.DistanceBetweenTurns = distanceBetweenTurns;
            spiralFactory.DeltaAngle = 360;
            var spiral = spiralFactory.GetFigurePath();
            var point1 = spiral.GetNextPoint();
            var point2 = spiral.GetNextPoint();

            GetDistanceBetweenPoints(point1, point2).Should().BeApproximately(distanceBetweenTurns, 1);
            spiralFactory.DistanceBetweenTurns = tempDistance;
            spiralFactory.DeltaAngle = tempAngle;
        }

        private double GetDistanceBetweenPoints(Point point1, Point point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) +
                             (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        [Test]
        public void SomeTest()
        {
            var p = new Process();
            var procInfo = new ProcessStartInfo("cmd.exe", "/C mystem -n -l -e cp866");
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            procInfo.RedirectStandardInput = true;
            procInfo.UseShellExecute = false;
            p.StartInfo = procInfo;
            p.Start();
            p.StandardInput.WriteLine("Привет мой друг родной");
            p.StandardInput.Close();
            var result = p.StandardOutput.ReadToEnd();
            var error = p.StandardError.ReadToEnd();
        }
    }
}
