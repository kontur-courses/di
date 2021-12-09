using System;
using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface IRenderingSettings : IDisposable
    {
        Size? DesiredImageSize { get; }
        float Scale { get; }
        Brush Background { get; }
    }
}