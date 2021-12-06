using System.Collections.Generic;
using System.Diagnostics;

namespace TagCloud.Extensions
{
    public static class ProcessExtensions
    {
        public static IEnumerable<string> ReadStandardOutput(this Process process)
        {
            while (!process.StandardOutput.EndOfStream)
                yield return process.StandardOutput.ReadLine();
        }
    }
}