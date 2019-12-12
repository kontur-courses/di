namespace TagsCloudContainer.ProcessingWorld
{
    public interface IProcessor
    {
        void Run(string pathToFile, string pathSave);
    }
}