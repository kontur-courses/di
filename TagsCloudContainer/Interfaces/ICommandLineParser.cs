using System.Collections.Generic;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public interface ICommandLineParser<out TConfiguration> where TConfiguration : IConfiguration
    {
        TConfiguration Parse(IEnumerable<string> args);
    }
}