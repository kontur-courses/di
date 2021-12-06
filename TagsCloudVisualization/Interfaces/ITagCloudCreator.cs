namespace TagsCloudVisualization.Interfaces
{
    public interface ITagCloudCreator
    {
        void CreateAndSaveCloudFromTo(string inputPath, string outputPath);
    }
}