namespace TagsCloudContainer.CloudGenerators;

public interface ITagsCloudGenerator
{
    public ITagCloud Generate(AnalyzeData analyzeData);
}