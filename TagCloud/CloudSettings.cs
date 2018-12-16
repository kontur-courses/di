using System;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;
using TagsCloudVisualization;

namespace TagCloud
{ 
    public class CloudSettings
    {
        //public DrawingSettings DrawingSettings{ get; set; }
        public string TLayouter { get; set; }
        public string TCounter { get; set; }
        public string TScaler { get; set; }
        
        public CloudSettings(Type tLayouter = null, Type tCounter= null ){}

        public CloudSettings(string tLayouter, string tCounter,string tScaler)
        {
            TCounter = tCounter;
            TLayouter = tLayouter;
            TScaler = tScaler;
        }

        public static CloudSettings Default() =>
            new CloudSettings( 
                typeof(RowwiseCloudLayouter).Name, 
                typeof(SimpleWordsCounter).Name,
                typeof(LinearWeightScaler).Name);
    }
}