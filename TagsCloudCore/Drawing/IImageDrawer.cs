using System.Drawing;
using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.Drawing;

public interface IImageDrawer
{
    public Bitmap DrawImage(WordColorerAlgorithm colorerAlgorithm);
}