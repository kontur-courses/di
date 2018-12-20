using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.WordsFilter;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudContainer
{
    public class ContainersCreator : IContainersCreator
    {
        public List<ContainerInfo> ContainersInfo => new List<ContainerInfo>(containersInfo);
        private readonly List<ContainerInfo> containersInfo;
        private readonly ICloudLayouter circularLayouter;
        private readonly int maxWordRepeatCount;
        private readonly Dictionary<string, int> wordsWithFrequency;
        private readonly int maxFontSize;
        private readonly string fontName;

        public ContainersCreator(IWordsFilters wordsFilters,
            ICloudLayouter circularLayouter,
            string fontName = "Tahoma",
            int maxFontSize = 50)
        {
            this.circularLayouter = circularLayouter;
            containersInfo = new List<ContainerInfo>();
            maxWordRepeatCount = wordsFilters
                .FilteredWords
                .Max(word => word.Value);
            wordsWithFrequency = wordsFilters.FilteredWords;
            this.maxFontSize = maxFontSize;
            this.fontName = fontName;
        }

        public void InitializeContainers()
        {
            foreach (var word in wordsWithFrequency)
            {
                var fontSize = Math.Max(10, maxFontSize * (maxWordRepeatCount - wordsWithFrequency[word.Key]) / maxWordRepeatCount);
                var containerSize = new Size((int)(word.Key.Length * fontSize / 1.1), (int)(fontSize * 1.5));

                containersInfo.Add(new ContainerInfo(
                    text: word.Key,
                    textFont: new Font(fontName, fontSize),
                    rectangle: circularLayouter.PutNextRectangle(containerSize),
                    textColor: GetAppropriateColor()
                    ));
            }
        }

        private Color GetAppropriateColor()
        {
            return Color.Black;
        }
    }
}
