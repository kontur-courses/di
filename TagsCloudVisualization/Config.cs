using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    // public struct UintWithMinusOne
    // {
    //     private int value;
    //     
    //     public static implicit operator UintWithMinusOne(int x)
    //     {
    //         if (x < -1) throw new InvalidCastException($"UintWithMinusOne can not be less than -1, but was {x}");
    //         return new UintWithMinusOne {value = x};
    //     }
    //     public static explicit operator int(UintWithMinusOne counter)
    //     {
    //         return counter.value;
    //     }
    // }
    
    public class Config
    {
        public Size Size { get; set; }
        public bool Open { get; set; }
        public uint WordCountToStatistic { get; set; }
        public uint Density { get; set; }
        public byte MinWordToStatisticLength { get; set; }
        public string Font { get; set; }
        public string SavePath { get; set; }
        public string SaveFileName { get; set; }
        public Color? Color { get; set; }
        
        public string TextFilePath { get; set; }
        public string? CustomIgnoreFilePath { get; set; }
        
        public TagCloudResultActions TagCloudResultActions { get; set; }
        public SourceTextInterpretationMode  SourceTextInterpretationMode { get; set; }
    }
}