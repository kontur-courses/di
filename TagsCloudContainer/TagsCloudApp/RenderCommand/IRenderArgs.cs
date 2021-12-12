using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp.RenderCommand
{
    public interface IRenderArgs
    {
        string InputPath { get; }
        string OutputPath { get; }
        FontFamily FontFamily { get; }
        int MaxFontSize { get; }
        int MinFontSize { get; }
        Size? ImageSize { get; }
        float ImageScale { get; }
        string BackgroundColor { get; }
        string DefaultColor { get; }
        string ColorMapperType { get; }
        string SpeechPartColorMap { get; }
        string ImageFormat { get; }
        string WordsScale { get; }
        IEnumerable<string> IgnoredSpeechParts { get; }
    }
}