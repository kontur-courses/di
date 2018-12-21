using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.ColorAlgorithm;
using TagsCloudContainer.WordsFilter;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudContainer
{
    public class ContainersCreator : IContainersCreator
    {
        public List<ContainerInfo> ContainersInfo => new List<ContainerInfo>(containersInfo);
        private readonly List<ContainerInfo> containersInfo;
        private readonly ICloudLayouter circularLayouter;
        private readonly IColorAlgorithm colorAlgorithm;
        private readonly Dictionary<string, int> filteredWords;
        private readonly int maxFontSize;
        private readonly string fontName;

        public ContainersCreator(IFilteredWords filteredWords,
            ICloudLayouter circularLayouter,
            IColorAlgorithm colorAlgorithm,
            string fontName = "Arial",
            int maxFontSize = 50)
        {
            this.circularLayouter = circularLayouter;
            this.colorAlgorithm = colorAlgorithm;
            containersInfo = new List<ContainerInfo>();
            this.filteredWords = filteredWords.FilteredWordsList;
            this.maxFontSize = maxFontSize;
            this.fontName = fontName;

            InitializeContainers();
        }

        private void InitializeContainers()
        {
            var maxWordRepeatCount = filteredWords
                .Max(word => word.Value);
            foreach (var word in filteredWords)
            {
                var fontSize = Math.Max(10, (int)(maxFontSize * filteredWords[word.Key] / maxWordRepeatCount));
                var containerSize = new Size((int)(word.Key.Length * fontSize / 1.1), (int)(fontSize * 1.5));

                containersInfo.Add(new ContainerInfo(
                    text: word.Key,
                    textFont: new Font(fontName, fontSize),
                    rectangle: circularLayouter.PutNextRectangle(containerSize),
                    textColor: colorAlgorithm.GetColor(filteredWords, word.Key)
                    ));
            }
        }
    }
}
