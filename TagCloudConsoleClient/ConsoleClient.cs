using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Default;

namespace TagCloudConsoleClient
{
    public class ConsoleClient
    {
        private readonly TextWriter console;

        public ConsoleClient(TextWriter console)
        {
            this.console = console;
        }

        public void Start(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Start);
        }

        private void Start(Options options)
        {
            var factory = new TagCloudFactory();
            try
            {
                var tagCloud = factory.CreateInstance(options.Manhattan, options.Order);
                tagCloud.CreateTagCloudFromFile(options.SourcePath, options.ResultPath, options.Font,
                    options.BackGround, options.MaxCount, options.Width, options.Height);
            }
            catch (Exception e)
            {
                console.WriteLine("Error: " + e.Message);
                return;
            }
            console.WriteLine("Success!");
            if (options.OpenResult)
            {
                console.WriteLine("Opening result");
                Process.Start(new ProcessStartInfo(options.ResultPath)
                    { UseShellExecute = true });
            }
        }
    }
}