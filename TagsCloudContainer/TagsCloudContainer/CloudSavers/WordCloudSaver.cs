using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class WordCloudSaver 
    {
        private IWordCloudPainter cloudPainter;
        private ImageSaver saver;

        public WordCloudSaver(IWordCloudPainter wordCloudPainter, ImageSaver saver)
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
