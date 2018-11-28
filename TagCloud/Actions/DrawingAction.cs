using TagCloud.Settings;
using TagCloud.Words;

namespace TagCloud.Actions
{
    public class DrawingAction : IUiAction
    {
        private readonly ImageBox imageBox;
        private readonly Words.Words words;
        private readonly TagGenerator tagGenerator;
        private readonly ImageSettings imageSettings;
        
        public DrawingAction(ImageBox imageBox, Words.Words words, 
                                TagGenerator tagGenerator, ImageSettings imageSettings)
        {
            this.imageBox = imageBox;
            this.words = words;
            this.tagGenerator = tagGenerator;
            this.imageSettings = imageSettings;
        }
        public string Category { get; } = "Tag Cloud";
        public string Name { get; } = "Draw";
        public string Description { get; } = "Draw new Tag Cloud";

        public void Perform()
        {
            var tags = tagGenerator.GetTags(words.Get());
            new TagCloudVisualization.Visualization.TagCloudVisualization(imageBox, imageSettings, tags).GetTagCloudImage();
        }
    }

    
} 