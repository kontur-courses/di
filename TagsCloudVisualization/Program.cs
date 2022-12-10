using CommandLine;
using TagsCloudVisualization.Enums;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Options.RunOptions);
            
            var words = Preprocessor.Process(Options.WordsFilePath, 
                PartSpeech.Noun | PartSpeech.Adjective);

            TagCloudCreator.Create(words, 100);
        }
    }
}