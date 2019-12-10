using CommandLine;

namespace TagsCloudVisualization.ApplicationOptions
{
    public class ApplicationOptionsExtractor
    {
        public ApplicationOptions GetOption(ParserResult<ApplicationOptions> parsedOptions)
        {
            var applicationOptions = new ApplicationOptions();
            parsedOptions.WithParsed(o =>
            {
                applicationOptions.FontFamily = o.FontFamily;
                applicationOptions.FontSize = o.FontSize;
                applicationOptions.ImageHeight = o.ImageHeight;
                applicationOptions.ImageName = o.ImageName;
                applicationOptions.ImageWidth = o.ImageWidth;
                applicationOptions.TextColorName = o.TextColorName;
                applicationOptions.TextName = o.TextName;
                applicationOptions.BackGroundColorName = o.BackGroundColorName;;
            });
            
            return applicationOptions;
        }
    }
}