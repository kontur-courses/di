using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Autofac;
using DeepMorphy;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.PointGenerator;
using WeCantSpell.Hunspell;
using IContainer = Autofac.IContainer;

namespace TagsCloudVisualization
{
    public class Program
    {
        private static IContainer container;

        public static void Main()
        {
            // var morph = new MorphAnalyzer();
            // var results = morph.Parse(new string[]
            // {
            //     "королёвские",
            //     "тысячу",
            //     "миллионных",
            //     "красотка",
            //     "1-ый"
            // }).ToArray();
            // var morphInfo = results[0];
            
            
            /*try
            {
                CompositionRootInitialize();
                using (container.BeginLifetimeScope())
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }*/
        }
        

        private static void CompositionRootInitialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Cache>().As<ICache>();
            builder.Register(c => new Spiral(0.1f, 0.5, new PointF(0, 0), c.Resolve<ICache>())).As<IPointGenerator>();
            builder.Register(c => new Visualizer(c.Resolve<ICloudLayouter>()));
            builder.RegisterType<CloudLayouter.CloudLayouter>().AsSelf().As<ICloudLayouter>();
            builder.RegisterType<WordsHandler>().AsSelf();
            container = builder.Build();
        }
    }
}