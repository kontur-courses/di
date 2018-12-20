using System;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;
using TagsCloudVisualization;

namespace TagCloud
{ 
    public class CloudSettings
    {
        public string TLayouter { get; }
        public string TCounter { get; }
        public string TScaler { get; }

        public CloudSettings WithLayouter(string tLayouter)=>
        new CloudSettings(tLayouter,TCounter,TScaler);
        
        public CloudSettings WithCounter(string tCounter)=>
            new CloudSettings(TLayouter,tCounter,TScaler);
        
        public CloudSettings WithScaler(string tScaler)=>
            new CloudSettings(TLayouter,TCounter,tScaler);
        
        private CloudSettings(string tLayouter, string tCounter,string tScaler)
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
        
        public static CloudSettings BuildValid(string tLayouter, string tCounter,string tScaler) =>
            new CloudSettings( 
                tLayouter ?? typeof(RowwiseCloudLayouter).Name, 
                tCounter ?? typeof(SimpleWordsCounter).Name,
                tScaler ?? typeof(LinearWeightScaler).Name);
    }
}