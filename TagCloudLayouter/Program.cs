using System;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagCloudLayouter
{
    class Program
    {
        static void Main(string[] args)
        {
            //args 
            var count = 20;
            var center = new Point(500, 500);
            var font = new Font("Times New Roman", 40.0f);


            var builder = new ContainerBuilder();

            builder.RegisterType<SimplePreprocessor>().As<IPreprocessor>();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.Register(ctx => new ArchimedesSpiral(center)).As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            var container = builder.Build();


            var layouter = container.Resolve<ICloudLayouter>();

            var proc = container.Resolve<IPreprocessor>();
            var text = container.Resolve<IReader>().ReadFromFile("C:\\Users\\vas21\\Desktop\\wp.txt");
            var allWords = container.Resolve<ITextParser>().GetWords(text);
            var validWords = proc.GetValidWords(allWords).Take(count);


            var vis = new TagCloudVisualization(layouter);
            vis.SaveCloudLayouter("SimpleCloud", Environment.CurrentDirectory, font, validWords);
        }
    }
}
