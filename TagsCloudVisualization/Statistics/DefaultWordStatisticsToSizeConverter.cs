using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Statistics
{
    public class DefaultWordStatisticsToSizeConverter : IWordStatisticsToSizeConverter
    {
        private readonly float maxFontSize;

        public DefaultWordStatisticsToSizeConverter(float maxFontSize = 1000)
        {
            this.maxFontSize = maxFontSize;
        }

        public Result<IEnumerable<TagWordInfo>> Convert(IWordsStatistics statistics, uint topWordCount)
        {
            return statistics.GetStatistics()
                .Then(x => x.ToList())
                .Then(x => (x, x.First().Count, (double)x.Last().Count))
                .Then(x => x.x.Select(y => (y, x.Count, x.Item3)))
                .Then(x => x.ToList())
                .Then(x => x.Take((int)topWordCount < 0 ? x.Count : (int)topWordCount))
                .ThenForEach(wordInfo =>
                {
                    var (wordCount, maxTagWeight, minTagWeight) = wordInfo;
                    var (word, count) = wordCount;
                    var fontSize = count <= minTagWeight
                        ? 1f
                        : (float)Math.Ceiling(maxFontSize * (count - minTagWeight) / (maxTagWeight - minTagWeight));
                    if (maxTagWeight == 1) fontSize = maxFontSize;
                    return new TagWordInfo(word, fontSize);
                });
        }
        
        public Result<IEnumerable<TagWordInfo>> Convert_old(IWordsStatistics statistics, uint topWordCount)
        {
            return statistics.GetStatistics()
                .Then(x => x.ToList())
                .Then(words =>
                {
                    var infos = new List<TagWordInfo>();
                    var maxTagWeight = words.First().Count;
                    var minTagWeight = (double)words.Last().Count;

                    foreach (var (word, count) in words.Take((int)topWordCount < 0 ? words.Count : (int)topWordCount))
                    {
                        var fontSize = count <= minTagWeight
                            ? 1f
                            : (float)Math.Ceiling(maxFontSize * (count - minTagWeight) / (maxTagWeight - minTagWeight));
                        if (maxTagWeight == 1) fontSize = maxFontSize;
                        infos.Add(new TagWordInfo(word, fontSize));
                    }

                    return (IEnumerable<TagWordInfo>)infos;
                });
        }
    }
}