using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public interface IImageSettingsProvider
    {
        Size ImageSize { get; set; }
        
    }
}