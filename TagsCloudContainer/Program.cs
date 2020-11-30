using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using CommandLine;
using LightInject;
using TagsCloudVisualization;
using WeCantSpell.Hunspell;

namespace TagsCloudContainer
{
    class Program
    {
        private static AppSettings _appSettings;
        private static readonly string _relativePathToDict = "../../../../Dictionaries/English (American).dic";
        private static readonly string _relativePathToAffixFile = "../../../../Dictionaries/English (American).aff";

        static void Main(string[] args)
        {
            try
            {
                var settings = Parser.Default.ParseArguments<AppSettings>(args);
                if (settings.Tag == ParserResultType.NotParsed)
                    throw new ArgumentException("Incorrect commandline arguments");

                _appSettings = ((Parsed<AppSettings>) settings).Value;
            
                var words = GetWords();
                var countedWords = CountWords(words);
                var image = GetDrawer().Draw(countedWords);
                SaveImage(image);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static string[] GetWords()
        {
            return new FileWordsLoader(_appSettings.PathToFile).GetWords();
        }

        private static Dictionary<string, int> CountWords(string[] words)
        {
            var excludedPartsOfSpeech = new List<PartOfSpeech>();
            foreach (var name in _appSettings.ExcludedPartsOfSpeechNames)
            {
                if (Enum.TryParse<PartOfSpeech>(name, true, out var partOfSpeech))
                    excludedPartsOfSpeech.Add(partOfSpeech);
                else
                    throw new ArgumentException($"Incorrect name of part of speech: {name}");
            }

            var appPath = Assembly.GetExecutingAssembly().Location;
            var wordList = WordList.CreateFromFiles( 
                Path.GetFullPath(_relativePathToDict, appPath),
                Path.GetFullPath(_relativePathToAffixFile, appPath));
            
            return new WordsCounter(wordList, excludedPartsOfSpeech).CountWords(words);
        }

        private static ITagsCloudDrawer GetDrawer()
        {
            var container = new ServiceContainer();

            var fontColor = Color.FromName(_appSettings.FontColorName);
            container.Register<IFontColorCreator>(factory =>
                new FontColorCreator(fontColor));
            
            container.Register<IFontCreator>(factory =>
                new FontCreator(_appSettings.FontName));

            var center = new Point(_appSettings.ImageWidth/2, _appSettings.ImageHeight/2);
            var angleInRadians = _appSettings.AngleStepInDegrees * Math.PI / 180;
            container.Register<ICloudLayouter>(factory =>
                new CircularCloudLayouter(center, new Spiral(angleInRadians, _appSettings.ShiftFactor)));
            
            container.Register<ITagsCloudSettings>(factory => _appSettings);
            
            container.Register<ITagsCloudDrawer, TagsCloudDrawer>();

            return container.GetInstance<ITagsCloudDrawer>();
        }

        private static void SaveImage(Bitmap image)
        {
            new ImageSaver(_appSettings.ImagePath).Save(image);
        }
    }
}