using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Drawers;
using TagCloud.DataReaders;
using TagCloud.Layouters;
using TagCloud.TextAnalyzer;

namespace TagCloud
{
    public class TagCloud
    {
        private ITagCloudDrawer drawer;
        private IDataReader dataReader;
        private ITextAnalyzer textAnalyzer;

        private int minFontSize;
        private int maxFontSize;
        
        public TagCloud(ITagCloudDrawer drawer, IDataReader dataReader, ITextAnalyzer textAnalyzer)
        {
            this.drawer = drawer;
            this.dataReader = dataReader;
            this.textAnalyzer = textAnalyzer;
            
            // TODO: задавать конструктором
            minFontSize = 12;
            maxFontSize = 48;
        }

        public void MakeTagCloud(string path)
        {
            var tags = GetTags(path);
            drawer.DrawTagCloud(tags);
        }
       

        private HashSet<TagInfo> GetTags(string path)
        {
            var tags = new HashSet<TagInfo>();
            var words = dataReader.ReadWords(path);
            var wordsToCount = textAnalyzer.GetWordsCounts(words);
            
            var minCount = wordsToCount.Values.ToList().Min();
            var maxCount = wordsToCount.Values.ToList().Max();
            
            foreach (var wordToCount in wordsToCount)
            {
                var wordTag = new TagInfo(wordToCount.Key,
                    GetFontSizeRelativeToCount(wordToCount.Value, minCount, maxCount));
                tags.Add(wordTag);
            }

            return tags;
        }

        // TODO: сделать отдельной функцией, передавать в конструктор
        private int GetFontSizeRelativeToCount(int count, int minCount, int maxCount)
        {
            if (minCount >= maxCount)
                return maxFontSize;
            var proportion = (double)(count - minCount) / (maxCount - minCount);
            return Convert.ToInt32((maxFontSize - minFontSize) * proportion + minFontSize);
        }
    }
}