using System;
using CommandLine;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class StandartOptions
    {
        [Value(1, HelpText = "File to take words from")]
        public string File { get; set; }
        [Option('c', "count", Required = false, Default = int.MaxValue, HelpText = "Count of words in tag cloud")]
        public int MaxCnt { get; set; }

        [Option('f', "format", Required = false, Default = "png", HelpText = "Format of tag cloud file")]
        public string Format
        {
            get { return format; }
            set
            {
                if (!availableFormats.Contains(value))
                    throw new ArgumentException("\nNot available format.\n" +
                                                "Available formats are:\n" +
                                                "   png\n" +
                                                "   bmp\n" +
                                                "   gif\n" +
                                                "   jpeg");
                format = value;
            }
        }

        private string format;
        private readonly HashSet<string> availableFormats = new HashSet<string>(
            new[] { "png", "bmp", "gif", "jpeg" });
        
    }
}