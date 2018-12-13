using System.Collections.Generic;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.CommandLineParser
{
    public interface ICommandLineParser<out TConfiguration> where TConfiguration : IConfiguration
    {
        TConfiguration Parse(IEnumerable<string> args);
    }
}