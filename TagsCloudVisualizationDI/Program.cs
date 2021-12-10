using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualizationDI.Settings;
using TagsCloudVisualizationDI.TextAnalization.Normalizer;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

namespace TagsCloudVisualizationDI
{
    public class Program
    {
        public static void Main(string pathToFile, string pathToSave)
        {
            var containerBuilder = new ContainerBuilder();
            RegistrationOfSettings(containerBuilder, pathToFile, pathToSave);
            var buildContainer = containerBuilder.Build();

            var settings = buildContainer.Resolve<ISettingsConfiguration>();
            var visualization = settings.Visualizator;
            var reader = settings.FileReader;
            var analyzer = settings.Analyzer;
            var normalizer = settings.Normalization;
            var filler = settings.Filler;
            var pictureSavePath = settings.SavePath;
            var elementSize = settings.ElementSize;

            reader.InvokeProcess();

            var wordsFromFile = reader.ReadText(reader.SaveAnalizationPath, reader.ReadingEncoding);
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();

            filler.FillInElements(elementSize, normalyzedWords);

            var elementsForVisualisation = filler.GetElementsList();
            using (var drawer = visualization.Invoke(elementsForVisualisation, pictureSavePath))
            {
                drawer.DrawAndSaveImage();
            }
        }

        private static IEnumerable<Word> NormalyzeWords(IEnumerable<Word> analyzedWords, IWordNormalizer normalizer)
        {
            foreach (var word in analyzedWords)
                yield return normalizer.NormalizeWord(word);
        }

        private static void RegistrationOfSettings(ContainerBuilder buildContainer, string pathToFile, string pathToSave)
        {
            buildContainer.RegisterType<DeffaultSettingsConfiguration>().As<ISettingsConfiguration>()
                .WithParameter("pathToFile", pathToFile)
                .WithParameter("pathToSave", pathToSave);
            //.WithParameter("format", format)
            //.WithParameter("encoding", encoding);
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
        


        /*
        private static void RegistrationOfFiller(ContainerBuilder buildContainer, Func<List<RectangleWithWord>, string, IVisualization> visualization)
        {

            buildContainer.RegisterType<CircularCloudLayouterForRectanglesWithText>().As<IContentFiller>()
                .WithParameter("center", new Point(2500, 2500))
                .WithParameter("visualization", visualization);
        }
        */

        /*
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
        }*/
    }
}
