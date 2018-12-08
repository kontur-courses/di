using TagCloudVisualization;

namespace TagCloudCreation
{
    public class TagCloudCreationOptions
    {
        public TagCloudCreationOptions(ImageCreatingOptions imageOptions)
        {
            ImageOptions = imageOptions;
        }

        public ImageCreatingOptions ImageOptions { get;  }
    }
}
