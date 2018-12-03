using System;
using TagCloud.Interfaces;
using TagCloud.Settings;
using TagCloud.Words;

namespace TagCloud.Actions
{
    public class DrawingAction : IUiAction
    {
        private readonly ImageBox imageBox;
        private readonly IRepository wordsRepository;
        private readonly ITagGenerator tagGenerator;
        private readonly ImageSettings imageSettings;
        
        public DrawingAction(ImageBox imageBox, IRepository wordsRepository, 
                                ITagGenerator tagGenerator, ImageSettings imageSettings)
        {
            this.imageBox = imageBox;
            this.wordsRepository = wordsRepository;
            this.tagGenerator = tagGenerator;
            this.imageSettings = imageSettings;
        }
        public string Category { get; } = "Tag Cloud";
        public string Name { get; } = "Draw";
        public string Description { get; } = "Draw new Tag Cloud";

        public void Perform()
        {
            var tags = tagGenerator.GetTags(wordsRepository.Get());
            new TagCloudVisualization.Visualization.TagCloudVisualization(imageBox, imageSettings, tags).GetTagCloudImage();
        }
    }

    
} 