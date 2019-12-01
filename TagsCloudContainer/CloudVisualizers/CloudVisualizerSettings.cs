using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace TagsCloudContainer.CloudVisualizers
{
    public class CloudVisualizerSettings
    {
        public Palette Palette { get; set; }
        public IBitmapMaker BitmapMaker { get; set; }
        
    }
}