﻿namespace TagCloudPainter.Common;

public class ParseSettings
{
    public HashSet<string> IgnoredMorphemes { get; set; } = new() { "CONJ", "PART", "PR", "SPRO", "APRO" };
    public HashSet<string> IgnoredWords { get; set; } = new HashSet<string>();
}