using System.Collections.Generic;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainerCLI.CommandLineParser
{
    public interface ICommandLineParser<out TConfiguration> where TConfiguration : IConfiguration
    {
        TConfiguration Parse(IEnumerable<string> args);
    }
}