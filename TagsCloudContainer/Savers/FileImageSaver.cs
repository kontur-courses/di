using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Savers
{
    public class FileImageSaver
    {
        private readonly IDictionary<string, IImageSaver> savers;

        public FileImageSaver(IEnumerable<IImageSaver> savers)
        {
            this.savers = new Dictionary<string, IImageSaver>();
            foreach (var saver in savers)
            foreach (var extension in saver.Extensions)
                this.savers[extension] = saver;
        }

        public void Save(string path, Image image)
        {
            var extension = PathUtils.GetExtension(path);
            savers[extension].Save(path, extension, image);
        }
    }
}