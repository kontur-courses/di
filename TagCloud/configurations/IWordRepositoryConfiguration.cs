using System.Collections.Generic;

namespace TagCloud.configurations
{
    public interface IWordRepositoryConfiguration
    {
        IEnumerable<string> Handle(IEnumerable<string> source);
        IEnumerable<string> Filter(IEnumerable<string> source);
    }
}