using System;
using System.Collections.Generic;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.Visualization;
using TagCloud.WordsPreparation;
using TagCloud.WordsProcessing;
using Autofac;
using TagCloud.Algorithm;

namespace TagCloud
{
    public class Program
    {
        private static IContainer GetContainer(string inputFilename, string myStemPath, HashSet<WordClass> excludedWordClasses)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new PictureConfig()).As<PictureConfig>();

            builder.RegisterType<WordsProvider>().As<IWordsProvider>();
            builder.RegisterInstance(new TxtTextReader(inputFilename)).As<ITextReader>();
            builder.RegisterInstance(new MyStemBasedWordClassIdentifier(myStemPath)).As<IWordClassIdentifier>();
            builder
                .Register(c => 
                    new WordClassBasedSelector(c.Resolve<IWordClassIdentifier>(), excludedWordClasses))
                .As<IWordSelector>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<WordSizeSetter>().As<IWordSizeSetter>();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>();


            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();

            builder.RegisterType<IndexBasedWordPainter>().As<IWordPainter>();

            builder.RegisterType<TagCloudGenerator>().As<ITagCloudGenerator>();


            return builder.Build();
        }


        public static void Main()
        {
            var rnd = new Random();

            var filename = $"D:/Desktop/{rnd.Next()}.png";
            var inputFilename = "D:/Desktop/bigTestFile.txt";
            var myStemPath = "D:/Desktop/mystem.exe";
            var excludedWordClasses = new HashSet<WordClass>
                {WordClass.Conjunction, WordClass.Preposition, WordClass.Particle, WordClass.Pronoun};
            var container = GetContainer(inputFilename, myStemPath, excludedWordClasses);


            using (var scope = container.BeginLifetimeScope())
            {
                var tagCloudGenerator = scope.Resolve<ITagCloudGenerator>();
                var bitmap = tagCloudGenerator.GetTagCloudBitmap();
                bitmap.Save(filename);
            }
        }
    }
}
