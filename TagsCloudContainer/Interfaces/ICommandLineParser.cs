using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ICommandLineParser<out TConfiguration> where TConfiguration : IConfiguration
    {
        TConfiguration Parse(IEnumerable<string> args);
    }
}