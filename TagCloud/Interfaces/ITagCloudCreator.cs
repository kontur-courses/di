using System.Drawing;
namespace TagCloud;

public interface ITagCloudCreator
{
    Bitmap GetCloud();
}