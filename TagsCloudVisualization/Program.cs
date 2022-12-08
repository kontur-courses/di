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

            // var fileGenerator = new FileGenerator();
            // fileGenerator.Generate(_wordsFilePath, 1000);
            
            var prepocessor = new Preprocessor();
            var words = prepocessor.Process(Options.WordsFilePath);
            
            TagCloudCreator.Create(words, Options.SaveTagCloudImagePath, CloudConfiguration.Default, DistributionConfiguration.Default, Options.AmountImages);
        }
    }
}