using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class WordCloudSaver : IWordCloudSaver
    {
        private IWordCloudPainter cloudPainter;
        private IImageSaver saver;

        public WordCloudSaver(IWordCloudPainter wordCloudPainter, IImageSaver saver)
        {
            cloudPainter = wordCloudPainter;
            this.saver = saver;
        }

        public string SaveCloud(string name, ImageSettings imageSettings)
        {
            var image = cloudPainter.PaintWords(imageSettings);
            var path = saver.Save(image, name);
            return path;
        }
    }
}
