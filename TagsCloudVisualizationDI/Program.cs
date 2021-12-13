using Autofac;
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
            var elementSize = settings.ElementSize;
            var saver = settings.Saver;

            analyzer.InvokeMystemAnalization();
            var wordsFromFile = reader.ReadText();
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();
            var formedElements = filler.FormElements(elementSize, normalyzedWords);
            var sizedElements = visualization.FindSizeForElements(formedElements);
            var sortedElements = sizedElements.
                OrderByDescending(el => el.WordElement.CntOfWords).ToList();
            var positionedElements = filler.MakePositionElements(sortedElements);

            visualization.DrawAndSaveImage(positionedElements, saver.GetSavePath(), settings.Format);
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
            buildContainer.RegisterType<DefaultSettingsConfiguration>().As<ISettingsConfiguration>()
                .WithParameter("pathToFile", pathToFile)
                .WithParameter("pathToSave", pathToSave)
                .WithParameter("format", imageFormat)
                .WithParameter("excludedWords", excludedWordsList);
        }
    }
}
