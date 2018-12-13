using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.UI
{
    public interface IUI
    {
        string InputPath { get; }
        string OutputPath { get; }
        string BlacklistPath { get; }
        Point TagsCloudCenter { get; }
        Size LetterSize { get; }
        Size ImageSize { get; }
        Color TextColor { get; }
    }
}