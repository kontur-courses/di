using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class ContainerFormatter
    {
        private readonly List<ContainerInfo> containersInfo;
        public List<ContainerInfo> ContainersInfo => new List<ContainerInfo>(containersInfo);
        private readonly CircularCloudLayouter circularLayouter;

        public ContainerFormatter(Dictionary<string, int> wordsWithFrequency, CircularCloudLayouter circularLayouter,
            int maxFontSize = 50)
        {
            this.circularLayouter = circularLayouter;
            containersInfo = new List<ContainerInfo>();
            var maxWordRepeatCount = wordsWithFrequency.Max(word => word.Value);

            InitializeContainers(wordsWithFrequency, maxFontSize, maxWordRepeatCount);
        }

        private void InitializeContainers(Dictionary<string, int> wordsWithFrequency,
            int maxFontSize, int maxWordRepeatCount)
        {
            foreach (var word in wordsWithFrequency.OrderBy(word => word.Value).ThenBy(word => word.Key.Length))
            {
                var fontSize = Math.Max(10, maxFontSize * (maxWordRepeatCount - wordsWithFrequency[word.Key]) / maxWordRepeatCount);
                var containerSize = new Size((int)(word.Key.Length * fontSize / 1.1), (int)(fontSize * 1.5));

                containersInfo.Add(new ContainerInfo(
                    text: word.Key,
                    textFont: new Font("Tahoma", fontSize),
                    rectangle: circularLayouter.PutNextRectangle(containerSize)
                    ));
            }
        }
    }
}
