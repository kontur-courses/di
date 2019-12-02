namespace TagsCloudGenerator.Interfaces
{
    public interface ITagsCloudGenerator
    {
        bool TryGenerate(string fromFile, string font, System.Drawing.Size imageSize, string pathToSave);
    }
}