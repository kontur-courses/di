using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;
using Autofac;
using Extensions;

namespace TagCloud
{
    public class CloudMaker
    {
        
        private readonly IWordsCounter counter;
        private readonly IWeightScaler scaler;
        private readonly CloudDrawer drawer;

        public CloudMaker(IWordsCounter counter, IWeightScaler scaler, CloudDrawer drawer)
        {
            this.counter = counter;
            this.scaler = scaler;
            this.drawer = drawer;
        }

        public Result<None> UpdateWith(string text) =>
            Result.OfAction(() => counter.UpdateWith(text))
                .RefineError("Counter throw error: ");

        public Result<None> UpdateFrom(Stream stream)
        {   //TODO should make bufferization 
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return UpdateWith(reader.ReadToEnd());
        }

        public Result<Bitmap> DrawCloud()=>
            Result.Of(() => counter.CountedWords.Select(x => (x.Key, x.Value)))
                .Then(p => counter.CountedWords.Select(x => x.Key).Zip(scaler.Scale(p), ValueTuple.Create))
                .RefineError("Counter throw error: ")
                .Then(drawer.Draw);
        
//            var pairs = counter.CountedWords.Select(x => (x.Key, x.Value));
//            pairs = counter.CountedWords.Select(x => x.Key).Zip(scaler.Scale(pairs), ValueTuple.Create);
//            return drawer.Draw(pairs);
        
    }
}