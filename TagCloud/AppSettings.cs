using System;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;
using TagsCloudVisualization;

namespace TagCloud
{ 
    public class AppSettings
    {
        public readonly DrawingSettings DrawingSettings;
        public Type TLayouter { get; }
        public Type TCounter { get; }

        public AppSettings(DrawingSettings drawingSettings, Type tLayouter, Type tCounter)
        {
            this.DrawingSettings = drawingSettings; //TODO make this error compilation time
            if(!tLayouter.GetInterfaces().Contains(typeof(ICloudLayouter)) ||
               !tCounter.GetInterfaces().Contains(typeof(IWordsCounter)))
                throw new ArgumentException();
            TCounter = tCounter;
        }

        public static AppSettings Default() =>
            new AppSettings(new DrawingSettings()
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