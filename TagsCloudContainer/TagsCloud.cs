using System;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer
{
    public class TagsCloud
    {
        public void ParseWords(ICloudWordsParser wordsParser, string wordsPath)
        {
            throw new NotImplementedException();
        }
        
        public void GenerateTagCloud(ICloudLayouter cloudLayouter)
        {
            throw new NotImplementedException();
        }

        public void VisualizeCloud(ICloudVisualizer cloudVisualizer)
        {
            throw new NotImplementedException();
        }

        public void SaveVisualized(ImageFileFormats fileFormat, string pathToSave)
        {
            throw new NotImplementedException();
        }
    }
}