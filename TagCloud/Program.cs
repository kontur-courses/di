using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.PointGenerator;
using IContainer = Autofac.IContainer;

namespace TagsCloudVisualization
{
    public class Program
    {
        private static IContainer container;

        public static void Main()
        {
            try
            {
                CompositionRootInitialize();
                using (container.BeginLifetimeScope())
                {
                    DrawRectangles("../../../ExamplesIMG/rectangles.png");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            /*Draw(Examples.GenerateFirstExample("Arial"), "../../../ExamplesIMG/1.png");
            Draw(Examples.GenerateSecondExample("Arial"), "../../../ExamplesIMG/2.png");
            Draw(Examples.GenerateThirdExample("Arial"), "../../../ExamplesIMG/3.png");*/
        }

        private static void DrawRectangles(string path)
        {
            //var pointGenerator = new Spiral(0.1f, 0.9, new PointF());
            var cloudLayouter = container.Resolve<CloudLayouter.CloudLayouter>();
            Examples.RandomFill(400, cloudLayouter, new Size(5, 5), new Size(50, 50));
            container.Resolve<Visualizer>().DrawRectangles(path);
        }

        private static void Draw(List<(string, Font)> example, string filename)
        {
            var template = container.Resolve<WordsHandler>().Handle(example);
            var visualizer = new Visualizer(template.Size + new Size(100, 100), Color.Bisque);
            visualizer.Draw(template, filename);
        }

        private static void CompositionRootInitialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Cache>().As<ICache>();
            builder.Register(c => new Spiral(0.1f, 0.8, new PointF(0, 0), c.Resolve<ICache>())).As<IPointGenerator>();
            builder.Register(c => new Visualizer(c.Resolve<ICloudLayouter>()));
            builder.RegisterType<CloudLayouter.CloudLayouter>().AsSelf().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<WordsHandler>().AsSelf();
            container = builder.Build();
        }
    }
}