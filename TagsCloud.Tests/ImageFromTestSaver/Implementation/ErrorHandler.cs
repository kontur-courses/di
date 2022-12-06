using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.Tests.ImageFromTestSaver.Implementation
{
    public class ErrorHandler : IImageFromTestSaver
    {
        private const string DateTimeFormat = "MM-dd-yy hh-mm-ss";
        private const string DirectoryName = "Error_Test_Images";

        public bool TrySaveImageToFile(string testName, Image image, out string path)
        {
            Directory.SetCurrentDirectory("../../../");
            if (!Directory.Exists(DirectoryName))
                Directory.CreateDirectory(DirectoryName);

            var fileName = DateTime.Now.ToString(DateTimeFormat) + $" {testName}.png";
            path = Path.Combine(DirectoryName, fileName);
            
            try
            {
#pragma warning disable CA1416
                image.Save(path, ImageFormat.Png);
#pragma warning restore CA1416
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}