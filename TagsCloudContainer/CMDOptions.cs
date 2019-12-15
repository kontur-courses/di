using System;
using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public sealed class CMDOptions
    {
        [Option('i', "input",
            Required = true,
            HelpText = "Input file with data with words.")]
        public string InputFile { get; set; }

        [Option('o', "output",
            Required = true,
            HelpText = "Output file name or path")]
        public string OutputFile { get; set; }

        [Option('w', "width",
            Required = true,
            HelpText = "Input width size of bitmap/cloud.")]
        public int Width { get; set; }

        [Option('h', "height",
            Required = true,
            HelpText = "Input height size of bitmap/cloud.")]
        public int Height { get; set; }

        [Option('c', "color",
            Required = false,
            Default = "Black",
            HelpText = "Color of text and border")]
        public string Color { get; set; }

        [Option('f', "font",
            Required = false,
            Default = "Tahoma",
            HelpText = "Font name")]
        public string Font { get; set; }

        [Option('m', "compression",
            Required = false,
            Default = true,
            HelpText = "Flag to enable/unable compression")]
        public bool Compression { get; set; }

        [Option('r', "format",
            Required = false,
            Default = "PNG",
            HelpText =
                "Format of output file, should be one of: MemoryBmp, Bmp, Emf, Wmf, Gif, Jpeg, Png, Tiff, Exif, Icon")]
        public string Format { get; set; }

        [Option('e', "excluded",
            Required = false,
            Default = new[] {"PR", "PART", "CONJ"},
            HelpText = "Excluded parts of speech. Should be one of: A, ADV, ADVPRO, ANUM, APRO, COM, CONJ, INTJ, " +
                       "NUM, PART, PR, S, SPRO, V")]
        public IEnumerable<string> Excluded { get; set; }
    }
}