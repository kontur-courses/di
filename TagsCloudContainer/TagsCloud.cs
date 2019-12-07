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
        private readonly ICloudWordsParser wordsParser;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly IImageSaver imageSaver;

        public TagsCloud(
            ICloudWordsParser wordsParser,
            ICloudLayouter cloudLayouter,
            ICloudVisualizer cloudVisualizer,
            IImageSaver imageSaver)
        {
            this.wordsParser = wordsParser;
            this.cloudLayouter = cloudLayouter;
            this.cloudVisualizer = cloudVisualizer;
            this.imageSaver = imageSaver;
        }
        public void ParseWords()
        {
            parsedWords = wordsParser.Parse().ToList();
        }
        
        public void GenerateTagCloud()
        {
            if (parsedWords is null) throw new InvalidOperationException("You should parse your words first!");
            visualizationWords = cloudLayouter.GetWords(parsedWords).ToList();
        }

        public void VisualizeCloud()
        {
            if (visualizationWords is null)
                GenerateTagCloud();
            visualizedBitmap = cloudVisualizer.GetBitmap(visualizationWords);
        }

        public void SaveVisualized()
        {
            if(visualizedBitmap is null)
                VisualizeCloud();
            imageSaver.Save(visualizedBitmap);
        }
    }
}