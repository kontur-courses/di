using TagCloudGenerator.Clients.CmdClient;
using TagCloudGenerator.GeneratorCore;
using TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors;

namespace TagCloudGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var cmdClient = new CommandLineClient(args);
            var contextGenerator = new CloudContextGenerator(cmdClient);
            var preprocessor = GetPreprocessor(contextGenerator);
            var cloudGenerator = new CloudGenerator(contextGenerator, preprocessor);

            cloudGenerator.CreateTagCloudImage();
        }

        private static CloudVocabularyPreprocessor GetPreprocessor(CloudContextGenerator cloudContextGenerator)
        {
            var cloudContext = cloudContextGenerator.GetTagCloudContext();

            CloudVocabularyPreprocessor preprocessor = new ExcludingPreprocessors(null, cloudContext);
            preprocessor = new ToLowerPreprocessor(preprocessor);

            return preprocessor;
        }
    }
}