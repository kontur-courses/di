using System;
using System.Diagnostics;
using System.IO;

namespace TagsCloudVisualisationTests.Infrastructure
{
    public class Measurement : IDisposable
    {
        private readonly string name;
        private readonly TextWriter writer;
        public readonly Stopwatch Stopwatch;

        public Measurement(string name, TextWriter writer)
        {
            this.name = name;
            this.writer = writer;
            Stopwatch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            Stopwatch.Stop();
            writer.WriteLine($"{name} finished, took {Stopwatch.Elapsed}");
        }

        public void Dispose()
        {
            Stop();
        }
    }
}