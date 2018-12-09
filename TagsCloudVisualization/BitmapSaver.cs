using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class BitmapSaver : ISaver
    {
        private readonly Bitmap bitmapFile;

        public BitmapSaver(Bitmap bitmapFile)
        {
            this.bitmapFile = bitmapFile;
        }

        public void Save(string filename)
        {
            bitmapFile.Save(filename);
        }
    }
}
