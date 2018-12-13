using System;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;
using TagsCloudVisualization;

namespace TagCloud
{ 
    public class ClouderSettings
    {
        public DrawingSettings DrawingSettings{ get; set; }
        public Type TLayouter { get; set; }
        public Type TCounter { get; set; }
        
        public ClouderSettings(Type tLayouter = null, Type tCounter= null ){}

        public ClouderSettings(DrawingSettings drawingSettings, Type tLayouter, Type tCounter)
        {
            this.DrawingSettings = drawingSettings; //TODO make this error compilation time
            if(!tLayouter.GetInterfaces().Contains(typeof(ICloudLayouter)) ||
               !tCounter.GetInterfaces().Contains(typeof(IWordsCounter)))
                throw new ArgumentException();
            TCounter = tCounter;
        }

        public static ClouderSettings Default() =>
            new ClouderSettings(new DrawingSettings()
            {
                Size = new Size(3000, 3000),
                FontType = "NewTimesRoman",
                FontBrush = Brushes.Black,
                BackgroundBrush = Brushes.White
            }, 
                typeof(RowiseCloudLayouter), 
                typeof(SimpleWordsCounter));
    }
}