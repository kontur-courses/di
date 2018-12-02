namespace TagsCloudContainer.ResultFormatters
{
    public interface IResultFormatter
    {
        void GenerateResult(string fileName, string fileExtension);
    }
}
