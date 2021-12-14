using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class WordCloudSaver : IWordCloudSaver
    {
        private IWordCloudPainter cloudPainter;

        public WordCloudSaver(IWordCloudPainter wordCloudPainter)
        {
            cloudPainter = wordCloudPainter;
        }

        public string SaveCloud(string pathToSaveDir, string name, ImageSettings imageSettings, ImageFormats imageFormat)
        {
            var image = cloudPainter.PaintWords(imageSettings);
            var path = @$"{pathToSaveDir}\{name}.{imageFormat}";
            image.Save(path);

            return Path.GetFullPath(path);
        }
    }
}
