namespace TagsCloudContainer.CloudGenerators;

public interface ITagsCloudGenerator
{
    public ITagCloud Generate(IEnumerable<WordDetails> wordsDetails);
}