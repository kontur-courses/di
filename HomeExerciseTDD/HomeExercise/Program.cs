using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace HomeExerciseTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            //var wordPath = "Word.txt";
            //var boringPath = "boringWord.txt";
            var console = new ConsoleCloudClient();
            //args = new[] {"options", "--words", wordPath, "--boring", boringPath, "--format", "jpeg", "--color", "23", "-c", "10"};
            //args = new[] {"options", "--words", wordPath};
            var builder = new ContainerBuilder();
            
            console.HandleSettingsFromConsole(args, builder);
            
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