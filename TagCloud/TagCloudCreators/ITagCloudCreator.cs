using TagCloud.CloudLayouters;
using TagCloud.TagCloudVisualizations;
using TagCloud.WordPreprocessors;

namespace TagCloud.TagCloudCreators
{
    public interface ITagCloudCreator
    {
        public delegate ITagCloudCreator Factory(
            ICloudLayouter.Factory cloudLayouterFactory,
            IWordPreprocessor wordPreprocessor,
            ITagCloudVisualizationSettings settings);

        public ITagCloud GenerateTagCloud();
    }
}
