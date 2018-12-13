using System.Linq;
using TagsCloudPreprocessor;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    public class TagCloudProgram
    {
        private readonly Config config;
        private readonly IPreprocessor preprocessor;
        private readonly ITagCloudVisualization tagCloudVisualization;
        
        public TagCloudProgram(
            IPreprocessor preprocessor, 
            ITagCloudVisualization tagCloudVisualization, 
            Config config)
        {
            this.preprocessor = preprocessor;
            this.tagCloudVisualization = tagCloudVisualization;
            this.config = config;
        }
        
        public void SaveTagCloud()
        {
            var validWords = preprocessor
                .GetValidWordsWithCount(config.InputFile, config.Count).ToList();
            
            tagCloudVisualization.SaveTagCloud(
                config.FileName,
                config.OutPath,
                validWords);
        }
    }
}