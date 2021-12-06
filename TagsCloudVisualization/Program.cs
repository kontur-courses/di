using Autofac;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualization.FileReader;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.TextAnalization;
using TagsCloudVisualization.TextAnalization.Analyzer;
using TagsCloudVisualization.TextAnalization.LowerCaseMaker;
using TagsCloudVisualization.TextAnalization.NormalizationMaker;
using TagsCloudVisualization.TextAnalization.VisualizatorMaker;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var textPath = "C:/GitHub/di/War and piece words.DOCX";
            var path = "C:/GitHub/di/img_words.jpeg";
            
            
            
            var containerBuilder = new ContainerBuilder();
            InitializeRegistration(containerBuilder);
            var buildContainer = containerBuilder.Build();
            var normalizationMaker = buildContainer.Resolve<INormalizationMaker>();
            var analyzer = buildContainer.Resolve<IAnalyzer>();
            var layouter = buildContainer.Resolve<ICircularCloudLayouter>();
            var reader = buildContainer.Resolve<ITextFileReader>();
            var lowerCaseMaker = buildContainer.Resolve<ILowerCaseMaker>();

            var rectangleSize = new Size(100, 100);
            var wordsFromFile = reader.ReadText(textPath); //!!!
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).Select(w => w.ToLower());
            var normalyzedWords = normalizationMaker.MakeNormalization(analyzedWords);
            var lowerCaseWords = lowerCaseMaker.MakeTextLowerCase(normalyzedWords);
            var wordList = lowerCaseWords.ToList();


            var visualization = new Visualization(layouter.GetElementsList(), new Pen(Color.White, 10), 
                new SolidBrush(Color.White), new Font("Times", 15));

            layouter.FillInElements(rectangleSize, wordList);

            visualization.DrawAndSaveImage(new Size(5000, 5000), path, ImageFormat.Jpeg);
        }

        private static void InitializeRegistration(ContainerBuilder buildContainer)
        {
            RegistrationOfTextFileReader(buildContainer);
            RegistrationOfCloudLayouter(buildContainer);
            RegistrationOfNormalizationMaker(buildContainer);
            RegistrationOfVisualizator(buildContainer);
            RegistrationOfTextAnalyzer(buildContainer);
            RegistrationOfTextLowercaseMaker(buildContainer);
        }

        private static void RegistrationOfVisualizator(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<VisualizatorMaker>().As<IVisualizatorMaker>();
        }

        private static void RegistrationOfNormalizationMaker(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<NormalizationMaker>().As<INormalizationMaker>();
        }

        private static void RegistrationOfCloudLayouter(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<CircularCloudLayouterForRectanglesWithText>().As<ICircularCloudLayouter>()
                .WithParameter("center", new Point(2500, 2500));

            //.WithParameter("words", new List<Word>());
        }

        private static void RegistrationOfTextLowercaseMaker(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<LowerCaseMaker>().As<ILowerCaseMaker>();
        }

        private static void RegistrationOfTextAnalyzer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<Analyzer>().As<IAnalyzer>()
                .WithParameter("speechParts",
                    Enum.GetValues(typeof(PartsOfSpeech.SpeechPart)).Cast<PartsOfSpeech.SpeechPart>());
        }

        private static void RegistrationOfTextFileReader(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<TextFileReader>().As<ITextFileReader>();
        }
    }
}
