using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using CommandLine;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class Options
    {
        [Value(0, MetaName = "filename", HelpText = "Text file", Required = true)]
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> preparedWords = new Dictionary<string, int>();

            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                preparedWords = WordsPreprocessor.Prepare(options.Text);
            });

            var visualizer = new CloudVisualizer(preparedWords);
            visualizer.VisualizeCloud();
        }
    }
}
