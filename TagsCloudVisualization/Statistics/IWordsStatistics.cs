using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ResultProject;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace TagsCloudVisualization.Statistics
{
    public interface IWordsStatistics
    {
        public void AddWords(IEnumerable<string> word);
        Result<IEnumerable<WordCount>> GetStatistics(uint topWordCount);
        Result<IEnumerable<WordCount>> GetStatistics();
        IWordsStatistics CreateStatistics();
    }
}