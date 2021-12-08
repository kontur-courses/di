using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer
{
    public interface ITagCloudCreator
    {
        public Bitmap CreateFromTextFile(string sourcePath);

        public void CreateFromTextFileAndSave(string sourcePath, string fileName, string destinationPath = null, ImageFormat format = null);
    }
}