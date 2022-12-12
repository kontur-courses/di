﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.ConsoleApp
{
    internal class Options
    {
        [Option('f', "inputFilename", Required = true, HelpText = "Set input file with words")]
        public string InputWordFilename { get; set; }

        [Option('o', "outputFilename", Required = true, HelpText = "Set image output file")]
        public string OutputImageFilename { get; set; }

        [Option('w', "outputImageWidth", Required = false, Default = 500, HelpText = "Set output image width")]
        public int OutputImageWidth { get; set; }

        [Option('h', "outputImageHeight", Required = false, Default = 500, HelpText = "Set output image height")]
        public int OutputImageHeight { get; set; }

        [Option("font", Required = false, Default = "Consolas", HelpText = "Set word font family")]
        public string FontFamily { get; set; }

        [Option("minColor", Required = false, Default = "#FFFFAA00", HelpText = "Set minimum frequency word plate color (ARGB with '#')")]
        public string MinFrequencyColorString { get; set; }

        [Option("maxColor", Required = false, Default = "#FFFF0000", HelpText = "Set maximum frequency word plate color (ARGB with '#')")]
        public string MaxFrequencyColorString { get; set; }

        [Option("minFontSize", Required = false, Default = 14F, HelpText = "Set minimum frequency word font size")]
        public float MinFrequncyFontSize { get; set; }

        [Option("maxFontSize", Required = false, Default = 24F, HelpText = "Set minimum frequency word font size")]
        public float MaxFrequncyFontSize { get; set; }
    }
}