using System;
using System.Drawing;

namespace TagsCloudContainerCore.Console;

[Serializable]
public class LayoutSettings
{
    public string FontName { get; set; }
    public int MaxFontSize { get; set; }
    public float MinAngle { get; set; }
    public int Step { get; set; }
    public Size PictureSize { get; set; }
    public string BackgroundColor { get; set; }
    public string FontColor { get; set; }

    public string PathToExcludedWords { get; set; }

    public override string ToString()
    {
        return $"Font name: {FontName}\n" +
               $"Max font size: {MaxFontSize}\n" +
               $"Font Color: {FontColor}\n" +
               $"Background Color: {BackgroundColor}\n" +
               $"Picture Size: {PictureSize}\n\n" +
               "Algorithm settings:\n" +
               $"\tAlgorithm step length: {Step}\n" +
               $"\tAlgorithm min angle: {MinAngle}\n\n" +
               $"Path to exclude words file: {PathToExcludedWords}\n";
    }
}