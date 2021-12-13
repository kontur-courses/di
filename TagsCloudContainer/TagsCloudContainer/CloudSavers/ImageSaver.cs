using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class ImageSaver : IImageSaver
    {
        private string pathToSaveDirectory;
        private string fileFormat;
        public ImageSaver(string pathToSaveDirectory, string fileFormat = "png")
        {
            this.pathToSaveDirectory = pathToSaveDirectory;
            this.fileFormat = fileFormat;
        }

        public string Save(Bitmap image, string name)
        {
            var path = @$"{pathToSaveDirectory}\{name}.{fileFormat}";
            image.Save(path);
            return Path.GetFullPath(path);
        }
    }
}
