using System.Collections.Generic;

namespace TagsCloudContainer.Common.Contracts
{
    public interface ITextAnalyzer
    {
        public Dictionary<string, int> GetWordStatisticsFromFile(string path);
        
        public Dictionary<string, int> GetWordStatistics(string text);
    }
}