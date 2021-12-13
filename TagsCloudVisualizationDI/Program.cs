using Autofac;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualizationDI.Settings;
using TagsCloudVisualizationDI.TextAnalization.Normalizer;

namespace TagsCloudVisualizationDI
{
    public class Program
    {
        public static void Main(string pathToFile, string pathToSave, ImageFormat imageFormat, List<string> excludedWordsList)
        {

            var containerBuilder = new ContainerBuilder();
            RegistrationOfSettings(containerBuilder, pathToFile, pathToSave, imageFormat, excludedWordsList);
            var buildContainer = containerBuilder.Build();

            var settings = buildContainer.Resolve<ISettingsConfiguration>();



            var visualization = settings.Visualizator;
            var reader = settings.FileReader;
            var analyzer = settings.Analyzer;
            var normalizer = settings.Normalization;
            var filler = settings.Filler;
            var pictureSavePath = settings.SavePath;
            var elementSize = settings.ElementSize;
            var saver = settings.Saver;


            analyzer.InvokeMystemAnalization();
            
            var wordsFromFile = reader.ReadText(reader.PreAnalyzedTextPath, reader.ReadingEncoding);
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();

            filler.FillInElements(elementSize, normalyzedWords);

            var elementsForVisualisation = filler.GetElementsList();
            using var drawer = visualization.Invoke(elementsForVisualisation, pictureSavePath);
            drawer.DrawAndSaveImage(saver.GetSavePath(), settings.Format);
        }

        private static IEnumerable<Word> NormalyzeWords(IEnumerable<Word> analyzedWords, INormalizer normalizer)
        {
            foreach (var word in analyzedWords)
            {
                word.WordText = normalizer.Normalize(word.WordText);
                yield return word;
            }
        }

        private static void RegistrationOfSettings(ContainerBuilder buildContainer, string pathToFile, 
            string pathToSave, ImageFormat imageFormat, List<string> excludedWordsList)
        {
            buildContainer.RegisterType<DeffaultSettingsConfiguration>().As<ISettingsConfiguration>()
                .WithParameter("pathToFile", pathToFile)
                .WithParameter("pathToSave", pathToSave)
                .WithParameter("format", imageFormat)
                .WithParameter("excludedWords", excludedWordsList);
        }
    }
}
