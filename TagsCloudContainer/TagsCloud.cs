using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.ImageSaving;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer
{
    public class TagsCloud
    {
        private List<CloudWord> parsedWords;
        private List<CloudVisualizationWord> visualizationWords;
        private Bitmap visualizedBitmap;
        public void ParseWords(CloudWordsParser wordsParser)
        {
            parsedWords = wordsParser.Parse().ToList();
        }
        
        public void GenerateTagCloud(CloudLayouter cloudLayouter)
        {
            visualizationWords = cloudLayouter.GetWords(parsedWords).ToList();
        }

        public void VisualizeCloud(CloudVisualizer visualizer)
        {
            visualizedBitmap = visualizer.GetBitmap(visualizationWords);
        }

        public void SaveVisualized(ImageSaver saver)
        {
            saver.Save(visualizedBitmap);
        }
    }
}