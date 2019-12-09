namespace TagsCloudGenerator.Interfaces
{
    public interface IGenerator
    {
        bool TryGenerate(string readFromPath, string saveToPath);
    }
}