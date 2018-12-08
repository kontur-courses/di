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

namespace TagCloud
{
    public class Clouder
    {
        public static Clouder Create(AppSettings settings)
        {
            var container = new ContainerBuilder();
            container.RegisterType<DrawingSettings>().AsSelf();
            container.RegisterType<IWordsCounter>().As(settings.TCounter);
            container.RegisterType<ICloudLayouter>().As(settings.TLayouter);            
            container.RegisterType<CloudDrawer>().AsSelf();
            container.RegisterType<Clouder>().AsSelf();
            return (Clouder) container.Build().Resolve(typeof(Clouder));
        }

        public static Clouder CreateDefault() =>
            Clouder.Create(AppSettings.Default());
        
        private readonly IWordsCounter counter;
        private readonly IWeightScaler scaler;
        private readonly CloudDrawer drawer;


        public Clouder(IWordsCounter counter, IWeightScaler scaler, CloudDrawer drawer)
        {
            this.counter = counter;
            this.scaler = scaler;
            this.drawer = drawer;
        }
        
        public void UpdateWith(string text)=>
            counter.UpdateWith(text);

        public void UpdateFrom(Stream stream)
        {    //TODO make bufferization 
            using (var reader = new StreamReader(stream,Encoding.UTF8))
                UpdateWith(reader.ReadToEnd());
        }

        public Bitmap DrawCloud()
        {
            var pairs = counter.CountedWords.Select(x => (x.Key, x.Value));
            pairs = counter.CountedWords.Select(x => x.Key).Zip(scaler.Scale(pairs), ValueTuple.Create);
            return drawer.Draw(pairs);
        }
    }
}