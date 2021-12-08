using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter;
using TagsCloudVisualizationDI.Layouter.Normalizer;
using TagsCloudVisualizationDI.Settings;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.TextAnalization.VisualizatorMaker;

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

            var imagePath = "C:/GitHub/di/TagsCloudVisualizationDI/img_words.jpeg";


            //var arguments = reader.Arguments;
            //var mystemPath = reader.MystemPath;
            //var filePath = reader.FilePath;
            //var savePath = reader.SavePath;
            //var readingEncoding = reader.ReadingEncoding;
            //var analyzer = buildContainer.Resolve<IAnalyzer>();
            //var filler = buildContainer.Resolve<IContentFiller>();
            //var rectangleSize = new Size(100, 100);
            //var normalizer = buildContainer.Resolve<IWordNormalizer>();

            reader.InvokeProcess();

            

            
                //КЛИЕНТЫ




            

            
            var wordsFromFile = reader.ReadText(reader.SavePath, reader.ReadingEncoding);


            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            //Здесь анализ на часть слова и соответствие строки слову
            //ИСКЛЮЧИ СКУЧНЫЕ СЛОВА


            
            var normalizer = settings.Normalization;

            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();
            //Здесь расширение функционала нормализации









            
            
            var filler = settings.Filler;
            var rectangleSize = settings.ElementSize;
            









            //НЕСКОЛЬКО АЛГОРИТМОВ
            filler.FillInElements(rectangleSize, normalyzedWords);

            var elementsForVisualisation = filler.GetElementsList();



            //ниже добавить параметризацию цветов и другихз настроек
            using (var visualization = new Visualization(elementsForVisualisation, new Pen(Color.White, 10),
                new SolidBrush(Color.White), new Font("Times", 15)))
            {
                visualization.DrawAndSaveImage(new Size(5000, 5000), imagePath, ImageFormat.Jpeg);
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
            RegistrationOfVisualizator(buildContainer);
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

        private static void RegistrationOfVisualizator(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<DefaultVisualizatorMaker>().As<IVisualizatorMaker>();
        }

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
