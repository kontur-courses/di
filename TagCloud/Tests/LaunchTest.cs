using System.Diagnostics;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class LaunchTest
    {
        [Test]
        public void CheckLaunchingOutput()
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "TagCloud.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Synchronously read the standard output of the spawned process.
                StreamReader reader = process.StandardOutput;
                string output = reader.ReadToEnd();

                output.Should()
                    .Contain("Result saved to:\n")
                    .And.Contain(".png");

                process.WaitForExit();
            }
            var result = Program.Main(new string[0]);
            result.Should().Be(0);
        }
    }
}