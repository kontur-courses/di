using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagCloudSaver
    {
        private readonly Dictionary<string, IBitmapSaver> saversByExtension;

        public TagCloudSaver(IEnumerable<IBitmapSaver> bitmapSavers)
        {
            saversByExtension = bitmapSavers
                .SelectMany(bs => bs.SupportedExtensions.Select(ext => (ext, bs)))
                .ToDictionary();
        }

        public void Save(Color bgColor, List<WordLayout> wordLayouts, string path)
        {
            var saver = FindBitmapSaver(Path.GetExtension(path));
            var bitmap = TagCloudDrawer.DrawTagCloud(wordLayouts, bgColor);
            saver.Save(bitmap, path);
        }

        private IBitmapSaver FindBitmapSaver(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (!saversByExtension.TryGetValue(extension, out var findBitmapSaver))
            {
                throw new ArgumentException($"Unsupported extension: \"{extension}\"");
            }

            return findBitmapSaver;
        }
    }
}
