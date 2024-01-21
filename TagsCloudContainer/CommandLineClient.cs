using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CommandLineClient : ITagCloudClient
    {
        private readonly IFileReader _fileReader;
        private readonly IPreprocessor _preprocessor;
        private readonly ITagCloudGenerator _tagCloudGenerator;
        private readonly IImageSettings _imageSettings;

        public CommandLineClient(IFileReader fileReader, IPreprocessor preprocessor,
            ITagCloudGenerator tagCloudGenerator, IImageSettings imageSettings)
        {
            _fileReader = fileReader;
            _preprocessor = preprocessor;
            _tagCloudGenerator = tagCloudGenerator;
            _imageSettings = imageSettings;
        }

        public void Run()
        {
            //TODO: Логика для CLI
        }
    }
}
