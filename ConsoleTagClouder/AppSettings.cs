using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using CommandLine;
using TagCloud;
using Extensions;

namespace ConsoleTagClouder
{
    public class AppSettings
    {
        private (int x, int y) size = (-1, -1);

        [Value(0)]
        public string SourcePath { get; set; }
        
        [Value(1)]
        public string TargetPath { get; set; }

        [Option('s', Separator = ';')]
        private (int x, int y) Size
        {
            get => size;
            set
            {
                if (value.x < 0 || value.y < 0)
                    throw new ArgumentException("Size must be positive");
                size = value;
            }
        }

        [Option('t')]
        private string FontType { get; set; }
        [Option('b')]
        private string FontBrush { get; set; }
        [Option('g')]
        private string BackgroundBrush { get; set; }
                
        [Option('l')]
        private string TLayouter { get; set;  }
        [Option('c')]
        private string TCounter { get; set;  }
        [Option('m')]
        private string TScaler { get; set;  }

        public CloudSettings BuildCloudSettings() =>
            CloudSettings.BuildValid(TLayouter, TCounter, TScaler);
        
        public DrawingSettings BuildDrawingSettings()=>
            DrawingSettings.BuildValid(Size,FontType,FontBrush,BackgroundBrush);
    }
}