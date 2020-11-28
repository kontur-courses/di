using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Drawer;
using TagCloud.FileReaders;
using TagCloud.Layouters;
using TagCloud.TextAnalyzer;

namespace TagCloud
{
    public class TagCloud
    {
        private IRectangleLayouter layouter;
        private IFileReader fileReader;
        private ITextAnalyzer textAnalyzer;

        private int minFontSize;
        private int maxFontSize;
        
        public TagCloud(IRectangleLayouter layouter, IFileReader fileReader, ITextAnalyzer textAnalyzer)
        {
            this.layouter = layouter;
            this.fileReader = fileReader;
            this.textAnalyzer = textAnalyzer;
            
            // TODO: задавать конструктором
            minFontSize = 12;
            maxFontSize = 48;
        }

        public void MakeTagCloud(string path)
        {
            var tags = GetTags(path).ToList();
            var picture = new Picture(new Size(2000, 2000));
            picture.FillRectangle(new Rectangle(0, 0, 2000, 2000), Color.Black);
            foreach (var tag in tags.OrderByDescending(t => t.Font.Height))
            {
                tag.Rectangle = layouter.PutNextRectangle(tag.Size);
                picture.DrawTag(tag);
            }

            picture.Save();
        }

        private HashSet<TagInfo> GetTags(string path)
        {
            var tags = new HashSet<TagInfo>();
            var words = fileReader.ReadWords(path);
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