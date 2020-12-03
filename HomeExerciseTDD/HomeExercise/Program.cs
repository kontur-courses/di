using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace HomeExerciseTDD
{
    class Program
    {
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.Func`2[System.Drawing.Rectangle,System.Boolean]")]
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: Enumerator[System.Drawing.Rectangle]")]
        static void Main(string[] args)
        {
            var wordPath = @"C:\Users\Enot\Desktop\spora\di\HomeExerciseTDD\HomeExercise\1oneWord.txt";
            var boringPath = @"C:\Users\Enot\Desktop\spora\di\HomeExerciseTDD\HomeExercise\boringWord.txt";
            var console = new ConsoleCloudClient();
            //args = new[] {"options", "--words", wordPath, "--boring", boringPath, "--format", "jpeg", "--color", "23", "-c", "10"};
            args = new[] {"options", "--words", wordPath};
            var builder = new ContainerBuilder();
            
            console.ConsoleHandler(args, builder);
            
            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<Spiral>().As<ISpiral>();
            builder.RegisterType<Word>().As<IWord>();
            builder.RegisterType<WordCloud>().As<IWordCloud>();
            builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.RegisterType<WordCloudPainter>().As<IPainter<Word>>();
            var container = builder.Build();
            var painter = container.Resolve<IPainter<Word>>();

            painter.DrawFigures();
        }
    }
}