using CloudDrawing;

namespace TagsCloudContainer.ProcessingWords
{
    public interface IProcessor
    {
        void Run(string pathToFile, string pathToSaveFile, ImageSettings imageSettings,
            WordDrawSettings wordDrawSettings);
    }
}