using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class ProgrammMain
    {
        public static void Execute(ConsoleParser.StandartOptions parsedArgs) //TODO Preprocessing Words(dull words)
        {
            var textFile = parsedArgs.File;
            var wordsFrequencyDict = FileHandler.GetWordsFrequencyDict(textFile, new Predicate<string>(s => true));
            var rectanglesSizes = GeneratingWordsSizesAlgorithms.DefaultAlgorithm(wordsFrequencyDict);
            var drawer = new TagCloudDrawing(2000, 2000, "test", new Point(0, 0));
            drawer.DrawTagCloud(rectanglesSizes);
        }
    }
}