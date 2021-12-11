#region

using System.Drawing;

#endregion

namespace TagsCloudVisualization.Interfaces
{
    public interface ITagCloudCreator
    {
        Bitmap CreateAndSaveCloudFromTextFile(string inputPath);
    }
}