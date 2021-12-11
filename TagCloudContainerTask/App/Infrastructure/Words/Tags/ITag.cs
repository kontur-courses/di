using System.Drawing;

namespace App.Infrastructure.Words.Tags
{
    public interface ITag
    {
        string Word { get; }
        float WordEmSize { get; }
        Rectangle WordOuterRectangle { get; set; }
    }
}