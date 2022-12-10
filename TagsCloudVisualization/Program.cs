using CommandLine;
using TagsCloudVisualization.Configurations;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Options.RunOptions);
            
            var words = Preprocessor.Process(Options.WordsFilePath);
            
            TagCloudCreator.Create(words);
        }
    }
}