using System.Drawing;

namespace TagsCloudContainer;

public interface IGraphicsProvider
{
    Graphics Create();
    Graphics Get();
    void Commit();
}