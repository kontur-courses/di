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

namespace TagsCloudVisualizationDI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            InitializeRegistration(containerBuilder);
            var buildContainer = containerBuilder.Build();
            var settings = buildContainer.Resolve<ISettingsConfiguration>();
            var reader = settings.FileReader;
            var analyzer = settings.Analyzer;
            var visualizator = settings.Visualizator;

            reader.InvokeProcess();


            //var arguments = reader.Arguments;
            //var mystemPath = reader.MystemPath;
            //var filePath = reader.FilePath;
            //var savePath = reader.SavePath;
            //var readingEncoding = reader.ReadingEncoding;
            //var analyzer = buildContainer.Resolve<IAnalyzer>();
            //var filler = buildContainer.Resolve<IContentFiller>();
            //var rectangleSize = new Size(100, 100);
            //var normalizer = buildContainer.Resolve<IWordNormalizer>();
            //var ImagePath = "C:/GitHub/di/TagsCloudVisualizationDI/img_words.jpeg";


            //КЛИЕНТЫ


            var wordsFromFile = reader.ReadText(reader.SavePath, reader.ReadingEncoding);
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            var normalizer = settings.Normalization;
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();
            //Здесь расширение функционала нормализации

            var filler = settings.Filler;
            var rectangleSize = settings.ElementSize;

            filler.FillInElements(rectangleSize, normalyzedWords);

            var elementsForVisualisation = filler.GetElementsList();

            using (var visualization = visualizator.Invoke(elementsForVisualisation))
            {
                visualization.DrawAndSaveImage();
            }
        }

        private static IEnumerable<Word> NormalyzeWords(IEnumerable<Word> analyzedWords, IWordNormalizer normalizer)
        {
            foreach (var word in analyzedWords)
                yield return normalizer.NormalizeWord(word);
        }

        private static void InitializeRegistration(ContainerBuilder buildContainer)
        {
            RegistrationOfTextFileReader(buildContainer);
            RegistrationOfLayouter(buildContainer);
            //RegistrationOfVisualizator(buildContainer);
            RegistrationOfTextAnalyzer(buildContainer);
            RegistrationOfNormalizer(buildContainer);


            RegistrationOsSettings(buildContainer);
        }



        private static void RegistrationOsSettings(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DeffaultSettingsConfiguration>().As<ISettingsConfiguration>();
        }



        private static void RegistrationOfNormalizer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<WordNormalizerOrigin>().As<IWordNormalizer>();
        }

        /*
        private static void RegistrationOfVisualizator(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DefaultVisualizatorMaker>().As<IVisualizatorMaker>();
        }
        */

        private static void RegistrationOfLayouter(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<CircularCloudLayouterForRectanglesWithText>().As<ICircularCloudLayouter, IContentFiller>()
                .WithParameter("center", new Point(2500, 2500));
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
