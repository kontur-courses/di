using TagCloudVisualization;

namespace TagCloudCreation
{
    public class TagCloudCreationOptions
    {
        public TagCloudCreationOptions(ImageCreatingOptions imageOptions, string pathToBoringWords = null)
        {
            ImageOptions = imageOptions;
            PathToBoringWords = pathToBoringWords;
        }

        public ImageCreatingOptions ImageOptions { get; }
        public string PathToBoringWords { get;}
    }
}
