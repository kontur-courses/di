using Autofac;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter;
using TagsCloudVisualizationDI.Layouter.Normalizer;
using TagsCloudVisualizationDI.Settings;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.Visualization;

namespace TagsCloudVisualizationDI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            

            RegistrationOfSettings(containerBuilder);
            var buildContainer = containerBuilder.Build();
            var settings = buildContainer.Resolve<ISettingsConfiguration>();


            var visualization = settings.Visualizator;
            var reader = settings.FileReader;
            var analyzer = settings.Analyzer;
            var normalizer = settings.Normalization;
            var filler = settings.Filler;
            var savePath = settings.SavePath;
            var elementSize = settings.ElementSize;


            reader.InvokeProcess();


            //var settings = buildContainer.Resolve<ISettingsConfiguration>();

            //var visualization = settings.Visualizator;


            //InitializeRegistration(containerBuilder, visualization);





            //var rectangleSize = new Size(100, 100);

            //var filler = buildContainer.Resolve<IContentFiller>();
            //var reader = buildContainer.Resolve<ITextFileReader>();
            //var analyzer = buildContainer.Resolve<IAnalyzer>();
            //var normalizer = buildContainer.Resolve<IWordNormalizer>();






            //КЛИЕНТЫ


            var wordsFromFile = reader.ReadText(reader.SavePath, reader.ReadingEncoding);
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();
            //Здесь расширение функционала нормализации


            filler.FillInElements(elementSize, normalyzedWords);


            var elementsForVisualisation = filler.GetElementsList();
            using (var drawer = visualization.Invoke(elementsForVisualisation, savePath))
            {
                drawer.DrawAndSaveImage();
            }
        }

        private static IEnumerable<Word> NormalyzeWords(IEnumerable<Word> analyzedWords, IWordNormalizer normalizer)
        {
            foreach (var word in analyzedWords)
                yield return normalizer.NormalizeWord(word);
        }

        
        private static void InitializeRegistration(ContainerBuilder builder, Func<List<RectangleWithWord>, string, IVisualization> visualization)
        {
            /*
            RegistrationOfTextFileReader(builder);
            RegistrationOfTextAnalyzer(builder);
            RegistrationOfNormalizer(builder);
            RegistrationOfFiller(builder, visualization);
            */
        }
        

        private static void RegistrationOfFiller(ContainerBuilder buildContainer, Func<List<RectangleWithWord>, string, IVisualization> visualization)
        {

            buildContainer.RegisterType<CircularCloudLayouterForRectanglesWithText>().As<IContentFiller>()
                .WithParameter("center", new Point(2500, 2500))
                .WithParameter("visualization", visualization);
        }

        private static void RegistrationOfSettings(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DeffaultSettingsConfiguration>().As<ISettingsConfiguration>();
        }


        
        private static void RegistrationOfNormalizer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<WordNormalizerOrigin>().As<IWordNormalizer>();
        }


        private static void RegistrationOfTextAnalyzer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DefaultAnalyzer>().As<IAnalyzer>()
                .WithParameter("speechParts",
                    Enum.GetValues(typeof(PartsOfSpeech.SpeechPart)).Cast<PartsOfSpeech.SpeechPart>());
        }

        private static void RegistrationOfTextFileReader(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DefaultTextFileReader>().As<ITextFileReader>();
        }
    }
}
