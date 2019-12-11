using CommandLine;

namespace TagsCloudVisualization.ApplicationOptions
{
    public class ApplicationOptionsExtractor
    {
        public ApplicationOptions GetOption(ParserResult<ApplicationOptions> parsedOptions)
        {
            var applicationOptions = new ApplicationOptions();
            parsedOptions.WithParsed(options => applicationOptions = options);
            return applicationOptions;
        }
    }
}